using UnityEngine;
using System.Collections;

public class craneBox : MonoBehaviour {
	//Locations the crane drops the box
	public Vector3[] dropLocations;
	//Iterator through the list of box locations
	private int locationIterator;
	
	/** Sends an OnDeath message to the player when he collides with the bottom of the box */
	void OnCollisionEnter( Collision collision ){
		if ( collision.gameObject.name.Equals( "Player" ) ) {			
	        foreach ( ContactPoint contact in collision.contacts ) {
				if ( contact.normal.y >= 0.7f ) {
					collision.gameObject.SendMessage( "OnDeath", SendMessageOptions.DontRequireReceiver );
				}
	        }
		}		
	}
	
	/** Returns the next drop location for the box */
	public Vector3 getDropLocation() {		
		Vector3 newLocation = this.dropLocations[ this.locationIterator ];
		this.locationIterator = ( this.locationIterator + 1 ) % this.dropLocations.Length;
		return newLocation;
	}
}
