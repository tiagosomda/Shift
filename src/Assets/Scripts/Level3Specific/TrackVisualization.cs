using UnityEngine;
using System.Collections;

/* TrackVisualization - v0.8
 *    Creates a railway system using line renderers.
 *    Builds path using nodes and recursion. Attach
 *    to an origin node.
 * 
 * CoScripts: cNode
 * Scribes: Ryan Wood
 */
public class TrackVisualization : MonoBehaviour {
	
	public float vOffset = 1.0f, hOffset = 1.0f;
	int index = 0;
	public Material trackMat = null;
	
	void Start() {
		SketchTrack(this.gameObject, 0);
		if (trackMat == null) 
			trackMat = (Material) Resources.Load("Fairy Dust", typeof(Material));
	}
	
	void SketchTrack(GameObject root, int mode) {
		GameObject lObj = new GameObject("Track");
		GameObject rObj = new GameObject("Track");
		LineRenderer lEdge = lObj.AddComponent<LineRenderer>();
		LineRenderer rEdge = rObj.AddComponent<LineRenderer>();
		
		index = 0;
		lEdge.material = trackMat;
		lEdge.SetWidth(.25f, .25f);
		lEdge.SetVertexCount(index+1);
		rEdge.material = trackMat;
		rEdge.SetWidth(.25f, .25f);
		rEdge.SetVertexCount(index+1);
		
		Vector3 lok = root.transform.position;
		lEdge.SetPosition(index, new Vector3(lok.x - hOffset, lok.y - vOffset, lok.z - hOffset));
		rEdge.SetPosition(index, new Vector3(lok.x + hOffset, lok.y - vOffset, lok.z + hOffset));
		
		if (mode == 0) {
			GameObject next = root.GetComponent<cNode>().oNext;
			if (next != null) NodalSketch(next, ++index, lEdge, rEdge);
			if (root.GetComponent<cNode>().divergence) SketchTrack(root, 1);
		} else {
			GameObject next = root.GetComponent<cNode>().aNext;
			if (next != null) NodalSketch(next, ++index, lEdge, rEdge);
		}
	}
	
	void NodalSketch(GameObject node, int index, LineRenderer lEdge, LineRenderer rEdge) {
		lEdge.SetVertexCount(index+1);
		rEdge.SetVertexCount(index+1);
		
		Vector3 lok = node.transform.position;
		lEdge.SetPosition(index, new Vector3(lok.x - hOffset, lok.y - vOffset, lok.z - hOffset));
		rEdge.SetPosition(index, new Vector3(lok.x + hOffset, lok.y - vOffset, lok.z + hOffset));
		
		GameObject next = node.GetComponent<cNode>().oNext;
		if (next != null) NodalSketch(next, ++index, lEdge, rEdge);
		if (node.GetComponent<cNode>().divergence) SketchTrack(node, 1);
	}
	
}
