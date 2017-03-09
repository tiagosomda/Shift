using UnityEngine;
using System.Collections;

public class crane : MonoBehaviour {
	
	public GameObject hookPlatform;
	public GameObject hook;
	public GameObject arm;
	public GameObject armExtension;
	public GameObject pointOfRotation;
	
	public GameObject[] collectionOfBoxes;
	public float speed = 0.4f;
	
	private GameObject currentBox;
	
	private int stage;
	private int substage;
	private float origHookY;
	private Vector3 origHookPos;
	private Vector3 dropPos;
	
	private float rotationAngle;
	private float rotationCur;
	
	private Vector3 moveTotal;
	private Vector3 moveCur;
	
	// Iterates through boxes in order
	private int boxIterator;	
	// Magnetic surface for picking crates up
	public GameObject magnet;
	
	// Use this for initialization
	void Start () {
		stage = 0;
		origHookPos = hook.transform.position;
		origHookY = origHookPos.y;
		dropPos.y = collectionOfBoxes[ 0 ].transform.position.y;
		
		this.boxIterator = 0;
	}
	
	
	bool rotateCrane(bool withBox){
		
		float rot = rotationAngle*Time.deltaTime*speed;
		
		if (Mathf.Abs(rotationCur+rot) > Mathf.Abs(rotationAngle)){
			rot=rotationAngle-rotationCur;
		}
		rotationCur+=rot;
		
		arm.transform.RotateAround(pointOfRotation.GetComponent<Renderer>().bounds.center,Vector3.up,rot);
		
		if (withBox){
			//currentBox.transform.RotateAround(pointOfRotation.renderer.bounds.center,Vector3.up,rot);
		}
			
		
		if (Mathf.Abs(rotationCur)<Mathf.Abs(rotationAngle)){
			return false;
		}
		return true;
		
		
	}
	
	
	bool movePlatform(bool withBox){
		
		Vector3 tran = moveTotal*Time.deltaTime*speed;
		
		moveCur+=tran;
		
		if (Mathf.Abs(moveCur.x)>Mathf.Abs(moveTotal.x) && Mathf.Abs(moveCur.y)>Mathf.Abs(moveTotal.y) && Mathf.Abs(moveCur.z)>Mathf.Abs(moveTotal.y)){
			moveCur-=tran;
			tran=moveTotal-moveCur;
			moveCur+=tran;
		}
		
		
		hookPlatform.transform.Translate(tran);
		
		if (withBox){
			//currentBox.transform.position= new Vector3(hookPlatform.transform.position.x,currentBox.transform.position.y,hookPlatform.transform.position.z);
		}	
		
		if (Mathf.Abs(moveCur.x)<Mathf.Abs(moveTotal.x) || Mathf.Abs(moveCur.y)<Mathf.Abs(moveTotal.y) || Mathf.Abs(moveCur.z)<Mathf.Abs(moveTotal.z)){
			return false;
		}
		return true;
		
	}
	
	
	
	bool moveHook(bool withBox){
		
		
		Vector3 tran = moveTotal*Time.deltaTime*speed;
		
		moveCur+=tran;
		
		if (Mathf.Abs(moveCur.y)>Mathf.Abs(moveTotal.y)){
			moveCur-=tran;
			tran=moveTotal-moveCur;
			moveCur+=tran;
		}
		
		
		hook.transform.Translate(tran,Space.World);
		
		if (withBox){
			//currentBox.transform.Translate(tran,Space.World);
		}	
		
		
		if (Mathf.Abs(moveCur.x)<Mathf.Abs(moveTotal.x) || Mathf.Abs(moveCur.y)<Mathf.Abs(moveTotal.y) || Mathf.Abs(moveCur.z)<Mathf.Abs(moveTotal.z)){
			return false;
		}
		return true;
		
		
	}
		
		
	
	/** Picks the next box to move to and pick up by iterating through the list of boxes */
	void pickNextBox(){
		this.currentBox = this.collectionOfBoxes[ this.boxIterator ];
		this.boxIterator = ( this.boxIterator + 1 ) % this.collectionOfBoxes.Length;
	}
	
