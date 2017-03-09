using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointSystem : MonoBehaviour {
	//last checkpoint visited. set this in the editor to the spawnpoint
	public GameObject currentCheckpoint;
	private List<GameObject> checkpoints;

	//rotation of player when entered checkpoint
	private Quaternion rotation;
	
	public Color activeColor;
	public Color inactiveColor;

	void Start () {
		
		checkpoints = new List<GameObject>();
		foreach (Transform child in transform){
			checkpoints.Add (child.gameObject);
			child.GetComponent<ParticleSystem>().startColor=inactiveColor;
		}
		rotation=new Quaternion(0,0,0,1);
		currentCheckpoint.GetComponent<ParticleSystem>().Stop();
		
	}
	
	void Update () {
	
	}
	
	
	public Vector3 getPosition(){
		return currentCheckpoint.transform.position;
	}
	
	public Quaternion getRotation(){
		return rotation;
	}
	
	public void handleCollision(GameObject checkpoint, Quaternion rot){
		if (currentCheckpoint!=checkpoint && checkpoint.name != "Starting Checkpoint"){
			currentCheckpoint.GetComponent<ParticleSystem>().startColor=inactiveColor;
			currentCheckpoint.GetComponent<ParticleSystem>().Clear();
			currentCheckpoint=checkpoint;		
			currentCheckpoint.GetComponent<ParticleSystem>().startColor=activeColor;
			currentCheckpoint.GetComponent<ParticleSystem>().Clear();
		}
		rotation=rot;
	}
	
}