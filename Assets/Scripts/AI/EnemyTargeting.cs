using UnityEngine;
using System.Collections;

//This class is for the enemy AI targeting the player within a certain radius
//if the player is within that radius the enemy performs and action
public class EnemyTargeting : MonoBehaviour {
	
	public float lineOfSight;
	
	GameObject[] player;
	// Each update the enemy checks the radius for the player
	void Update () {
		
		//get game objects with tag player
		player = GameObject.FindGameObjectsWithTag( "Player" );
		
		foreach ( GameObject obj in player ) {
			Vector3 direction = obj.transform.position - this.transform.position; //player - enemy position
			float distance = direction.magnitude;
			if(distance < lineOfSight) {
				this.renderer.material.color = new Color(255,255,0);
			} else {
				this.renderer.material.color = new Color(255,255,255);
			}
		}
	}
}
