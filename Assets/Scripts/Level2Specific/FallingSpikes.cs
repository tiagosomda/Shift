using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class FallingSpikes : MonoBehaviour {
	
	public int spikeWidth;
	public int spikeDepth;
	public GameObject[] spikePrefabs;
	public List<GameObject> spikes;
	public float fallDist=10.0f;
	private GameObject player;
	
	void Start () {
		spikes = new List<GameObject>();
		Vector3 pos=this.collider.bounds.min;
		pos.z+= + (this.collider.bounds.size.z)/(spikeWidth*4);
		GameObject s;
		int rand;
		player = GameObject.Find("Player");
		for (int i=0; i<spikeWidth; i++){
			pos.x=this.collider.bounds.min.x + (this.collider.bounds.size.x)/(spikeDepth*4);
			if (i%2==0){
				pos.x+=(this.collider.bounds.size.x)/(spikeDepth*2);
			}
			for (int j=0; j<spikeDepth; j++){
				rand=Random.Range(0,8);
				s=(GameObject)Object.Instantiate(spikePrefabs[rand],pos,this.transform.rotation);
				((Spike)s.GetComponent("Spike")).setPlayer(player);
				((Spike)s.GetComponent("Spike")).setFallDist(fallDist);
				((Spike)s.GetComponent("Spike")).tag = this.gameObject.tag;
				spikes.Add(s);
				pos.x+=((this.collider.bounds.size.x)/spikeDepth);
			}			
			pos.z+=((this.collider.bounds.size.z)/spikeWidth);
		}
		
		GameObject.Find("Player").SendMessage("OnFindObjectTimes", SendMessageOptions.DontRequireReceiver);

		
	}
	
	void tellSpikesToCheck(bool t){
		for (int i=0; i<spikes.Count; i++){
			((Spike)spikes[i].GetComponent("Spike")).setCheck(t);
		}
		
	}
	
	void Update () {
		Vector3 playerPos=new Vector3(player.transform.position.x,this.transform.position.y,player.transform.position.z);
		
		if (Mathf.Sqrt (this.renderer.bounds.SqrDistance(playerPos))<fallDist){
			tellSpikesToCheck(true);
		}
		else{
			tellSpikesToCheck(false);
		}

	
	}
}
