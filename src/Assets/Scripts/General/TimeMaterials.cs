using UnityEngine;
using System.Collections;

public class TimeMaterials : MonoBehaviour {

	public Material pastMat;
	public Material futureMat;
	
	
	public void setMat( bool past ){
		if ( past ){
			gameObject.GetComponent<Renderer>().material = pastMat;
		} else {
			gameObject.GetComponent<Renderer>().material = futureMat;
		}
	}
	
}
