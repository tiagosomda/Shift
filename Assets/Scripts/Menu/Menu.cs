using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	private guiRect menuBox;
	public GUISkin skin;
	
	public guiLabel title;
	public guiRect btnStartGame;
	public guiRect btnOptions;
	public guiRect btnCredits;
	
	public guiRect optionsToolbar;
	
	public GameObject mainCamera;

	
	public GameObject startMenu;
	public bool cameraFlight = false;
	//public GameObject optionsMenu;
	//public GameObject creditsMenu;
	
	public enum Page {None,Main,Options,Credits}
	private Page currentPage;
	
	int  toolbarInt = 0;
	string[]  toolbarstrings =  {"Audio","Graphics"};


	// Use this for initialization
	void Start () {
		currentPage = Page.Main;
		
		//START Camera Fly Motion
		if (cameraFlight){
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
		//END Camera Fly Motion
		
		iTween.MoveTo(title.gameObject, iTween.Hash("x", Screen.width*.05, "easeType", "easeOutCirc", "loopType", "none", "delay", 10+0.1, "time", 3));//10.1
		iTween.MoveTo(btnStartGame.gameObject, iTween.Hash("x", 140, "easeType", "easeOutCirc", "loopType", "none", "delay", 10+0.1, "time", 2));
		iTween.MoveTo(btnOptions.gameObject, iTween.Hash("x", 140, "easeType", "easeOutCirc", "loopType", "none", "delay", 10.5+0.1, "time", 2));
		iTween.MoveTo(btnCredits.gameObject, iTween.Hash("x", 140, "easeType", "easeOutCirc", "loopType", "none", "delay", 11+0.1, "time", 2));
		//iTween.MoveTo(title2.gameObject, iTween.Hash("name", "menu", "x", Screen.width*.05, "easeType", "easeInOutSine", "loopType", "none", "delay", 10.1));
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnGUI() {
		if (skin != null) {GUI.skin = skin;}
		title.draw ();

		
		
		switch(currentPage) {
			case Page.Main: showMain(); break;
			case Page.Options: showOptions(); break;
		}
	}
	
	void showMain() {
		if (GUI.Button(btnStartGame.rect,btnStartGame.content)){}
		if (GUI.Button(btnOptions.rect,btnOptions.content)){currentPage = Page.Options;}
		if (GUI.Button(btnCredits.rect,btnCredits.content)){}
	}
	
	void showOptions() {

		toolbarInt = GUI.Toolbar (optionsToolbar.rect, toolbarInt, toolbarstrings);
	    switch (toolbarInt) {
	       case 0: VolumeControl(); break;
	       case 1: Qualities(); QualityControl(); break;
	      // case 2: StatControl(); break; // TODO
	    }
	}
	
	/// <summary>
	/// Option Menu Helpers
	/// </summary>
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
