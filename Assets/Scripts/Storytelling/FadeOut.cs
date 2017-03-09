using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour {
	
	public int guiDepth = 0;
	public string levelToLoad = "1";
	public Texture2D splashLogo; // the logo to splash;
	public float alpha = 1.0f;
	public float fadeSpeed = 0.3f;

	
	public enum LogoPositioning
	{
		Centered,
		Stretched
	}
	private Rect splashLogoPos = new Rect();
	public LogoPositioning logoPositioning;
	

	// Use this for initialization
	void Start () {
		if (logoPositioning == LogoPositioning.Centered)
		{
			splashLogoPos.x = (Screen.width * 0.5f) - (splashLogo.width * 0.5f);
			splashLogoPos.y = (Screen.height * 0.5f) - (splashLogo.height * 0.5f);
 
			splashLogoPos.width = splashLogo.width;
			splashLogoPos.height = splashLogo.height;
		}
		else
		{
			splashLogoPos.x = 0;
			splashLogoPos.y = 0;
 
			splashLogoPos.width = Screen.width*2;
			splashLogoPos.height = Screen.height*2;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		alpha += -fadeSpeed * Time.deltaTime;
		if(Input.anyKey) {
			Application.LoadLevel(levelToLoad);
		}
	}
	
	void OnGUI() {
		GUI.depth = guiDepth;
		if (splashLogo != null) {
			GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, Mathf.Clamp01(alpha));
			GUI.DrawTexture(splashLogoPos, splashLogo);
		}
	}
}
