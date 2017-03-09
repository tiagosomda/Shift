using UnityEngine;
using System.Collections;

public class PickupPiece : MonoBehaviour {
	
	//Boolean that displays a message when set to true
	private bool displayMessage;
	
	// Use this for initialization
	void Start () {
		GameObject.Find ("Next Level").GetComponent<LevelGoalObject>().proceed = false;
	}
	
	// player interacts with this obj, then disappear
	void OnCollisionEnter(Collision kol) {
		GameObject.Find ("Next Level").GetComponent<LevelGoalObject>().proceed = true;
		displayMessage = true;
		StartCoroutine(displayMessagePickup());
		this.gameObject.SetActive(false);
	}
	
	//Display the ANA piece pickup saved message for 3 seconds
	IEnumerator displayMessagePickup(){
		yield return new WaitForSeconds(3);
		displayMessage = false;
	}
	
	//Display ANA piece pickup saved message after picking up the piece of ANA
	void OnGUI() {
		if(displayMessage == true){
			GUIStyle centeredStyle = GUI.skin.GetStyle("textArea");
			centeredStyle.font = (Font)Resources.Load ("Fonts/Arial", typeof(Font));
   			centeredStyle.alignment = TextAnchor.UpperCenter;
    		GUI.Label (new Rect (Screen.width/2 - 100, 20, 170, 20), "Piece of ANA Obtained", centeredStyle);
		}
	}
}
