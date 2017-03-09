using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Tutorial system.
/// Attach this script to one GameObject in the scene
/// skin : Attach Scripts/TutorialSystem/TutorialSkin to this variable
/// collapseTime : the amount of time it takes to expand/collapse the window
/// key : The key that collapses and expands the tutorial window
/// </summary>
public class TutorialSystem : MonoBehaviour {
	//Public
	public GUISkin skin;
	public float collapseTime;
	//Temporally Private
	private List<string> messages;
	private Rect window;
	private Vector2 insidePadding;
	private Vector2 outsidePadding;
	private KeyCode key = KeyCode.Tab;
	//Private
	private float rowHeight,colWidth;
	private int numRows,numCols;
	private int currentMax, current;
	private float popRange;
	private Vector2 closedWinPos,openWinPos;
	private Rect box,collapseBtn,firstBtn,secondBtn,thirdBtn,label, labelBox;
	private string[] collapseBtnText = {"Collapse","Open"};
	private int state = 1;
	
	// Use this for initialization
	void Start () {
		//Temporary Settings until resizing bugs are fixed
		insidePadding.x = insidePadding. y = 10;
		outsidePadding.x = outsidePadding.y = 10;
		window.width = 400;
		window.height = 200;
		
		//Setting up variables based on screen resolution
		numRows = 10;
		numCols = 3;
		currentMax = -1;
		current = -1;
		rowHeight = window.height/numRows;
		colWidth = window.width/numCols;
		popRange = Screen.height - rowHeight - window.y;
		openWinPos.Set(Screen.width - window.width - outsidePadding.x,Screen.height-window.height-outsidePadding.y);
		closedWinPos.Set(Screen.width - window.width - outsidePadding.x,Screen.height-rowHeight);
		collapseBtn = new Rect(0,0,window.width,rowHeight);
		box = new Rect(0,0,window.width,window.height);
		firstBtn = new Rect(insidePadding.x,rowHeight,colWidth-insidePadding.x,rowHeight);
		secondBtn = new Rect(colWidth,rowHeight,colWidth,rowHeight);
		thirdBtn = new Rect(colWidth*2+insidePadding.x,rowHeight,colWidth - insidePadding.x*2,rowHeight);
		labelBox = new Rect(insidePadding.x,insidePadding.y+rowHeight*2,window.width-insidePadding.x*2,window.height-rowHeight*2-insidePadding.y*2);
		label = new Rect(insidePadding.x*2,insidePadding.y*2+rowHeight*2,window.width-insidePadding.x*5,window.height-rowHeight*2-insidePadding.y*2);
	
		
		//Setting up window initial position and state
		state = 1;
		window.x = closedWinPos.x;
		window.y = closedWinPos.y;
		messages = new List<string>();
	}
	
	void Update() {
		if (Input.GetKeyDown( key )){
			StartCoroutine(moveWindow());	
		}
	}
	
	void OnGUI() {
		if (skin != null) {GUI.skin = skin;}

		GUI.BeginGroup(window);
			GUI.Box (box, "");
			if(GUI.Button(collapseBtn, collapseBtnText[state] + " (" + key.ToString() + ")")){
				StartCoroutine(moveWindow());
			}
			if(GUI.Button(firstBtn, "previous")) {
				if (current > 0) {
					current--;
				}	
			}
			GUI.Label(secondBtn, (current+1)+"/"+(currentMax+1));
			if(GUI.Button(thirdBtn, "next")) {
				if (current < currentMax) {
					current++;
				}
			}
			GUI.Box(labelBox,"");
			if (currentMax >= 0)
				GUI.Label(label, (messages[current]));		
		GUI.EndGroup();
	}
	//Adds message without poping up the tutorial window
	//Returns index of added message
	public int addMessage(string msg) {
		messages.Add(msg);
		current++;
		currentMax++;
		return currentMax;
	}
	//Adds message and pops up the tutorial window
	//Returns index of added message
	public int popUpNewMessage(string msg) {
		messages.Add(msg);
		current = currentMax++;
		popUp();
		return currentMax;
	}
	
	//Pops up the tutorial window
	//and sets the message displayed
	public void popUpMessageIndex(int messageIndex) {
		current = messageIndex;
		popUp();
	}
	
	//Pops up window - helper function
	public void popUp() {
		if (window.y >= closedWinPos.y) {
			StartCoroutine(moveWindow());
			state = 1;
		}
	}
	
	//Smoothly moves the window based on the collpaseTime variable
	IEnumerator moveWindow() {
		if (window.y == closedWinPos.y) { //if closed - open
			while(window.y != openWinPos.y){
				window.y -= (popRange/collapseTime)*Time.deltaTime;
				if(window.y < openWinPos.y) 
						window.y = openWinPos.y;
				yield return new WaitForSeconds (0.01f);
			}
		} else if (window.y == openWinPos.y) { // else if open - close
			while(window.y != closedWinPos.y){
				window.y += (popRange/collapseTime)*Time.deltaTime;
				if(window.y > closedWinPos.y) 
					window.y = closedWinPos.y;
				yield return new WaitForSeconds (0.01f);
			}
		}
		
		state = (++state)%2;
		yield return new WaitForSeconds (0.01f);
	}
}
