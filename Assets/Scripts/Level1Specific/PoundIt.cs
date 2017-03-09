using UnityEngine;
using System.Collections;

/* PoundIt - v0.8
 *    Paired with another "pounder." Must specify
 *    direction of pounding.
 * 
 * CoScripts: NULL
 * Scribes: Ryan Wood
 */
public class PoundIt : MonoBehaviour {
	
	// 0-N, 1-E, 2-S, 3-W, 4-F, 5-B
	public int direction = 0;
	public GameObject buddy = null;
	bool pounding = false;
	bool blockade = false;
	GameObject player = null;
	
	// initialization of needed components
	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	public bool IsPound() {return pounding;}
	
	// pounding?
	void Update() {
		if (pounding && buddy.GetComponent<PoundIt>().IsPound()){
			player.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
		}
	}
	
	// After pounding against the blockade object, stops the movement of the smasher and of the blockade
	void FixedUpdate() {
		if (blockade && buddy.GetComponent<PoundIt>().blockade == true){
			this.GetComponent<PathingAI>().triggerStart = false;
			//GameObject.Find("SmasherBlockade").gameObject.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			GameObject.Find("SmasherBlockade").gameObject.rigidbody.mass = 10000;
		}	
	}
	
	// player interacts with this obj, then pound
	void OnCollisionEnter(Collision kol) {

		if (kol.contacts.Length > 0)
			switch (direction) {
			case 0:
				// NORTH
				if ((Vector3.Dot(kol.contacts[0].normal, Vector3.down)) > .5){
					if(kol.collider.Equals(player.collider)){
						pounding = true;
					}
					if(kol.gameObject.name == "SmasherBlockade"){
						blockade = true;
					}
				}
				break;
			case 1:
				// EAST
				if ((Vector3.Dot(kol.contacts[0].normal, Vector3.left)) > .5){
					if(kol.collider.Equals(player.collider)){
						pounding = true;
					}
					if(kol.gameObject.name == "SmasherBlockade"){
						blockade = true;
					}
				}
				break;
			case 2:
				// SOUTH
				if ((Vector3.Dot(kol.contacts[0].normal, Vector3.up)) > .5){
					if(kol.collider.Equals(player.collider)){
						pounding = true;
					}
					if(kol.gameObject.name == "SmasherBlockade"){
						blockade = true;
					}
				}
				break;
			case 3:
				// WEST
				if ((Vector3.Dot(kol.contacts[0].normal, Vector3.right)) > .5){
					if(kol.collider.Equals(player.collider)){
						pounding = true;
					}
					if(kol.gameObject.name == "SmasherBlockade"){
						blockade = true;
					}
				}
				break;
			case 4:
				// FORWARDS
				if ((Vector3.Dot(kol.contacts[0].normal, Vector3.forward)) > .5){
					if(kol.collider.Equals(player.collider)){
						pounding = true;
					}
					if(kol.gameObject.name == "SmasherBlockade"){
						blockade = true;
					}
				}
				break;
			case 5:
				// BACKWARDS
				if ((Vector3.Dot(kol.contacts[0].normal, Vector3.back)) > .5){
					if(kol.collider.Equals(player.collider)){
						pounding = true;
					}
					if(kol.gameObject.name == "SmasherBlockade"){
						blockade = true;
					}
				}
				break;
			default:
				// do nothing
				break;
			}
	}
	
	// revert
	void OnCollisionExit(Collision kol) {
		if (kol.collider.Equals(player.collider)){
			pounding = false;
		}
		
		if (kol.gameObject.name == "SmasherBlockade"){
			this.GetComponent<PathingAI>().triggerStart = true;
		}
	}
	
}