	/** Gets the drop location from the box and sets the location of dropPos to that location */
	void setDropPos(){
		Vector3 dropLocation = this.currentBox.GetComponent< craneBox >().getDropLocation();		
		dropPos.x = dropLocation.x +=pointOfRotation.GetComponent<Renderer>().bounds.center.x;		
		dropPos.z = dropLocation.z +=pointOfRotation.GetComponent<Renderer>().bounds.center.z;				
	}
	
	void findAngle(Vector3 pointA){
		
		pointA.y=pointOfRotation.transform.position.y;
		
		Vector3 pointB = pointOfRotation.transform.position;
		
		Vector3 pointC = armExtension.GetComponent<Renderer>().bounds.center;
		pointC.y=pointOfRotation.transform.position.y;
		
		pointA= pointA-pointB;
		pointC=pointC-pointB;
		pointC.Normalize();
		pointA.Normalize();
		float dot;
		dot = Vector3.Dot(pointC,pointA);
		float angle = Mathf.Acos(dot);
		Vector3 cross;
		cross= Vector3.Cross(pointC, pointA);
		Vector3 planeNormal=Vector3.up;
		dot= Vector3.Dot( cross, planeNormal);
		
		if (dot<0){
			angle*=-1;
		}
		rotationAngle=angle*Mathf.Rad2Deg;
		
		
		
		rotationCur=0;
	
	}
	
	void findPlat(Vector3 point){
		point.y=pointOfRotation.GetComponent<Renderer>().bounds.center.y;
		
		Vector3 plat = hookPlatform.GetComponent<Renderer>().bounds.center;
		plat.y=pointOfRotation.GetComponent<Renderer>().bounds.center.y;
		
		
		
		moveTotal=new Vector3(0,Vector3.Distance(point,plat),0);
		if (Vector3.Distance(point,pointOfRotation.GetComponent<Renderer>().bounds.center)<Vector3.Distance(plat,pointOfRotation.GetComponent<Renderer>().bounds.center)){
			moveTotal.y*=-1;
		}
		
		
		moveCur=Vector3.zero;
	}
	
	void findHook(Vector3 point){
		
		Vector3 h=hook.GetComponent<Renderer>().bounds.center;
		h.y=hook.GetComponent<Renderer>().bounds.min.y;
		
		moveTotal=point-h;
		
		moveCur=Vector3.zero;
		
	}
	
	
	
	void Update () {	
		if (stage==0){
		
			pickNextBox();
			findAngle(currentBox.transform.position);
		
			stage+=1;
		}
		if (stage==1){
			if (rotateCrane(false)){
				stage+=1;
				findPlat(currentBox.transform.position);
				
			}
		}
		if (stage==2){
			if (movePlatform(false)){
				stage+=1;	
				origHookPos=hook.transform.position;
				origHookPos.y=origHookY;
				
				findHook(new Vector3(currentBox.transform.position.x,currentBox.GetComponent<Renderer>().bounds.max.y,currentBox.transform.position.z));
			}
		}
		if (stage==3){
			if (moveHook(false)){
				stage+=1;
				//currentBox.rigidbody.useGravity=false;
				
				this.magnet.GetComponent< Magnetism >().SetCharge( 1000 );
				
				findHook(origHookPos);
			}
		}
		if (stage==4){
			if (moveHook(true)){
				stage+=1;
			}
		}
		if (stage==5){
			setDropPos();
			findAngle(dropPos);
			stage+=1;
		}
		if (stage==6){
			if (rotateCrane(true)){
				stage+=1;
				findPlat(dropPos);
			}
		}
		if (stage==7){
			if (movePlatform(true)){
				stage=0;
				origHookPos=hook.transform.position;
				origHookPos.y=origHookY;
				//currentBox.rigidbody.useGravity=true;
				
				this.magnet.GetComponent< Magnetism >().SetCharge( 0 );
			}
		}
		
		Vector3 position = new Vector3( this.hook.transform.position.x, this.hook.transform.position.y + 0.1f, this.hook.transform.position.z );
		this.magnet.transform.position = position;
		
		
		
			
	
	}
	
}
