using UnityEngine;
using System.Collections;

public class MoveImage : MonoBehaviour {
	public float endXLocation, endYLocation,delay, time;

	
	void Start () {
		iTween.MoveTo(gameObject, iTween.Hash("x", endXLocation,"y",endYLocation, "easeType", "linear", "loopType", "none", "delay", delay, "time", time));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
