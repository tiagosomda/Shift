using UnityEngine;
using System.Collections;

public class LevelGoalObject : MonoBehaviour {
	
	public string nextSceneName;
	
	public bool proceed = true;
	
	//Boolean that displays a message when set to true
	private bool displayMessage;
	
	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.name.Equals("Player") && proceed == true){
			
			Application.LoadLevel(nextSceneName);
			
		}else if(collision.gameObject.name.Equals("Player") && proceed == false){
			
			StartCoroutine(displayProceedMessage());
			displayMessage = true;
			
		}
		
	}
	
	//Display the proceed message for 3 seconds
	IEnumerator displayProceedMessage(){
		yield return new WaitForSeconds(3);
		displayMessage = false;
	}
	
	//Display proceed message
	void OnGUI() {
		if(displayMessage == true){
			GUIStyle centeredStyle = GUI.skin.GetStyle("textArea");
			centeredStyle.font = (Font)Resources.Load ("Fonts/Arial", typeof(Font));
   			centeredStyle.alignment = TextAnchor.UpperCenter;
    		GUI.Label (new Rect (Screen.width/2 - 100, 20, 250, 20), "Unable to Proceed Until Piece is Found", centeredStyle);
		}
	}

}
