using UnityEngine;
using System.Collections;

public class CheckpointAlternatorPart1 : MonoBehaviour {
	
	public string currentSceneName;
	public string previousSceneName;
	public string nextSceneName;
	public int enableMenu = -1;
	
	//Kill the player immediately
	void Start(){
		gameObject.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
	}
	
	void Update(){
		
		if (Input.GetKeyDown("m")){
			enableMenu *= -1;	
		}
		
	}
	
	void OnGUI () {
		
		if(enableMenu == 1){
		
			// Make a background box
			GUI.Box(new Rect(10,10,140,240), "Level 3 - Part 1");
			
			// Swaps to first checkpoint
			if(GUI.Button(new Rect(20,40,120,20), "Checkpoint 1")) {
				GameObject.Find("Checkpoints System").gameObject.GetComponent<CheckpointSystem>().handleCollision(GameObject.Find("Checkpoints System").gameObject.transform.FindChild("Room 1 Checkpoint").gameObject, this.gameObject.transform.rotation);
				gameObject.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
			}
	
			// Swaps to second checkpoint
			if(GUI.Button(new Rect(20,70,120,20), "Checkpoint 2")) {
				GameObject.Find("Checkpoints System").gameObject.GetComponent<CheckpointSystem>().handleCollision(GameObject.Find("Checkpoints System").gameObject.transform.FindChild("Room 2 Part 1 Checkpoint").gameObject, this.gameObject.transform.rotation);
				gameObject.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
			}
			
			// Swaps to third checkpoint
			if(GUI.Button(new Rect(20,100,120,20), "Checkpoint 3")) {
				GameObject.Find("Checkpoints System").gameObject.GetComponent<CheckpointSystem>().handleCollision(GameObject.Find("Checkpoints System").gameObject.transform.FindChild("Room 2 Part 2 Checkpoint").gameObject, this.gameObject.transform.rotation);
				gameObject.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
			}
			
			// Swaps to fourth checkpoint
			if(GUI.Button(new Rect(20,130,120,20), "Checkpoint 4")) {
				GameObject.Find("Checkpoints System").gameObject.GetComponent<CheckpointSystem>().handleCollision(GameObject.Find("Checkpoints System").gameObject.transform.FindChild("Room 2 Part 3 Checkpoint").gameObject, this.gameObject.transform.rotation);
				gameObject.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
			}
			
			// Swaps to second part
			if(GUI.Button(new Rect(20,160,120,20), "Load Next Part")) {
				Application.LoadLevel(nextSceneName);
			}
			
			// Load Previous Level
			if(GUI.Button(new Rect(20,190,120,20), "Load Last Level")) {
				Application.LoadLevel(previousSceneName);
			}
			
			// Reset level
			if(GUI.Button(new Rect(20,220,120,20), "Reset Level")) {
				Application.LoadLevel(currentSceneName);
			}
		
		}
	}
	
}
