using UnityEngine;
using System.Collections;

public class Crawler : MonoBehaviour {
	
	public bool dangerousOre = false;
	public bool collectableOre = false;
	public bool harmful;
	
	// Use this for initialization
	void Start () {
		if(dangerousOre){
			harmful = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//Destroy on collision
	void OnCollisionEnter( Collision collide ) {
		
		if (collide.gameObject.CompareTag("Player")){
			if( dangerousOre && harmful){
				DestroyObject();
				collide.gameObject.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
				collide.gameObject.GetComponent<MinecartMechanic>().resetOreCount();
			}if( dangerousOre && !harmful){
					DestroyObject();
			}if(!collide.gameObject.GetComponent<MinecartMechanic>().maxAmount){
				if( collectableOre ){
					DestroyObject();
				}
			}
		}else{
			InvokeRepeating("DestroyObject", 3f, 0f);
		}
		if (!collide.collider.gameObject.CompareTag("Player")){
			if ( collide.contacts.Length > 0 ) {
				if ( ( Vector3.Dot( collide.contacts[0].normal, Vector3.up ) > .5 ) ) {
					harmful = false;
				}
			}
		}
	}
	
	void OnTriggerEnter( Collider other ) {
		if (other.gameObject.name.Equals("DeathZone")){
			DestroyObject();
		}
	}
	
	void DestroyObject() {
		Destroy(gameObject);
	}
	
	public void SetOretype(string type){
		if( type == "Dangerous" ){
			dangerousOre = true;
		}else if( type == "Collectable" ){
			collectableOre = true;
		}
	}
	
	public string GetOretype(){
		if( dangerousOre == true ){
			return "Dangerous";
		}else if( collectableOre == true ){
			return "Collectable";
		}else{
			return null;
		}
	}
	
}
