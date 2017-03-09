using UnityEngine;
using System.Collections;

public class PlayerControls3D : MonoBehaviour {
	// The spawn point of EMPA
	public Transform spawnPoint;
	// The number of collision contacts
	private int numCombinedContacts;
	// The number of collision contacts
	private int numPastContacts;
	// The number of collision contacts
	private int numFutureContacts;
	// Number of grounding contacts
	private int numGroundingContacts;
	// Indicates whether or not magnetism and movement should be enabled
	public bool magOn = true, movOn = true;
	// AudioSource for magnetism
	AudioSource magnetismAudio;
	// Audiosource for walking
	AudioSource walkingAudio;
	// Audiosource for landing
	AudioSource landingAudio;
	// Audiosource for jumping
	AudioSource jumpingAudio;
	// Audiosource for death
	AudioSource deathAudio;
	
	/**
	  * Start is called at the scene's outset
	  */
	void Start() {
		this.numPastContacts = 0;
		this.numFutureContacts = 0;
		this.numCombinedContacts = 0;
		
		magnetismAudio = gameObject.AddComponent< AudioSource >();
		magnetismAudio.clip = ( AudioClip ) Resources.Load( "MagnetismSound" );
		magnetismAudio.loop = true;
		
		walkingAudio = gameObject.AddComponent< AudioSource >();
		walkingAudio.clip = ( AudioClip ) Resources.Load( "WalkSound1" );
		walkingAudio.loop = true;
		walkingAudio.pitch = 0.73f;
		
		landingAudio = gameObject.AddComponent< AudioSource >();
		landingAudio.clip = ( AudioClip ) Resources.Load( "WalkSound3" );
		landingAudio.loop = false;
		
		jumpingAudio = gameObject.AddComponent< AudioSource >();
		jumpingAudio.clip = ( AudioClip ) Resources.Load( "JumpSound" );
		jumpingAudio.loop = false;
		
		deathAudio = gameObject.AddComponent< AudioSource >();
		deathAudio.clip = ( AudioClip ) Resources.Load( "DeathSound" );
		deathAudio.loop = false;
		
		Spawn();
	}
	
