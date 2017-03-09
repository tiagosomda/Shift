using UnityEngine;
using System.Collections;

public class CheckpointAlternatorPart2 : MonoBehaviour {

	public string currentSceneName;
	public string lastSceneName;
	public string nextSceneName;
	public int enableMenu = -1;
	
	void Update(){
		
		if (Input.GetKeyDown("m")){
			enableMenu *= -1;	
		}
		
	}
	
	void OnGUI () {
		
		if(enableMenu == 1){
			// Make a background box
			GUI.Box(new Rect(10,80,140,120), "Level 3 - Part 2");
			
			// Swaps to first part
			if(GUI.Button(new Rect(20,110,120,20), "Load Last Part")) {
				Application.LoadLevel(lastSceneName);
			}
			
			// Swaps to second part
			if(GUI.Button(new Rect(20,140,120,20), "Load Next Level")) {
				Application.LoadLevel(nextSceneName);
			}
			
			// Reset level
			if(GUI.Button(new Rect(20,170,120,20), "Reset Level")) {
				Application.LoadLevel(currentSceneName);
			}
		}
	}
}
