using UnityEngine;
using System.Collections;

/* KynForm - v0.8
 *    Allows the player to move with the object of
 *    this script.
 * 
 * CoScripts: PathingAI
 * Scribes: Ryan Wood
 */
public class KynForm : MonoBehaviour {
	
	GameObject player = null;
	bool following = false;
	float speed = 0.0f;
	
	// initialization of needed components
	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// following?
	void FixedUpdate() {
		if (following) {
			speed = gameObject.GetComponent<PathingAI>().Speed();
			player.transform.Translate(Vector3.forward * speed * Time.deltaTime, this.transform);
		}
	}
	
	// player interacts with this obj, then follow
	void OnCollisionEnter(Collision kol) {
		if (kol.collider.Equals(player.collider) && kol.contacts.Length > 0)
			if ((Vector3.Dot(kol.contacts[0].normal, Vector3.down)) > .5)
				following = true;
	}
	
	// revert
	void OnCollisionExit(Collision kol) {
		if (kol.collider.Equals(player.collider) && kol.contacts.Length > 0)
			if ((Vector3.Dot(kol.contacts[0].normal, Vector3.down)) > .5)
				following = false;
	}
	
}
