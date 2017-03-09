using UnityEngine;
using System.Collections;

/// <summary>
/// This script should be attached to an object with that 
/// can activate the OnTriggerEnter function.
/// 	Controller : The script that handles the tutorial windo
/// 	Message : The message to be displayed on the tutorial windo
/// 	seeOnce : False - Will be active on only one collision
/// 			  True - Will always pop out the tutorial window
/// </summary>
public class popMessageTrigger : MonoBehaviour {

	public TutorialSystem controller;
	public string message = "no message";
	public bool seeOnce = true;
	
	private bool wasSeen = false;
	private int messageIndex;
	
	void OnTriggerEnter(Collider collided) {
		if(collided.gameObject.CompareTag("Player")){
			if(!wasSeen) {
				wasSeen = true;
				messageIndex = controller.addMessage(message);
				controller.popUp();
			} else if (!seeOnce){
				controller.popUpMessageIndex(messageIndex);
			}
		}
	}
	
}
