using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* PathingAI - v1.3
 *    System designed to handle node-based pathing. Attach to
 *    object of pathing. Visualizes path using Gizmos.
 * 
 *    v1.1 ~ collision adjustment
 *    v1.2 ~ randomization fix
 *    v1.3 ~ triggerStart & function updates
 * 
 * CoScripts: Node
 * Scribes: Ryan Wood, Luka
 */
public class PathingAI : MonoBehaviour {
	
	// main components
	int index = 0;
	GameObject target = null;
	public List<GameObject> nodes = new List<GameObject>();
	public bool triggerStart = true;
	
	// node components
	float velocity = 0.0f, leapage = 0.0f, interval = 0.0f;
	bool jump = false, sleep = false, cycle = true;
	
	// feature control
	public Color hue = Color.black;
	public bool loop = true;
	[HideInInspector]
	public int iteration = 1;
	[HideInInspector]
	public bool reverse = false, randomPatrol = false;
	
	// initialization
	void Start() {
		if (randomPatrol) ShuffleNodes(0);
		this.transform.position = nodes[index].transform.position;
	}	
	
	void FixedUpdate() {
		if (triggerStart) {
			// delay synchronization
			if (sleep) {
				sleep = false;
				cycle = false;
				StartCoroutine(Sleeping());
			}
			
			// update cycle
			if (cycle) {
				if (jump) {		
					jump = false;
					this.GetComponent<Rigidbody>().AddForce(Vector3.up * leapage * Time.deltaTime, ForceMode.VelocityChange);
				}
				if (target != null) this.transform.LookAt(target.transform); // redundant checking (debug)
				this.transform.Translate(Vector3.forward * velocity * Time.deltaTime);
			}
		}
	}
	
	// delay pathing function
	IEnumerator Sleeping() {
		yield return new WaitForSeconds(interval);
		cycle = true;
	}
	
	public float Speed() { return velocity; }
	
	// node detection
	void OnTriggerEnter(Collider node) {
		if (node.gameObject.CompareTag("Node") && nodes.Contains(node.gameObject)
			&& node.gameObject.Equals(nodes[index])) {
			// grab node components
			velocity = nodes[index].GetComponent<Node>().velocity;
			jump = nodes[index].GetComponent<Node>().jump;
			leapage = nodes[index].GetComponent<Node>().leapage;
			sleep = nodes[index].GetComponent<Node>().sleep;
			interval = nodes[index].GetComponent<Node>().interval;
			// check pathing termination
			if (index == nodes.Count-1) {
				if (!loop) iteration--;
				if (loop || iteration>0) {
					index = 0;
					if (reverse) {
						nodes.Reverse();
						index = 1;
					}	
					target = nodes[index];
					this.transform.LookAt(target.transform);
					if (randomPatrol) ShuffleNodes(1);
				} else cycle = false;
			} else {
				index++;
				target = nodes[index];
				this.transform.LookAt(target.transform);
			}
		}
	}
	
	void OnCollisionEnter() {
		// reacquire pathing target 
		this.transform.LookAt(target.transform);
	}
	
	// randomization of nodes
	void ShuffleNodes(int mode) {
		List<GameObject> list = new List<GameObject>();
		list.AddRange(nodes);
		nodes.Clear();
		// check initialization 
		if (mode == 1) {
			list.Remove(target);
			nodes.Add(target);
		}
		for (int i=list.Count; i>0; i--) {
			int n = Random.Range(0, list.Count);
			GameObject obj = list[n];
			list.RemoveAt(n);
			nodes.Add(obj);
		}
	}
	
	// renders Gizmos to be used as visual aids
	void OnDrawGizmos() {
		for (int i=0; i<nodes.Count; i++) {
			Gizmos.color = Color.green;
			if (reverse) Gizmos.color = Color.yellow;
			if (randomPatrol) Gizmos.color = Color.red;
			// node of origin
			if (i == 0) {
				Gizmos.DrawWireCube(nodes[i].transform.position, Vector3.one);
				Gizmos.color = hue;
				Gizmos.DrawLine(nodes[i].transform.position, nodes[i+1].transform.position);
				Gizmos.DrawWireSphere(nodes[i].transform.position, .5f);
			// node of termination
			} else if (i == nodes.Count-1) {
				if ((loop || iteration>1) && !reverse) {
					Gizmos.color = hue;
					Gizmos.DrawLine(nodes[i].transform.position, nodes[0].transform.position);
				} else
					Gizmos.DrawWireCube(nodes[i].transform.position, Vector3.one);
				Gizmos.color = hue;
				Gizmos.DrawWireSphere(nodes[i].transform.position, .5f);
			// path nodes
			} else {	
				Gizmos.color = hue;
				Gizmos.DrawLine(nodes[i].transform.position, nodes[i+1].transform.position);
				Gizmos.DrawWireSphere(nodes[i].transform.position, .5f);
			}	
		}
	}
	
}