	/**
	  * Update is called once per frame
	  */
	void Update() {
		
		if (magOn) {
			// Manipulate charge with Q and E
			if ( Input.GetKey( KeyCode.Q ) && !Input.GetKey( KeyCode.E ) ) {
				this.GetComponent< Magnetism >().SetCharge( 100 );
				magnetismAudio.Play();
			} else if ( Input.GetKey( KeyCode.E ) && !Input.GetKey( KeyCode.Q ) ) {
				this.GetComponent< Magnetism >().SetCharge( -100 );
				magnetismAudio.Play();
			} else {
				this.GetComponent< Magnetism >().SetCharge( 0 );
				magnetismAudio.Pause();
			}
		}
		
		// Movement enabled
		if (movOn) {
			// Check if left and right rotate is pressed
			float horiz = Input.GetAxis( "Horizontal" );
			// If the character isn't moving forward, spin in place 3x as fast
			if ( Input.GetAxis( "Vertical" ) == 0 ) {
				horiz *= 3.0F;
			}
			
			float strafe = 0.0F;
			// Check to see if the right mouse button is pressed, if so, override previous rotation
	    	if ( Input.GetMouseButton( 1 ) ) {
	        	// Get the difference in horizontal mouse movement
				horiz = Input.GetAxis( "Mouse X" ) * 6.0F;
				
				// Allow A and D to strafe the character
				strafe = Input.GetAxis( "Horizontal" );
	    	}
			
			// Check to see if the W or S key is being pressed
			float vert = Input.GetAxis( "Vertical" );
			// Check if left and right mouse are held down; if so, override old vert
			if ( Input.GetMouseButton( 0 ) && Input.GetMouseButton( 1 ) ) {
				vert = 1;
			}		
			
			// Set animation variables
			this.GetComponent< Animator >().SetFloat( "Rotation", horiz );
			if ( vert != 0 ) {
				this.GetComponent< Animator >().SetFloat( "Speed", vert );
			} else {
				this.GetComponent< Animator >().SetFloat( "Speed", Mathf.Abs( strafe ) );
			}
			
			// Set strafe and vert to increase as a function of time
			if ( vert < 0 ) {
				vert *= Time.deltaTime * 4.0F;
				strafe *= Time.deltaTime * 4.0F;
			} else {
				vert *= Time.deltaTime * 8.0F;
				strafe *= Time.deltaTime * 8.0F;
			}
			
			// Rotate the character based on horizontal axis key presses
	    	this.transform.Rotate( 0, horiz, 0 );
			
			// Set the character direction
			// If strafing...
			if ( strafe != 0 ) {
				// ...and moving forward...
				if ( vert != 0 ) {
					strafe *= Mathf.Sqrt( 2 ) / 2;
					vert *= Mathf.Sqrt( 2 ) / 2;
					// ...move diagonally
					if ( strafe * vert > 0 ) {
						this.transform.Find( "controller_MAIN" ).rotation = this.transform.rotation * Quaternion.Euler( 0, 45, 0 );
					} else if ( strafe * vert < 0 ) {
						this.transform.Find( "controller_MAIN" ).rotation = this.transform.rotation * Quaternion.Euler( 0, -45, 0 );
					}
				// If not moving forward...
				} else {
					// ...move side to side
					if ( strafe > 0 ) {
						this.transform.Find( "controller_MAIN" ).rotation = this.transform.rotation * Quaternion.Euler( 0, 90, 0 );
					} else if ( strafe < 0 ) {
						this.transform.Find( "controller_MAIN" ).rotation = this.transform.rotation * Quaternion.Euler( 0, -90, 0 );
					}
				}
			// If not strafing...
			} else {
				// ...move straight forward
				this.transform.Find( "controller_MAIN" ).rotation = this.transform.rotation * Quaternion.Euler( 0, 0, 0 );
			}
			
			// Move the character in the appropriate direction
			if ( numGroundingContacts > 0 ) {
				this.transform.Translate( strafe, 0, vert );
			} else if ( numCombinedContacts == 0 ) {
				this.transform.Translate( 0.7f * strafe, 0, 0.7f * vert );
			}
			
			if ( numGroundingContacts > 0 && ( strafe != 0 || vert != 0 ) ) {
				if ( !walkingAudio.isPlaying ) {
					walkingAudio.Play();
				}
			} else {
				walkingAudio.Stop();
			}
			
			// Jump if space is pressed and the character can
			if ( Input.GetKeyDown( KeyCode.Space ) && numGroundingContacts > 0 ) {
				this.rigidbody.AddForce( 0, 20000, 0 );
				this.GetComponent< Animator >().SetBool( "Jump", true );
				jumpingAudio.Play();
			} else {
				this.GetComponent< Animator >().SetBool( "Jump", false );
			}
		}
		
		if ( numGroundingContacts > 0 ) {
			this.GetComponent< Animator >().SetBool( "On Wall", false );
			this.GetComponent< Animator >().SetBool( "On Ceiling", false );
			this.GetComponent< Animator >().SetBool( "Airborne", false );
		} else if ( numCombinedContacts == 0 ) {
			this.GetComponent< Animator >().SetBool( "Airborne", true );
			this.numGroundingContacts = 0;
		}		
	}
	
	public void resetPastContacts() {
		this.numCombinedContacts -= this.numPastContacts;
		this.numPastContacts = 0;
		this.numGroundingContacts = Mathf.Min( this.numGroundingContacts, this.numCombinedContacts );
	}
	
	public void resetFutureContacts() {
		this.numCombinedContacts -= this.numFutureContacts;
		this.numFutureContacts = 0;
		this.numGroundingContacts = Mathf.Min( this.numGroundingContacts, this.numCombinedContacts );
	}
	
