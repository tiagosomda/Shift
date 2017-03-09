/// <summary>
/// Modified code from:
/// http://wiki.unity3d.com/index.php/AutoType
/// </summary>
using UnityEngine;
using System.Collections;

public class TypeWriter2 : MonoBehaviour {
	
	public bool screenSize;
	private Rect rect;
	public float width,height;

	
	public AudioClip sound;
	public float letterPause = 0.2f;
	public float messagePause = 0.5f;

	public bool holdLastMessage = false;

	public string[] messages;
	private string currentMessage;
	private int numbMessages;
	public bool addNewLine = false;
	public float verticalOffSet = 0.5f;

	void Start () {
		if (screenSize)
				width = Screen.width;
		rect = new Rect(gameObject.transform.position.x,gameObject.transform.position.y,width,height);

		//Auto-magically sets the vertical offset
		//GUIStyle style = new GUIStyle();
		//Vector2 size = style.CalcSize(new GUIContent(messages[0]));
		//verticalOffSet = -size.y;
		
		//TODO: Function that devides message into multiple lines depending on the size of screen (or width set)
		//TODO: Specify time to write all letters and set letterpause to appropriate value
		currentMessage = "";
		numbMessages = messages.Length;
		StartCoroutine(TypeText ());
	}
	
	void Update() {
		rect.Set(gameObject.transform.position.x,gameObject.transform.position.y,width,height);

	}
	
	IEnumerator TypeText () {
		int count = 0;
		foreach(string message in messages) {
			count++;
			foreach (char letter in message.ToCharArray()) {
				currentMessage += letter;
				if (sound)
					GetComponent<AudioSource>().PlayOneShot (sound);
					yield return 0;
				yield return new WaitForSeconds (letterPause);
			}
			yield return new WaitForSeconds (messagePause);
			
			
			
			if(addNewLine) {
				GetComponent<GUIText>().text += "\n";
				iTween.MoveBy(gameObject,  iTween.Hash("y", verticalOffSet, "easeType", "easeOutCirc", "loopType", "none", "delay", 0, "time", messagePause));
				//gameObject.transform.position.y += verticalOffSet;
				//gameObject.transform.position.Set (gameObject.transform.position.x + verticalOffSet, gameObject.transform.position.y,gameObject.transform.position.z);
			} else {
				if(!holdLastMessage)
					currentMessage = "";
				else if (count < numbMessages)
					currentMessage = "";
			}
		}	  
	}
	
	IEnumerator test() {
		int count = 0;
		foreach(string message in messages) {
			foreach (char letter in message.ToCharArray()) {
				GetComponent<GUIText>().text += letter;
				if (sound)
					GetComponent<AudioSource>().PlayOneShot (sound);
					yield return 0;
				yield return new WaitForSeconds (letterPause);
			}
			yield return new WaitForSeconds (messagePause);
			
			if(addNewLine) {
				currentMessage += "\n";
				iTween.MoveBy(gameObject,  iTween.Hash("y", verticalOffSet, "easeType", "easeOutCirc", "loopType", "none", "delay", 0, "time", messagePause));
				//gameObject.transform.position.y += verticalOffSet;
				//gameObject.transform.position.Set (gameObject.transform.position.x + verticalOffSet, gameObject.transform.position.y,gameObject.transform.position.z);
			} else {
				if(!holdLastMessage)
					currentMessage = "";
				else if (count < numbMessages)
					currentMessage = "";
			}
			
		}
	}
	
	void OnGUI() {
		test ();
		GUI.Label(rect, currentMessage);
		
		
	}
}