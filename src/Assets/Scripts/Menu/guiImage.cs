using UnityEngine;
using System.Collections;

public class guiImage : MonoBehaviour {

	public bool screenRelative = false;
	public float x,y,width,height;
	public Rect rect;
	public GUITexture image;
	

	// Use this for initialization
	void Start () {
		
		if(screenRelative) {
			rect = new Rect(Screen.width*x,Screen.height*y,width,height);
		} else {
			rect = new Rect(gameObject.transform.position.x,gameObject.transform.position.y,width,height);
			//width = image.width;
			//height = image.height;
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
		//GUI.DrawTexture(rect,image);	
	}
}
