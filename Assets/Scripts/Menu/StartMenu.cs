using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	
	public GUISkin skin;
	public float width,height;
	
	private Page currentPage;
	public enum Page { None, Start, Pause, Options, Credits, Demos, GameStory }
	
	int  toolbarInt = 0;
	string[]  toolbarstrings =  {"Audio","Graphics"};
	
	public bool cameraFlight = false;
	public GameObject mainCamera;

	// Use this for initialization
	void Start () {
		currentPage = Page.Start;
		cameraFlightPath();
		iTween.MoveTo(gameObject, iTween.Hash("x", Screen.width*.05, "easeType", "easeOutCirc", "loopType", "none", "delay", 10+0.1, "time", 3));//10.1
	}
	void cameraFlightPath() {
		iTween.MoveTo(mainCamera, iTween.Hash("x", -8.2,"y", 4.5,"z", -28, "easeType", "linear", "loopType", "none", "delay", 2, "time", 1.5));
		iTween.RotateTo(mainCamera, iTween.Hash("x", 353.4,"y", 38.7,"z", 346, "easeType", "linear", "loopType", "none", "delay", 2, "time", 1.5));
		//iTween.MoveTo(mainCamera, iTween.Hash("name", "menu", "x", -2.9,"y", 4.3,"z", -15, "easeType", "easeOutSine", "loopType", "none", "delay", 5.5, "speed", 4));
		
		
		iTween.MoveTo(mainCamera, iTween.Hash("x", 9.63,"y", 7.8,"z", -5.74, "easeType", "linear", "loopType", "none", "delay", 3.1, "time", 4));
		iTween.RotateTo(mainCamera, iTween.Hash("x", 12.69,"y", 341.4,"z", 29, "easeType", "linear", "loopType", "none", "delay", 3.1, "time", 4));
		
		
		//iTween.MoveTo(mainCamera, iTween.Hash("name", "menu", "x", 2.4,"y", 4.4,"z", 7.3, "easeType", "linear", "loopType", "none", "delay", 9.1, "time", 2));
		//iTween.RotateTo(mainCamera, iTween.Hash("x", 12.69,"y", 330,"z", -3.3, "easeType", "linear", "loopType", "none", "delay", 9.1, "time", 2));
		
		
		iTween.MoveTo(mainCamera, iTween.Hash("x", -16,"y", 3,"z", 11, "easeType", "linear", "loopType", "none", "delay", 7.1, "time", 3));
		iTween.RotateTo(mainCamera, iTween.Hash("x", 350,"y", 35,"z", -8.7, "easeType", "linear", "loopType", "none", "delay", 7.1, "time", 3));
	}
	
	// Update is called once per frame
	void Update () {}
	
	void OnGUI() {
		if (skin != null){GUI.skin = skin;}
		
		
		GUILayout.BeginArea (new Rect(gameObject.transform.position.x,gameObject.transform.position.y,width,height));
			GUILayout.BeginVertical();
			//DrawTitle
			GUILayout.Label("SHIFT", "title");
			//DrawMenuChoices
			switch(currentPage) {
				case Page.Start: showStartMenu(); break;
				case Page.Options: showOptions(); break;
				case Page.Credits: showCredit(); break;
			}
			GUILayout.EndVertical();
		GUILayout.EndArea();
	}
	
	void showStartMenu() {
		if (GUILayout.Button("Start Game")){currentPage = Page.None; Application.LoadLevel(3);}
		if (GUILayout.Button("Options")){currentPage = Page.Options;}
		if (GUILayout.Button("Credits")){currentPage = Page.Credits;}
		
	}
	
	void showOptions() {
		if (GUILayout.Button("Back")) { currentPage = Page.Start;}
		toolbarInt = GUILayout.Toolbar (toolbarInt, toolbarstrings);
	    switch (toolbarInt) {
	       case 0: VolumeControl(); break;
	       case 1: Qualities(); QualityControl(); break;
	      // case 2: StatControl(); break; // TODO
	    }
	}
	
	void showCredit() {
		if (GUILayout.Button("Back")) { currentPage = Page.Start;}
		GUILayout.Button("Team Leader: Tiago Damasceno");
		GUILayout.Button("AI Lead: Luka Hankewycz");
		GUILayout.Button("AI Lead: Ryan Wood");
		GUILayout.Button("AI Lead: Zach Nall");
		GUILayout.Button("Core Mechanics Lead: Ryan Gross");
		GUILayout.Button("Core Mechanics Lead: Rachel Lloyd");
		GUILayout.Button("Art Designer: Ross Emery");
		GUILayout.Button("Art Designer: Leo Henault");	
	}
	
	
	
	void VolumeControl() {
	    GUILayout.Label("Volume");
	    AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0, 1);
	}
	
	void Qualities() {
	    switch (QualitySettings.GetQualityLevel()) 
		{
	        case 0:
	        	GUILayout.Label("Fastest");
	        	break;
	        case 1:
	        	GUILayout.Label("Fast");
	        	break;
	        case 2:
		        GUILayout.Label("Simple");
		        break;
	        case 3:
		        GUILayout.Label("Good");
		        break;
	        case 4:
		        GUILayout.Label("Beautiful");
		        break;
	        case 5:
	        	GUILayout.Label("Fantastic");
	        	break;
	    }
	}
 
	void QualityControl() {
	    GUILayout.BeginHorizontal();
	    if (GUILayout.Button("Decrease")) {
	        QualitySettings.DecreaseLevel();
	    }
	    if (GUILayout.Button("Increase")) {
	        QualitySettings.IncreaseLevel();
	    }
	    GUILayout.EndHorizontal();
	}
}
