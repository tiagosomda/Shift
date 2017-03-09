using UnityEngine;
using System.Collections;

/* ControlSwitch - v0.8
 *    System responisble for handling control switching
 *    between objects [player]. Highly dependent on other
 *    scripts attached to the player.
 * 
 * CoScripts: PathControlAI, cSwitch, 
 *            PlayerControls3D, Magnetism, CamFollow3D
 * Scribes: Ryan Wood
 */
public class ControlSwitch : MonoBehaviour {

	GameObject player = null, cam = null;
	public bool minecartEnabled;
	public bool ableSwitch = true;
	bool drawLimitNotAcheived = false;
	
	// initialization of needed components
	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		cam = GameObject.FindGameObjectWithTag("MainCamera");
		
		PlayerControls3D pCon = this.gameObject.AddComponent<PlayerControls3D>();
		pCon.spawnPoint = this.GetComponent<PathControlAI>().origin.transform;
		pCon.magOn = true;
		pCon.movOn = false;
		pCon.enabled = false;
		
		Magnetism pMag = this.gameObject.AddComponent<Magnetism>();
		pMag.charge = 0;
		pMag.enabled = false;
		pMag.fieldMask = new Vector3(1,1,1);

	}
	
	// player interacts with this obj
	void OnCollisionStay(Collision kol) {
		if (!kol.collider.Equals(this.GetComponent<Collider>()))
			if (kol.collider.Equals(player.GetComponent<Collider>()) && kol.contacts.Length > 0)
				if ((Vector3.Dot(kol.contacts[0].normal, Vector3.down)) > .5)
					if(ableSwitch)
						ConSwitch();
	}
	
	// deactivate current "player", make this obj == player
	void ConSwitch() {
		player.SetActive(false);
		minecartEnabled = true;
		
		this.tag = "Player";
		cam.transform.parent = this.transform;
		cam.GetComponent<CamFollow3D>().target = this.transform;
		
		this.gameObject.GetComponent<PlayerControls3D>().enabled = true;
		this.gameObject.GetComponent<Magnetism>().enabled = true;
		this.gameObject.GetComponent<Magnetism>().dynamic = true;
		this.gameObject.GetComponent<PathControlAI>().cycle = true;

	}
	
	// this obj interacts with a switch
	void OnTriggerEnter(Collider obj) {
		if (obj.CompareTag("Switch")){
			if(gameObject.GetComponent<MinecartMechanic>().maxAmountReached()){
				GameObject.Find("Left Door Open").gameObject.GetComponent<PathingAI>().triggerStart = true;
				GameObject.Find("Right Door Open").gameObject.GetComponent<PathingAI>().triggerStart = true;
				StartCoroutine(WaitOnExit(obj.GetComponent<cSwitch>().spawn));
				ableSwitch = false;
			}else{
				drawLimitNotAcheived = true;
			}
		}
	}
	
	// this obj interacts with a switch
	void OnTriggerExit(Collider obj) {
		if (obj.CompareTag("Switch")){
				drawLimitNotAcheived = false;
		}
	}
	
	// reverts this obj to default, player is reactivated
	void Revert(GameObject spawn) {
		this.gameObject.GetComponent<PathControlAI>().cycle = false;
		
		this.gameObject.GetComponent<Magnetism>().enabled = false;
		this.gameObject.GetComponent<Magnetism>().dynamic = false;
		this.gameObject.GetComponent<PlayerControls3D>().enabled = false;
		
		cam.transform.parent = player.transform;
		cam.GetComponent<CamFollow3D>().target = player.transform;
		this.tag = "Untagged";
		
		minecartEnabled = false;
		player.transform.position = spawn.transform.position;
		player.SetActive(true); 
	}
	
	//Print to screen if weight limit is acceptable or not acceptable
	void OnGUI() {
		if(drawLimitNotAcheived == true){
			GUIStyle centeredStyle = GUI.skin.GetStyle("textArea");
   			centeredStyle.alignment = TextAnchor.UpperCenter;
    		GUI.Label (new Rect (Screen.width/2 - 100, 20, 200, 20), "Weight Limit Not Achieved", centeredStyle);
		}
	}
	
	//Waits a couple seconds before exiting the cart
	IEnumerator WaitOnExit(GameObject spawn) {
		
		yield return new WaitForSeconds (4f);
		Revert (spawn);

	}
}
