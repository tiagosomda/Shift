using UnityEngine;
using System.Collections;

public class CollapsibleFloor : MonoBehaviour {

	// player interacts with this obj, then disappear
	void OnCollisionEnter(Collision kol) {
		this.gameObject.GetComponent<Renderer>().enabled = false;
		this.gameObject.GetComponent<Collider>().enabled = false;
	}
	
}
