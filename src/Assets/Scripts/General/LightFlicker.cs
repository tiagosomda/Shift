using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {
	
	int cycle;
	public float intensityScale;
	public float intensityMin;
	public float intensityMax;
	
	// Use this for initialization
	void Start () {
		cycle = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(cycle == 1){
			
			GetComponent<Light>().intensity -= intensityScale;
				
			if(GetComponent<Light>().intensity <= intensityMin){
				cycle = 0;
			}
			
		}
		
		if(cycle == 0){
			
			GetComponent<Light>().intensity += intensityScale;

			if(GetComponent<Light>().intensity >= intensityMax){
				cycle = 1;
			}
			
		}
	}
	
}
