using UnityEngine;
using System.Collections;

/* CeilingCrawler - v0.1
 *    Responsible for randomly generating ore for the player to
 *	  either avoid or pick up.
 *    
 *    v0.1 ~ Basic implementation
 * 
 * CoScripts: Magnetism
 * Scribes: Luka Hankewycz
 */
public class CeilingCrawler : MonoBehaviour {
	
	public float PositivePolarityCharge;
	public float NegativePolarityCharge;
	bool createMineral = false;
	bool harmfulDebris = false;
	bool positiveOre = false;
	bool negativeOre = false;
	GameObject MiningOre;
	GameObject ore = null;
	
	
	// Use this for initialization
	void Start () {
	
		InvokeRepeating("SpawnOre", .01f, 4f);
		MiningOre = GameObject.Find("MiningOre");
	}

	// Mineral ore generation
	void SpawnOre() {
		
		int n = Random.Range(0, 100);
		if(n <= 25){
			harmfulDebris = true;
			createMineral = true;
		}else if((n <= 50) && (n > 25)){
			positiveOre = true;
			createMineral = true;
		}else if((n <= 75) && (n > 50)){
			negativeOre = true;
			createMineral = true;
		}
		
		if(createMineral){
			ore = (GameObject)Instantiate(MiningOre);
			ore.transform.position = gameObject.transform.position + ( Vector3.down * 1 );
			ore.name = "Ore";
		}
		
		if(positiveOre){
			ore.GetComponent<Renderer>().material = (Material) Resources.Load("Blue", typeof(Material));
			ore.AddComponent<Rigidbody>();
			ore.GetComponent<Rigidbody>().mass = 1;
			ore.AddComponent<Magnetism>();
			ore.GetComponent<Magnetism>().charge = PositivePolarityCharge;
			ore.GetComponent<Magnetism>().dynamic = true;
			ore.AddComponent<Crawler>();
			ore.GetComponent<Crawler>().collectableOre = true;
		}else if(negativeOre){
			ore.GetComponent<Renderer>().material = (Material) Resources.Load("Red", typeof(Material));
			ore.AddComponent<Rigidbody>();
			ore.GetComponent<Rigidbody>().mass = 1;
			ore.AddComponent<Magnetism>();
			ore.GetComponent<Magnetism>().charge = NegativePolarityCharge;
			ore.GetComponent<Magnetism>().dynamic = true;
			ore.AddComponent<Crawler>();
			ore.GetComponent<Crawler>().collectableOre = true;
		}else if(harmfulDebris){
			ore.GetComponent<Renderer>().material = (Material) Resources.Load("RockStubble", typeof(Material));
			ore.transform.localScale = new Vector3(3f, 3f, 3f);
			ore.AddComponent<Rigidbody>();
			ore.AddComponent<Crawler>();
			ore.GetComponent<Crawler>().dangerousOre = true;
		}
		
		negativeOre = false;
		positiveOre = false;
		harmfulDebris = false;
		createMineral = false;

	}
}
