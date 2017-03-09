using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour {
	
	public bool checkPos;
	public bool shake;
	private bool falling;
	private Quaternion toRot;
	private Quaternion fromRot;
	public float rotSpeed=1;
	public float topRotSpeed=60;
	public float numSpeeds=3;
	public float fallSpeed=0;
	private float t;
	private GameObject player;
	private float fallDist;
	public float randMult;
	
	
	
	
	
	// Use this for initialization
	void Start () {
		checkPos=false;
		shake=false;
		if (this.renderer==null){
			foreach (Transform child in transform) {
				this.GetComponent<MeshCollider>().sharedMesh=child.GetComponent<MeshFilter>().mesh; 
	       	}
		}
		
		toRot=this.transform.rotation;
		fromRot=toRot;
		randMult=Random.Range(.3f,5.0f);
		
	}
	
	public void setPlayer(GameObject p){
		player = p;		
	}
	public void setFallDist(float f){
		fallDist = f;
	}
	public void setCheck(bool c){
		checkPos=c;
	}
	
	
	
	
	// Update is called once per frame
	void Update () {
		
		if (checkPos){
			
			float dist = Vector2.Distance(new Vector2(player.transform.position.x,player.transform.position.z),new Vector2(this.transform.position.x,this.transform.position.z));
			
			if (dist<fallDist){
				shake=true;
				rotSpeed= (topRotSpeed+(2*randMult))-((topRotSpeed/((fallDist/dist))));
				rotSpeed= rotSpeed - (rotSpeed%(topRotSpeed/numSpeeds));
				Debug.Log("rotSpeed: "+rotSpeed);
			}
			else{
				shake=false;
			}
			
			if (shake){
				HandleShaking();
				if (rotSpeed>=topRotSpeed){
					HandleFall();
				}
			}
			
			t+=(Time.deltaTime)*rotSpeed;
			if (t<=1.0f){
				this.transform.rotation=Quaternion.Slerp(fromRot,toRot,t);
			}
			
		}
		
		if (falling){
			fallSpeed+=(Time.deltaTime*2);
			this.transform.Translate(new Vector3(0.0f,-fallSpeed,0.0f));
		}	
		
		
	
	}
	
	void HandleFall(){
		shake=false;
		t=0.0f;
		toRot=new Quaternion(0.0f,0.0f,0.0f,1.0f);
		fromRot=this.transform.rotation;
		falling=true;
		
		
			
	}
	
	
	void HandleShaking(){
		if (t>1.0f){
			toRot=new Quaternion(Random.Range(-.1f,.1f),0.0f,Random.Range(-.1f,.1f),1.0f);
			fromRot=this.transform.rotation;
			t=0.0f;
		}
		
	}
	
	
	
	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.name.Equals("Player"))			
			collision.gameObject.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
	}
	
}
