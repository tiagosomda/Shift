using UnityEngine;
using System.Collections;


public class TimeSwitching : MonoBehaviour {
	
	public bool past = true;
	public GameObject[] pastObjs;
	public GameObject[] futureObjs;
	public GameObject[] bothObjs;
	private AudioClip timeSwitchSound;
	public Transform player;
	public bool found=false;

	// Use this for initialization
	void Start () {		
		//pastObjs = GameObject.FindGameObjectsWithTag( "Past" );
		//futureObjs = GameObject.FindGameObjectsWithTag( "Future" );
		//bothObjs = GameObject.FindGameObjectsWithTag( "Both" );
		this.timeSwitchSound = ( AudioClip ) Resources.Load( "TimeSwitchSound" );
		//SwitchTime();		
	}
	
	void OnFindObjectTimes() {
		foreach ( GameObject obj in pastObjs ) {    
   			obj.SetActive( true );
 	 	}
  
  		foreach ( GameObject obj in futureObjs ) {    
  	 		obj.SetActive( true );
  		}
   
  		pastObjs = GameObject.FindGameObjectsWithTag( "Past" );
  		futureObjs = GameObject.FindGameObjectsWithTag( "Future" );
  		bothObjs = GameObject.FindGameObjectsWithTag( "Both" );
  		SwitchTime();
  		SwitchTime();
	}
	
	// Update is called once per frame
	void Update () {
		if (!found){
   			OnFindObjectTimes();
   			found=true;
 		}
		if ( Input.GetKeyUp( KeyCode.LeftShift ) ) {
			past = !past;
			//AudioSource.PlayClipAtPoint( timeSwitchSound, this.player.transform.position );
			AudioSource.PlayClipAtPoint( timeSwitchSound, this.player.transform.position, 0.2f );
			SwitchTime();
		}
	}
	
	void SwitchTime(){ 
		if ( past ){
			RenderSettings.skybox = (Material)Resources.Load("PastSkybox", typeof(Material));
			
			foreach ( GameObject obj in pastObjs ) {				
				obj.SetActive( true );
			}
			
			foreach ( GameObject obj in futureObjs ) {				
				obj.SetActive( false );
			}
			
			foreach ( GameObject obj in bothObjs ) {				
				obj.GetComponent<TimeMaterials>().setMat( true );
			}
		} else {
			RenderSettings.skybox = (Material)Resources.Load("FutureSkybox", typeof(Material));
			
			foreach ( GameObject obj in futureObjs ) {				
				obj.SetActive( true );
			}
			
			foreach ( GameObject obj in pastObjs ) {				
				obj.SetActive( false );
			}
			
			foreach ( GameObject obj in bothObjs ) {				
				obj.GetComponent< TimeMaterials >().setMat( false );
			}
		}
		
	}
	
}
