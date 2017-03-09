using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This class contains data for the charge of objects and methods that applies forces
//to charged objects based on the charges of nearby charged objects
public class Magnetism : MonoBehaviour {
	// The charge of this object
	public float charge;
	// Flag for if this object should be affected by magnetic forces on its movement
	public bool dynamic;
	// Reference to the object this one is attracted to and touching
	private GameObject attractor;
	// Mask for the direction of the magnetic field
	public Vector3 fieldMask;
	// Reference to the particle effect prefab for this charge
	private ParticleSystem chargeEffect;
	// Reference to the original scale of this object
	private Vector3 originalScale;
	
	void Start() {
		this.originalScale = this.transform.localScale;
		GameObject prefabReference = ( GameObject )Resources.Load( "Charge Effect", typeof( GameObject ) );
		this.chargeEffect = Instantiate( prefabReference.GetComponent< ParticleSystem >(), this.transform.position, this.transform.rotation ) as ParticleSystem;
		this.chargeEffect.transform.parent = this.transform;
		if ( this.charge > 0 ) {
			this.chargeEffect.Clear();
			this.chargeEffect.startColor = new Color( 0.2f, 0.6f, 1.0f, 0.7f );
		} else if ( this.charge < 0 ) {
			this.chargeEffect.Clear();
			this.chargeEffect.startColor = new Color( 1.0f, 0.6f, 0.2f, 0.7f );	
		} else {
			this.chargeEffect.Clear();
			this.chargeEffect.startColor = new Color( 0.0f, 0.0f, 0.0f, 0.0f );	
		}
	}
	
	void FixedUpdate () {		
		if ( this.charge == 0 ) {
			if ( this.dynamic ) {
				this.attractor = null;
				this.transform.parent = null;
				this.transform.localScale = originalScale;
			}
			return;
		}
		
		if ( this.attractor != null ) {
			return;	
		}
		
		Collider[] nearObjs = Physics.OverlapSphere( this.transform.position, 100 );
		
		foreach ( Collider obj in nearObjs ) {			
			if ( obj.GetComponent< Magnetism >() && obj.GetComponent< Magnetism >().dynamic && !obj.Equals( this.collider ) ) {		
				float otherCharge = obj.GetComponent< Magnetism >().GetCharge();			
				
				if ( otherCharge != 0 ) {
					Vector3 direction = obj.transform.position - this.transform.position;
					Vector3 force = Vector3.Scale( fieldMask, direction.normalized * ( this.charge * otherCharge ) / ( direction.magnitude * direction.magnitude ) );
					
					bool xCheck = this.collider.bounds.max.x > obj.collider.bounds.min.x && this.collider.bounds.min.x < obj.collider.bounds.max.x;
					bool yCheck = this.collider.bounds.max.y > obj.collider.bounds.min.y && this.collider.bounds.min.y < obj.collider.bounds.max.y;
					bool zCheck = this.collider.bounds.max.z > obj.collider.bounds.min.z &&	this.collider.bounds.min.z < obj.collider.bounds.max.z;
					
					if ( ( fieldMask.x == 1 && yCheck && zCheck ) || ( fieldMask.y == 1 && xCheck && zCheck ) || ( fieldMask.z == 1 && xCheck && yCheck ) ) {
						obj.rigidbody.AddForce( force );
					}
				}
			}
		}	
	}
	
	/** Returns the value of charge */
	public float GetCharge() {
		return this.charge;			
	}		
	
	/** Sets the value of charge and sets the particle affect to the appropriate color */
	public void SetCharge( float newCharge ) {
		if ( newCharge != this.charge ) {
			this.charge = newCharge;
			if ( this.charge > 0 ) {
				this.chargeEffect.Clear();
				this.chargeEffect.startColor = new Color( 0.2f, 0.6f, 1.0f, 0.7f );
			} else if ( this.charge < 0 ) {
				this.chargeEffect.Clear();
				this.chargeEffect.startColor = new Color( 1.0f, 0.6f, 0.2f, 0.7f );	
			} else {
				this.chargeEffect.Clear();
				this.chargeEffect.startColor = new Color( 0.0f, 0.0f, 0.0f, 0.0f );	
			}
		}
	}
	
	void OnCollisionEnter( Collision collision ) {
		if ( this.dynamic && collision.gameObject.GetComponent< Magnetism >() && !collision.gameObject.GetComponent< Magnetism >().dynamic && this.charge * collision.gameObject.GetComponent< Magnetism >().charge < 0 ) {
			this.attractor = collision.gameObject;
			this.rigidbody.velocity = new Vector3( 0, 0, 0 );
			//this.transform.parent = this.attractor.transform;
		}		
	}
	
	void OnCollisionExit( Collision collision ) {
		if ( this.dynamic && collision.gameObject.Equals( this.attractor ) ) {
			this.attractor = null;
			this.transform.parent = null;
			this.transform.localScale = originalScale;
		}
	}
}