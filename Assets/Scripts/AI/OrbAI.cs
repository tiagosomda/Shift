using UnityEngine;
using System.Collections;

/* OrbAI - v0.8
 *    Basic targeting system for obelisk projectiles, aka
 *    orbs. As of now, destroys (this) upon impact.
 * 
 *    v0.8 ~ projectile trail & life cycle
 * 
 * CoScripts: ObeliskAI
 * Scribes: Ryan Wood
 */
public class OrbAI : MonoBehaviour {

	bool sentient = false;		// active?
	float speed = 1.5f;			// (of) projectile
	GameObject target = null, trail = null;
	
	void FixedUpdate() {
		if (sentient) {
			this.transform.LookAt(target.transform);
			this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
		}
	}
	
	// releases projectile
	public void Activate(GameObject target, float burst) {
		// trail creation
		trail = new GameObject("Trail");
		TrailRenderer c = trail.AddComponent<TrailRenderer>();
		c.material = (Material) Resources.Load("Fairy Dust", typeof(Material));
		c.startWidth = .125f;
		c.endWidth = .125f;
		trail.transform.localPosition = this.transform.position;
		trail.transform.parent = this.transform;
		
		// acquire target
		this.target = target;
		this.transform.LookAt(target.transform);
		this.rigidbody.AddRelativeForce(Vector3.up * burst * Time.deltaTime, ForceMode.VelocityChange);
		sentient = true;
	}
	
	void OnCollisionEnter() {
		// [if (obj.Equals(target)) destroy player]
		
		trail.transform.parent = null;
		trail.GetComponent<TrailRenderer>().autodestruct = true;
		
		Destroy(this.gameObject);
	}	
	
}
