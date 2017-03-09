using UnityEngine;
using System.Collections;

public class PlayerControls2D : MonoBehaviour {
	
	public float speed = 1000.0F;
	public float jump = 600.0F;
	public float gMod = -20.0f;
	float x = 0.0F, y = 0.0F;
	Vector3 direction = Vector3.zero;
	private bool grounded = true;
	public float chargeMultiplier = 10.0f;	
	
	void OnCollisionStay(Collision kol) {
		if (kol.contacts.Length > 0)
			if ((Vector3.Dot(kol.contacts[0].normal, Vector3.up)) > .5)
				grounded = true;
	}
	
	void Update(){
	
		if ( Input.GetKeyDown( KeyCode.C ) ) {
			this.GetComponent< Magnetism >().SetCharge( 1 * chargeMultiplier );
		}
		
		if ( Input.GetKeyDown( KeyCode.X ) ) {
			this.GetComponent< Magnetism >().SetCharge( -1 * chargeMultiplier );
		}
		
		if ( Input.GetKeyUp( KeyCode.C) || Input.GetKeyUp( KeyCode.X ) ){
			this.GetComponent< Magnetism >().SetCharge( 0 );
		}
		
		if ( Input.GetKeyUp( KeyCode.X ) ) {
			this.GetComponent< Magnetism >().SetCharge( 0 );
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {	
		
		x = Input.GetAxis("Horizontal");
		y = Input.GetAxis("Vertical");
		
		if (grounded){
			x *= speed;
		} else {// in-air 
			x *= (speed/4);
		}
		
		if (Input.GetKey(KeyCode.Space) && grounded) {
			y += jump;
			grounded = false;
		}
		
		direction = new Vector3( x * Time.deltaTime, ( gMod + y ) * Time.deltaTime );
		GetComponent<Rigidbody>().AddForce(direction, ForceMode.VelocityChange);
		
	}
	
	void Spawn() {
		this.transform.position = new Vector3( 0, 0, 0 );
	}
	
	void OnDeath() {
		Spawn();
	}
}
