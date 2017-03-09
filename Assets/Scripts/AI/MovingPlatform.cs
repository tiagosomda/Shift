using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
	
	void Update(){
		
		
	}
	
	void OnCollisionStay( Collision collide ){
		
		if(collide.gameObject.tag == "Player"){
			
			collide.gameObject.transform.parent = this.gameObject.transform;
			//Vector3 newPosition = new Vector3(this.transform.position.x, collide.gameObject.transform.position.y, this.transform.position.z); 
			//collide.transform.position = newPosition;
			
		}
		
	}
	
	void OnCollisionExit( Collision collide ){
		
		if(collide.gameObject.tag == "Player"){
			
			collide.gameObject.transform.parent = null;
			//Vector3 newPosition = new Vector3(this.transform.position.x, collide.gameObject.transform.position.y, this.transform.position.z); 
			//collide.transform.position = newPosition;
			
		}
		
	}
	
}
