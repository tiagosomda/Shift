using UnityEngine;
using System.Collections;

public class TimeMaterials : MonoBehaviour {

	public Material pastMat;
	public Material futureMat;
	
	
	public void setMat( bool past ){
		if ( past ){
			gameObject.renderer.material = pastMat;
		} else {
			gameObject.renderer.material = futureMat;
		}
	}
	
}
