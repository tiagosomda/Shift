/// <summary>
/// Modified code from:
/// http://wiki.unity3d.com/index.php/AutoType
/// </summary>
using UnityEngine;
using System.Collections;

public class TypeWriter : MonoBehaviour {
	public AudioClip sound;
	public float letterPause = 0.2f;
	public float messagePause = 0.5f;

	public bool holdLastMessage = false;
	public bool addNewLine = false;
	public float verticalOffSet = 0.5f;
	public string[] messages;
	private int numbMessages;

	void OnGUI() {
			
	}
	// Use this for initialization
	void Start () {		
		guiText.text = "";
		numbMessages = messages.Length;
		StartCoroutine(TypeText ());
	}
	
	IEnumerator TypeText () {
		int count = 0;
		foreach(string message in messages) {
			count++;
			foreach (char letter in message.ToCharArray()) {
				guiText.text += letter;
				if (sound)
					audio.PlayOneShot (sound);
					yield return 0;
				yield return new WaitForSeconds (letterPause);
			}
			yield return new WaitForSeconds (messagePause);
			
			if(addNewLine) {
				guiText.text += "\n";
				if (count < numbMessages)
					iTween.MoveBy(gameObject,  iTween.Hash("y", verticalOffSet, "easeType", "easeOutCirc", "loopType", "none", "delay", 0, "time", messagePause+1));
			} else {
				if(!holdLastMessage)
					guiText.text = "";
				else if (count < numbMessages)
					guiText.text = "";
			}
		}	  
	}
}
