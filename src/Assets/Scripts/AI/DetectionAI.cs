using UnityEngine;
using System.Collections;

/* DetectionAI - v1.0
 *    Determines the position of the player in relation
 *    to the game object.
 * 
 * CoScripts: NULL
 * Scribes: Ryan Wood, Zach
 */
public class DetectionAI : MonoBehaviour {
	
	public float detectionLoS = 10.0f, targetLoS = 5.0f;
	bool activeD = false, activeT = false;
	GameObject player = null;
	
	void Update() {
		player = GameObject.FindGameObjectWithTag("Player");
		
		// checks player proximity
		if (player != null) {
			Vector3 radius = player.transform.position - this.transform.position;
			float distance = radius.magnitude;
			if (distance < detectionLoS) activeD = true;
			else activeD = false;
			if (distance < targetLoS) activeT = true;
			else activeT = false;
		}	
	}
	
	public bool LoSD() { return activeD; }
	public bool LoST() { return activeT; }
	public GameObject User() { return player; }
	
}
