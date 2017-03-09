using UnityEngine;
using System.Collections;

public class guiLabel : MonoBehaviour {

	private bool screenRelative = false;
	private float x = 0,y = 0;
	public float width,height;
	public Rect rect;
	public string content;
	

	// Use this for initialization
	void Start () {
		
		if(screenRelative) {
			rect = new Rect(Screen.width*x,Screen.height*y,width,height);
		} else {
			rect = new Rect(gameObject.transform.position.x,gameObject.transform.position.y,width,height);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		if(screenRelative) {
			rect.Set(Screen.width*x,Screen.height*y,width,height);
		} else {
			rect.Set(gameObject.transform.position.x,gameObject.transform.position.y,width,height);
		}
	}
	
	public void draw() {
	//	int temp = GUI.skin.label.fontSize;
	//	GUI.skin.label.fontSize = 170;
	//	GUI.Label(rect,content);
	//	GUI.skin.label.fontSize = temp;
	}
}
