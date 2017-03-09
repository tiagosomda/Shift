using UnityEngine;
using System.Collections;
// Code based of http://wiki.unity3d.com/index.php/PauseMenu

public class PauseMenu : MonoBehaviour {
	public GUISkin skin;

	private float startTime = 0.1f;
	private float savedTimeScale;
	
	int  toolbarInt = 0;
	string[]  toolbarstrings =  {"Audio","Graphics"};
		
	private Page currentPage;
	public enum Page { None, Start, Pause, Options, Credits, Demos, GameStory }
	private Page[] pageHistory = {Page.None,Page.None,Page.None,Page.None}; 
	private int currentHistory = 0;
	
	public string[] credits= {"Game Story: Zach and Luka",
							  "Core Mechanics: R. Gross and Rachel",
							  "Artificial Intelligence: Luka, R. Wood and Zach",
	                          "Concept Art: Ross and Leo", 
							  "3D Models: Ross",
							  "System and Interface: Tiago"} ;
	
	public string[] gameStory = {"Setting: Metal is scarce in 2113, but it is abundant in 1913. Time traveling has been discovered, but it is illegal to humans. Thus, Robot-Only facilities were created to mine minerals from regions that were not explored in past, but which are now useless due to the time that has passed.",
								 "Main Character: EMPA",
								 "Side Character: ANA",
	                             "Plot:  ANA lost her parts. EMPA will save her!",
								 "Conclusion: Shift Happens... "};
	
	//Starts game menu
	void Start() {
		Time.timeScale = 1;
		if (Application.loadedLevelName == "StartScreen") {
			PauseGame();
		}
	}
	
	void Update() {
		
		//Back Button - Mapped to Escape Key
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (currentPage != Page.None) {
				GoBack ();
			} else {
				PauseGame();	
			}
		}
	}
	
	void GoBack() {
		if(currentHistory > 0) {
			--currentHistory;
			currentPage = pageHistory[currentHistory];
			if (currentPage == Page.None) {
				UnPauseGame();	
			}
		}
	}
	
	
	void LateUpdate() {	
		if (Input.GetKeyDown(KeyCode.Return)) {
			switch(currentPage) {
				case Page.None: PauseGame(); break;
				case Page.Start : UnPauseGame(); break;
				case Page.Pause : UnPauseGame(); break;
				default : currentPage = Page.None; break;
			}
		}
	}
	
	//Draws current menu
	void OnGUI() {		
		if (skin != null) { GUI.skin = skin;}
		if (Application.loadedLevelName == "StartScreen" ) {
			if (IsGamePaused()) {
				switch (currentPage) {
					case Page.None	   : PressStart();	  break;
					case Page.Start	   : StartMenu(); 	  break;
					case Page.Demos	   : ShowDemos(); 	  break;
					case Page.Options  : ShowOptions();   break;
					case Page.Credits  : ShowCredits();   break;
					case Page.GameStory: ShowGameStory(); break;
				}
			} 
		} else {
			if (IsGamePaused()) {
				GUI.Box (new Rect(0,0,Screen.width, Screen.height), "");
				switch (currentPage) {
					case Page.Pause	   : PauseScreen();   break;
					case Page.Demos	   : ShowDemos(); 	  break;
					case Page.Options  : ShowOptions();   break;
					case Page.Credits  : ShowCredits();   break;
					case Page.GameStory: ShowGameStory(); break;
				}
			}
		}
	}
	
	void PressStart() {
		BeginPage(320,400);
		GUILayout.Box("SHIFT","title");
		EndPage ();
	}
	
	void StartMenu() {
		BeginPage(320,400);
		GUILayout.Box("SHIFT","title");
		if (GUILayout.Button ("Play")) {
			Application.LoadLevel("Level1");
		}
		
		if (GUILayout.Button ("Options")) {
			currentPage = Page.Options;
			pageHistory[++currentHistory] = currentPage;
		}
		if (GUILayout.Button ("Credits")) {
			currentPage = Page.Credits;
			pageHistory[++currentHistory] = currentPage;
		}
		EndPage();
	}
	
	void PauseScreen() {
		BeginPage(200,200);
		if (GUILayout.Button ("Options")) {
			currentPage = Page.Options;
			pageHistory[++currentHistory] = currentPage;
		}
		if (GUILayout.Button ("Credits")) {
			currentPage = Page.Credits;
			pageHistory[++currentHistory] = currentPage;
		}
		if (GUILayout.Button ("Quit")) {
			//Go to Level X
			Application.LoadLevel("StartScreen");
		}
		EndPage();
	}
	
	void ShowDemos() {
		BeginPage(400,300);
		
		if (GUILayout.Button("Game Story")) {
			currentPage = Page.GameStory;
			pageHistory[++currentHistory] = currentPage;
		}
		
		if (GUILayout.Button("Core Mechanics")) {
			UnPauseGame();
			Application.LoadLevel("TechDemo3D");
		}
		
		if (GUILayout.Button("Artificial Intelligence")) {
			UnPauseGame();
			Application.LoadLevel("AI_Demo");
		}
		
		if (GUILayout.Button("Concept Art")) {
			UnPauseGame();
			Application.LoadLevel("ConceptArt");
		}

		
		if (GUILayout.Button("3D Models")) {
			UnPauseGame();
			Application.LoadLevel("Models3D");
		}

		EndPage();
	}
	
	void ShowCredits() {
		BeginPage(700, 500);
		//Draws credit text on screen
		foreach (string credit in credits) {
			GUILayout.Label(credit);	
		}
		EndPage();
	}
	
	void ShowGameStory() {
		GUILayout.BeginArea(new Rect((Screen.width-500)/2, 
							         (Screen.height - 400)/4, 
									  500, 800)
							);	
		//Draws credit text on screen
		foreach (string story in gameStory) {
			GUILayout.Label(story,"story");	
		}
		EndPage();
	}
	
	void ShowBackButton() {
		if (GUI.Button(new Rect(20, Screen.height - 50, 60, 30),"Back","backButton")) {
	        GoBack();
	    }
	}
	
	void PauseGame() {
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		if(Application.loadedLevelName == "StartScreen") {
			currentPage = Page.Start;
			pageHistory[++currentHistory] = currentPage;
		} else {
			currentPage = Page.Pause;
			pageHistory[++currentHistory] = currentPage;
		}
	}
	
	void UnPauseGame() {
		Time.timeScale = savedTimeScale;
		currentPage = Page.None;
	}
	
	void BeginPage(int width, int height) {
		GUILayout.BeginArea(new Rect((Screen.width-width)/2, 
							         (Screen.height - height)/2, 
									  width, height)
							);	
	}	

	void EndPage() {
		GUILayout.EndArea();
		if (currentPage != Page.Start) {
			ShowBackButton();	
		}
	}
	
	bool IsBeginning() {
		return (Time.time < startTime);	
	}
	
	bool IsGamePaused() {
		return (Time.timeScale == 0);
	}
	
	void ShowOptions() {
		BeginPage(700, 500);
		toolbarInt = GUILayout.Toolbar (toolbarInt, toolbarstrings);
	    switch (toolbarInt) {
	       case 0: VolumeControl(); break;
	       case 1: Qualities(); QualityControl(); break;
	      // case 2: StatControl(); break; // TODO
	    }
		EndPage();
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