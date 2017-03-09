using UnityEngine;
using System.Collections;

public class SlideShow : MonoBehaviour {
	
	public Texture2D[] slides = new Texture2D[1];
   	private int currentSlide = 0;
	public GUITexture slideshow;
	
	// Use this for initialization
	void Start () {
		slideshow.texture = slides[currentSlide];
		currentSlide++;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.LeftArrow)) {currentSlide--;}
		if(Input.GetKeyDown(KeyCode.RightArrow)) {currentSlide++;}		
		
		//Check for out of bounds - I could have used mod 
		//but it didn't work - I am too tired to think about it
		if (currentSlide >= slides.Length) {currentSlide = 0;}
		else if (currentSlide < 0) {currentSlide = slides.Length -1;}
		slideshow.texture = slides[currentSlide];
	
	}
}
