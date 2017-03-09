using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* ObeliskAI - v0.7
 *    Responsible for the waxing and waning of obelisk turrets.
 *    Behavior based off DetectionAI, aka player proximity. As
 *    of now, should be attached to the "shaft."
 *    
 *    v0.6 ~ modular apex creation in relation to shaft
 *    v0.7 ~ orb-based projectiles & life cycle, cooldown system
 * 
 * CoScripts: DetectionAI
 * Scribes: Ryan Wood
 */
public class ObeliskAI : MonoBehaviour {
	
	Vector3 scale = Vector3.zero, lok = Vector3.zero;
	bool cycle = true, erect = false, refreshing = false;
	public float cooldown = 3.5f;
	float shift = .05f;				// rate of change, 0 - 1
	List<GameObject> apex = new List<GameObject>();
	GameObject orb = null;
	DetectionAI observer = null;
	
	// initialize turret, ~collapse
	void Start() {
		observer = this.gameObject.GetComponent<DetectionAI>();
		scale = this.transform.localScale;
		lok = this.transform.localPosition;
		this.transform.localScale -= new Vector3(0, scale.y, 0);
		this.transform.localPosition -= new Vector3(0, scale.y/2, 0);
		
		// establish the apex based on the shaft & deactivate
		GameObject o = null;
		for (int i=0; i<4; i++) {
			o = new GameObject("Edge");
			LineRenderer edge = o.AddComponent<LineRenderer>();
			edge.material = (Material) Resources.Load("Fairy Dust", typeof(Material));
			edge.SetWidth(scale.x, .1f);
			edge.SetVertexCount(2);
			// clockwise
			switch (i) {
			case 0:
				edge.SetPosition(0, new Vector3(lok.x, lok.y + (scale.y/2), lok.z - (scale.z/2)));
				break;
			case 1:
				edge.SetPosition(0, new Vector3(lok.x - (scale.x/2), lok.y + (scale.y/2), lok.z));
				break;
			case 2:
				edge.SetPosition(0, new Vector3(lok.x, lok.y + (scale.y/2), lok.z + (scale.z/2)));
				break;
			case 3:
				edge.SetPosition(0, new Vector3(lok.x + (scale.x/2), lok.y + (scale.y/2), lok.z));
				break;
			default:
				break;
			}
			edge.SetPosition(1, new Vector3(lok.x, lok.y + (scale.y/2) + .5f, lok.z));
			o.SetActive(false);
			apex.Add(o);
		}	
	}
	
	// update cycle
	void Update() {
		if (cycle) {
			if (observer.LoSD() && !erect) {
				cycle = false;
				StartCoroutine(Wax());
			}
			if (!observer.LoSD() && erect) {
				cycle = false;
				StartCoroutine(Wane());
			}
			if (erect && orb != null && observer.LoST()) {
				orb.GetComponent<OrbAI>().Activate(observer.User(), observer.targetLoS*10);
				orb = null;
				StartCoroutine(Cooldown());
			}
			if (erect && orb == null && !refreshing) SpawnOrb();
		}
	}
	
	IEnumerator Cooldown() {
		refreshing = true;
		yield return new WaitForSeconds(cooldown);
		refreshing = false;
	}
	
	// obelisk expansion
	IEnumerator Wax() {
		if (this.transform.localScale.y < scale.y) {
			this.transform.localScale += new Vector3(0, shift * scale.y, 0);
			this.transform.localPosition += new Vector3(0, shift * (scale.y/2), 0);
		}
		yield return new WaitForSeconds(shift);
		if (this.transform.localScale.y >= scale.y - shift) { // scale.y
			// waxing complete
			erect = true;
			foreach (GameObject e in apex) e.SetActive(true); // reveal apex
			SpawnOrb();
			cycle = true;
		} else
			StartCoroutine(Wax());
	}
	
	// obelisk contraction
	IEnumerator Wane() {
		if (orb != null) {
			Destroy(orb);
			orb = null;
		}
		foreach (GameObject e in apex) e.SetActive(false); // conceal apex
		if (this.transform.localScale.y > 0) {
			this.transform.localScale -= new Vector3(0, shift * scale.y, 0);
			this.transform.localPosition -= new Vector3(0, shift * (scale.y/2), 0);
		}
		yield return new WaitForSeconds(shift);
		if (this.transform.localScale.y <= shift) { // 0
			// waning complete
			erect = false;
			cycle = true;
		} else
			StartCoroutine(Wane());
	}
	
	// projectile creation
	void SpawnOrb() {
		orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		orb.name = "Orb";
		orb.renderer.material = (Material) Resources.Load("Soap Bubble", typeof(Material));
		orb.transform.localScale = new Vector3(.25f, .25f, .25f);
		orb.transform.localPosition = new Vector3(lok.x, lok.y + (scale.y/2) + .5f, lok.z);
		orb.AddComponent<OrbAI>();
		orb.AddComponent<Rigidbody>();
		orb.rigidbody.useGravity = false;
	}
	
}
