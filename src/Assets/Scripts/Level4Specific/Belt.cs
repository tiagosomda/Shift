using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Belt : MonoBehaviour {
	
	public List<GameObject> objsOn;
	public List<GameObject> objsIgnore;
	public Vector3 moveDirection;
	public float speed;
	
	void Start () {
		
		((movingTexture)this.gameObject.GetComponent<movingTexture>()).speed.x=moveDirection.x*speed;
		((movingTexture)this.gameObject.GetComponent<movingTexture>()).speed.y=moveDirection.z*speed;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		for (int i=0; i<objsOn.Count; i++){
			if (objsOn[i].GetComponent<Rigidbody>()==null){
				objsOn[i].transform.Translate((moveDirection*speed*Time.deltaTime),Space.World);
			}
			else{	
				objsOn[i].GetComponent<Rigidbody>().MovePosition(objsOn[i].transform.position+ (moveDirection*speed*Time.deltaTime));
			}
		}
		
	
	}
	
	
	void OnCollisionEnter(Collision c){
		if (!objsOn.Contains(c.gameObject) && !objsIgnore.Contains(c.gameObject)){
			objsOn.Add(c.gameObject);
		}
	}
	
	
	void OnCollisionExit(Collision c){
		if (objsOn.Contains(c.gameObject)){
			objsOn.Remove(c.gameObject);
		}
	}
}