	/**
	  * Sets the location of the player to that of the set spawnPoint
	  */
	void Spawn() {		
		if( gameObject.name.Equals( "Player" ) ){
			Vector3 checkPointPos = ( ( CheckpointSystem)spawnPoint.GetComponent( "CheckpointSystem" ) ).getPosition();
			checkPointPos = new Vector3( checkPointPos.x, checkPointPos.y + 2.0f, checkPointPos.z );
			
			this.transform.position = checkPointPos;
			this.transform.rotation = ((CheckpointSystem)spawnPoint.GetComponent("CheckpointSystem")).getRotation();
		} else if(gameObject.name.Equals( "Minecart" ) ){
			this.transform.position = spawnPoint.transform.position;
			this.transform.rotation = spawnPoint.transform.rotation;
		}
		
		if ( walkingAudio != null ) {
			walkingAudio.Stop();
		}
	}
	
	/**
	  * Sets the actions to be performed when an OnDeath message is sent to the player object
	  */
	void OnDeath() {
		if ( deathAudio != null ) {
			deathAudio.Play();
		}
		Spawn();
	}
	
	/* Sets the behavior for when a collision occurs */
	void OnCollisionExit( Collision col ) {
		if ( col.gameObject.CompareTag( "Past" ) ) {
			this.numPastContacts--;
		} else if ( col.gameObject.CompareTag( "Future" ) ) {
			this.numFutureContacts--;
		}
		this.numCombinedContacts--;
		
		if ( this.numCombinedContacts == 0 ) {
			this.GetComponent< Animator >().SetBool( "On Wall", false );
			this.GetComponent< Animator >().SetBool( "On Ceiling", false );
			this.GetComponent< Animator >().SetBool( "Airborne", true );
		}
		if ( col.contacts.Length > 0 ) {
			if ( ( Vector3.Dot( col.contacts[0].normal, Vector3.up ) > 0.5f ) ) {
				numGroundingContacts--;
			}
			if ( ( Vector3.Dot( col.contacts[0].normal, Vector3.right ) > 0.5 ) ||
				 ( Vector3.Dot( col.contacts[0].normal, Vector3.right ) < -0.5f ) ||
			   	 ( Vector3.Dot( col.contacts[0].normal, Vector3.forward ) > 0.5f ) ||
				 ( Vector3.Dot( col.contacts[0].normal, Vector3.forward ) < -0.5f ) ) {
				this.GetComponent< Animator >().SetBool( "On Wall", false );
			}
			if ( ( Vector3.Dot( col.contacts[0].normal, Vector3.up ) < -0.5f ) ) {
				this.GetComponent< Animator >().SetBool( "On Ceiling", false );
			}
		}
	}
	
	/* Sets the behavior when leaving a surface */
	void OnCollisionEnter( Collision col ) {
		if ( col.gameObject.CompareTag( "Past" ) ) {
			this.numPastContacts++;
		} else if ( col.gameObject.CompareTag( "Future" ) ) {
			this.numFutureContacts++;
		}
		this.numCombinedContacts = Mathf.Max( this.numCombinedContacts + 1, 1 );
		
		this.GetComponent< Animator >().SetBool( "Airborne", false );
		if ( col.contacts.Length > 0 ) {
			if ( ( Vector3.Dot( col.contacts[0].normal, Vector3.up ) > .5 ) ) {
				numGroundingContacts++;
				this.GetComponent< Animator >().SetBool( "On Wall", false );
				this.GetComponent< Animator >().SetBool( "On Ceiling", false );
				landingAudio.Play();
			}
			if ( numGroundingContacts == 0 ) {
				if ( ( Vector3.Dot( col.contacts[0].normal, Vector3.right ) > 0.5 ) ||
					 ( Vector3.Dot( col.contacts[0].normal, Vector3.right ) < -0.5f ) ||
					 ( Vector3.Dot( col.contacts[0].normal, Vector3.forward ) > 0.5f ) ||
					 ( Vector3.Dot( col.contacts[0].normal, Vector3.forward ) < -0.5f ) ) {
					this.GetComponent< Animator >().SetBool( "On Wall", true );
				}
				if ( ( Vector3.Dot( col.contacts[0].normal, Vector3.up ) < -0.5 ) ) {
					this.GetComponent< Animator >().SetBool( "On Ceiling", true );
				}
			}
		}
	}
}