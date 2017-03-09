using UnityEngine;
using System.Collections;

public class Level1Alternator : MonoBehaviour {
	
	public string currentSceneName;
	public string nextSceneName;
	public int enableMenu = -1;
	
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
			GUI.Box(new Rect(10,10,140,180), "Level 1");
			
			// Swaps to first checkpoint
			if(GUI.Button(new Rect(20,40,120,20), "Checkpoint 1")) {
				GameObject.Find("Checkpoints System").gameObject.GetComponent<CheckpointSystem>().handleCollision(GameObject.Find("Checkpoints System").gameObject.transform.FindChild("Room 1 Checkpoint").gameObject, this.gameObject.transform.rotation);
				gameObject.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
			}
			
			
			// Swaps to second checkpoint
			if(GUI.Button(new Rect(20,70,120,20), "Checkpoint 2")) {
				GameObject.Find("Checkpoints System").gameObject.GetComponent<CheckpointSystem>().handleCollision(GameObject.Find("Checkpoints System").gameObject.transform.FindChild("Room 2 Checkpoint").gameObject, this.gameObject.transform.rotation);
				gameObject.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
			}
			
			// Swaps to third checkpoint
			if(GUI.Button(new Rect(20,100,120,20), "Checkpoint 3")) {
				GameObject.Find("Checkpoints System").gameObject.GetComponent<CheckpointSystem>().handleCollision(GameObject.Find("Checkpoints System").gameObject.transform.FindChild("Room 3 Checkpoint").gameObject, this.gameObject.transform.rotation);
				gameObject.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
			}
			
			
			// Swaps to second part
			if(GUI.Button(new Rect(20,130,120,20), "Load Next Level")) {
				Application.LoadLevel(nextSceneName);
			}
			
			// Reset level
			if(GUI.Button(new Rect(20,160,120,20), "Reset Level")) {
				Application.LoadLevel(currentSceneName);
			}
			
		}
	}
	
}
