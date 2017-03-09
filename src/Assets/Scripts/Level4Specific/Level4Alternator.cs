using UnityEngine;
using System.Collections;

public class Level4Alternator : MonoBehaviour {
	public string currentSceneName;
	public string previousSceneName;
	//public string nextSceneName;
	public int enableMenu = -1;
	
	//Kill the player immediately
	void Start() {
		gameObject.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
	}
	
	void Update(){
		
		if (Input.GetKeyDown("m")){
			enableMenu *= -1;	
		}
		
	}
	
	void OnGUI() {
		
		if(enableMenu == 1){
			// Make a background box
			GUI.Box(new Rect(10,10,140,150), "Level 4");
			
			// Swaps to first checkpoint
			if(GUI.Button(new Rect(20,40,120,20), "Checkpoint 1")) {
				GameObject.Find("CheckpointSystem").gameObject.GetComponent<CheckpointSystem>().handleCollision(GameObject.Find("CheckpointSystem").gameObject.transform.FindChild("001_Checkpoint").gameObject, this.gameObject.transform.rotation);
				gameObject.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
			}
	
			// Swaps to second checkpoint
			if(GUI.Button(new Rect(20,70,120,20), "Checkpoint 2")) {
				GameObject.Find("CheckpointSystem").gameObject.GetComponent<CheckpointSystem>().handleCollision(GameObject.Find("CheckpointSystem").gameObject.transform.FindChild("002_Checkpoint").gameObject, this.gameObject.transform.rotation);
				gameObject.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
			}
	
			// Load Previous Level
			if(GUI.Button(new Rect(20,100,120,20), "Load Last Level")) {
				Application.LoadLevel(previousSceneName);
			}
			
			// Reset level
			if(GUI.Button(new Rect(20,130,120,20), "Reset Level")) {
				Application.LoadLevel(currentSceneName);
			}
		}
	}
	
}
