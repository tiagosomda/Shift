using UnityEngine;
using System.Collections;

/* Node - v1.1
 *    Container attached to each node game-object used in
 *    the pathing system handled by PathingAI.
 * 
 *    v1.1 ~ minor tweaks
 * 
 * CoScripts: PathingAI
 * Scribes: Ryan Wood
 */
public class Node : MonoBehaviour {
	
	// affects forward vector
	public float velocity = 10.0f;
	
	// affects upward vector
	public bool jump = false;
	public float leapage = 220.0f;
	
	// affects delay in pathing behavior
	public bool sleep = false;
	public float interval = 1.0f;
	
}
