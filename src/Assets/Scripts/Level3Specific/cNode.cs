using UnityEngine;
using System.Collections;

/* cNode - v0.8
 *    Container attached to each node game-object used in
 *    the pathing system handled by PathControlAI.
 * 
 * CoScripts: PathControlAI
 * Scribes: Ryan Wood
 */
public class cNode : MonoBehaviour {
	
	// original target
	public GameObject oNext = null;
	// last target
	public GameObject pre = null;
	
	// split path? : alternative target
	public bool divergence = false;
	public GameObject aNext = null;
	
}
