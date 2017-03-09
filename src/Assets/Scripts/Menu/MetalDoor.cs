using UnityEngine;
using System.Collections;

public class MetalDoor : MonoBehaviour {
	
	public GameObject LeftDoor, RightDoor, Center;
	// Use this for initialization
	void Start () {
		iTween.MoveTo(Center, iTween.Hash("x", 1.5, "easeType", "easeInOutSine", "loopType", "none", "delay", .5, "time", 3));
		iTween.MoveTo(LeftDoor, iTween.Hash("x", -0.5, "easeType", "easeInOutSine", "loopType", "none", "delay", .5, "time", 3));
		iTween.MoveTo(RightDoor, iTween.Hash("x", 1.5, "easeType", "easeInOutSine", "loopType", "none", "delay", .5, "time", 3, "oncomplete", "menu"));
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
