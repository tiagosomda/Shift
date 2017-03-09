using UnityEngine;
using System.Collections;

/* PathControlAI - v0.8
 *    System designed to handle node-based pathing. Attach to
 *    object of pathing. Visualizes path using Gizmos. Control
 *    is regulated to the player.
 * 
 * CoScripts: cNode
 * Scribes: Ryan Wood
 */
public class PathControlAI : MonoBehaviour {

	// main components
	public GameObject origin = null;
	GameObject target = null, last = null;
	[HideInInspector]
	public bool cycle = false;			// active?
	
	// options
	public float velocity = 10.0f;
	bool oPath = true;
	public Color hue = Color.white;
	
	void Start() {
		this.transform.position = origin.transform.position;
	}
	
	void Update() {
		if (cycle) {
			// path checking
			if (Input.GetKey(KeyCode.A))
				oPath = true;
			if (Input.GetKey(KeyCode.D))
				oPath = false;
		
			// forward checking
			if (Input.GetKey(KeyCode.W) && target != null) {
				this.transform.LookAt(target.transform);
				this.transform.Translate(Vector3.forward * velocity * Time.deltaTime);
			}
			// backward checking
			if (Input.GetKey(KeyCode.S) && last != null) {
				this.transform.LookAt(last.transform);
				this.transform.Translate(Vector3.forward * velocity * Time.deltaTime);
			}
		}
	}
	
	// node detection
	void OnTriggerEnter(Collider node) {
		if (node.gameObject.CompareTag("Node")) {
			// grab node components
			target = node.GetComponent<cNode>().oNext;
			last = node.GetComponent<cNode>().pre;
			if (node.GetComponent<cNode>().divergence)
				if (!oPath) target = node.GetComponent<cNode>().aNext;
		}
	}
	
	// node detection
	void OnTriggerExit(Collider node) {
		if (node.gameObject.CompareTag("Node") && Input.GetKey(KeyCode.W)) {
			// grab node components
			target = node.GetComponent<cNode>().oNext;
			last = node.gameObject;
			if (node.GetComponent<cNode>().divergence)
				if (!oPath) target = node.GetComponent<cNode>().aNext;
		}
		if (node.gameObject.CompareTag("Node") && Input.GetKey(KeyCode.S)) {
			// grab node components
			target = node.gameObject;
			last = node.GetComponent<cNode>().pre;
		}
	}
	
	// renders Gizmos to be used as visual aids
	void OnDrawGizmos() {
		Gizmos.color = hue;
		if (origin != null) Draw(origin);
	}
	
	// recursive helper function
	void Draw(GameObject node) {
		Gizmos.DrawWireSphere(node.transform.position, .5f);
		GameObject next = node.GetComponent<cNode>().oNext;
		if (next != null) {
			Gizmos.DrawLine(node.transform.position, next.transform.position);
			Draw(next);
		}
		if (node.GetComponent<cNode>().divergence) {
			GameObject alt = node.GetComponent<cNode>().aNext;
			if (alt != null) {
				Gizmos.DrawLine(node.transform.position, alt.transform.position);
				Draw(alt);
			}
		}
	}
	
}
