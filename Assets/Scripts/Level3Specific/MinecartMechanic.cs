using UnityEngine;
using System.Collections;

public class MinecartMechanic : MonoBehaviour {
	
	public int oreCount;
	public int oreIncrement = 5;
	public int oreLimit = 20;
	public bool maxAmount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if( oreCount == oreLimit ){
			maxAmount = true;
		}
	}
	
	void OnCollisionEnter( Collision collide ){

		if(collide.collider.gameObject.name.Equals("Ore") && !maxAmount){
			if(	collide.collider.gameObject.GetComponent<Crawler>().GetOretype() == "Collectable" ){
				if(oreCount + oreIncrement >= oreLimit){
	       			oreCount = oreLimit;
				}else{
					oreCount = oreCount + oreIncrement;
				}
			}
		}
   	}
	
	void OnGUI() {
		if(gameObject.GetComponent<ControlSwitch>().minecartEnabled){
			GUI.Box(new Rect(10, 10, 215, 55), "");
			if( oreCount != oreLimit ){
				GUI.Label(new Rect(26, 18, 200, 70), "Current Weight : " + oreCount + " kg\nWeight Station Requires " + oreLimit + " kg.");
			}else if ( maxAmount ){
				GUI.Label(new Rect(56, 27.5f, 200, 70), "Weight Limit Reached");
			}
	    }
	}
	
	public bool maxAmountReached(){
		return maxAmount;	
	}
	
	public void resetOreCount(){
		oreCount = 0;
		maxAmount = false;
	}
	
}
