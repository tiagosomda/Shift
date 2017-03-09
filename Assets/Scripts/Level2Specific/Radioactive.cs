using UnityEngine;
using System.Collections;

public class Radioactive : MonoBehaviour {
	
	private float enterTime;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
    void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.name.Equals("Player")){	
			enterTime=Time.time;
		}
	}
	
    void OnTriggerStay(Collider collision) {
		if (collision.gameObject.name.Equals("Player")){	
			if (Time.time-enterTime >2){
				Debug.Log ("injured");
				collision.gameObject.SendMessage("OnInjure", SendMessageOptions.DontRequireReceiver);
				enterTime=Time.time;
			}
		}
		
	}
}
