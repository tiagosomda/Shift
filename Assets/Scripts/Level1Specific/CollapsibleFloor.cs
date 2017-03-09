using UnityEngine;
using System.Collections;

public class CollapsibleFloor : MonoBehaviour {

	// player interacts with this obj, then disappear
	void OnCollisionEnter(Collision kol) {
		this.gameObject.renderer.enabled = false;
		this.gameObject.collider.enabled = false;
	}
	
}
