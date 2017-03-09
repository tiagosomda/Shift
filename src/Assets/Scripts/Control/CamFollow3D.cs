using UnityEngine;
using System.Collections; 

public class CamFollow3D : MonoBehaviour {
    public Transform target;
    private float targetHeight = 1.7f;
	private float distance = 5.0f;
    private float maxDistance = 20;
    private float minDistance = .6f;
    private float xSpeed = 250.0f;
    private float ySpeed = 120.0f;
    private int zoomRate = 40;
    private float rotationDampening = 3.0f;
    private float zoomDampening = 5.0f;
    private float x = 0.0f;
	private float y = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private float correctedDistance;
    private AudioClip backgroundMusic;
	
	/**
	 * This function sets the initial angle and distance values.
	 */
    void Start () {
        // Set the initial values for the rotation angles
		Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;
        
		// Set the initial values for the distances
		currentDistance = distance;
        desiredDistance = distance;
        correctedDistance = distance;

        gameObject.AddComponent<AudioSource>();
        GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("BackgroundMusic");
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().volume = 0.15f;
        GetComponent<AudioSource>().Play();
    }    

    /**
     * LateUpdate is used to move the camera after player movements have been executed in Update().
     */
    void LateUpdate () {
        // If the right mouse button is down, the camera follows behind the player
        if ( Input.GetMouseButton( 1 ) ) {
			float targetRotationAngle = target.eulerAngles.y;
            float currentRotationAngle = transform.eulerAngles.y;
            x = Mathf.LerpAngle( currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime );
			y -= Input.GetAxis( "Mouse Y" ) * ySpeed * 0.02f;
		
		// If the left mouse button is down, let the mouse govern camera position
		} else if ( Input.GetMouseButton( 0 ) ) {
            x += Input.GetAxis( "Mouse X" ) * xSpeed * 0.02f;
            y -= Input.GetAxis( "Mouse Y" ) * ySpeed * 0.02f;

        // Otherwise, ease behind the target if any of the directional keys are pressed
		} else if ( Input.GetAxis( "Vertical" ) != 0 || Input.GetAxis( "Horizontal" ) != 0 ) {
            float targetRotationAngle = target.eulerAngles.y;
            float currentRotationAngle = transform.eulerAngles.y;
            x = Mathf.LerpAngle( currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime );
        } 
		
		// Clamp the angle to 80 degrees above and below the horizon line
        y = ClampAngle( y, -80F, 80F ); 

        // Set the camera rotation to the appropriate rotations around the x and y axes
        Quaternion rotation = Quaternion.Euler( y, x, 0 ); 

        // Calculate the desired distance
        desiredDistance -= Input.GetAxis( "Mouse ScrollWheel" ) * Time.deltaTime * zoomRate * Mathf.Abs( desiredDistance );
        desiredDistance = Mathf.Clamp( desiredDistance, minDistance, maxDistance );
        correctedDistance = desiredDistance; 

        // Calculate desired camera position
        Vector3 position = target.position - ( rotation * Vector3.forward * desiredDistance + new Vector3( 0, -targetHeight, 0 ) ); 

        // Check for collision using the true target's desired registration point as set by user using height
        RaycastHit collisionHit;
        Vector3 trueTargetPosition = new Vector3( target.position.x, target.position.y + targetHeight, target.position.z ); 

        // If there was a collision, correct the camera position and calculate the corrected distance
        bool isCorrected = false;
        if ( Physics.Linecast(trueTargetPosition, position, out collisionHit ) ) {
			if ( collisionHit.collider.gameObject.GetComponent< popMessageTrigger >() == null ) {
           		position = collisionHit.point;
            	correctedDistance = Vector3.Distance( trueTargetPosition, position );
            	isCorrected = true;
			}
        }        

        // For smoothing, lerp distance only if either distance wasn't corrected, or correctedDistance is more than currentDistance
        currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp( currentDistance, correctedDistance, Time.deltaTime * zoomDampening ) : correctedDistance;

        // Recalculate position based on the new currentDistance
        position = target.position - ( rotation * Vector3.forward * currentDistance + new Vector3( 0, -targetHeight, 0 ) ); 

        transform.rotation = rotation;
        transform.position = position;
    } 
	
	/**
	 * This function clamps the angle between a maximum and minimum value.
	 */
    private static float ClampAngle( float angle, float min, float max ) {
        //Don't allow angles less than -360 
		if ( angle < -360 ) {
            angle += 360;
		}
		
		//Don't allow angles greater than 360
        if ( angle > 360 ) {
            angle -= 360;
		}

        return Mathf.Clamp( angle, min, max );
    }
}
	