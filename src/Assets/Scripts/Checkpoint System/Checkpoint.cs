using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	
	//Creates a CheckpointSystem object
	private CheckpointSystem checkpointSystem;
	
	//Boolean that displays a checkpoint saved message when set to true
	private bool displayMessage;
	
	// Audiosource 
	AudioSource checkPointAudio;
	
	void Start () {
		checkpointSystem= (CheckpointSystem)this.transform.parent.gameObject.GetComponent("CheckpointSystem");
		checkPointAudio = gameObject.AddComponent< AudioSource >();
		checkPointAudio.clip = ( AudioClip ) Resources.Load( "CheckPointSound" );
		checkPointAudio.loop = false;
	}
	
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider collision){
		
		if(checkpointSystem.currentCheckpoint != this.gameObject && this.gameObject.name != "Starting Checkpoint"){
			checkpointSystem.handleCollision(this.gameObject,collision.transform.rotation);
			StartCoroutine(displayCheckpointSaved());
			displayMessage = true;
			checkPointAudio.Play();
		}
		
	}
	

	
	//Display the checkpoint saved message for 3 seconds
	IEnumerator displayCheckpointSaved(){
		yield return new WaitForSeconds(3);
		displayMessage = false;
	}
	
	//Display checkpoint saved message after saving progress to new portal
	void OnGUI() {
		if(displayMessage == true){
			GUIStyle centeredStyle = GUI.skin.GetStyle("textArea");
			centeredStyle.font = (Font)Resources.Load ("Fonts/Arial", typeof(Font));
   			centeredStyle.alignment = TextAnchor.UpperCenter;
    		GUI.Label (new Rect (Screen.width/2 - 100, 20, 170, 20), "Progress Saved", centeredStyle);
		}
	}
	
}
