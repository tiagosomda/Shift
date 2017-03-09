using UnityEngine;
using System.Collections;

public class MinecartOnly : MonoBehaviour {
	
	public string currentSceneName;
	
	void OnTriggerEnter( Collider other ) {
		if(other.gameObject.name == "Player"){
			Application.LoadLevel(currentSceneName);
		}
	}
}
