using UnityEngine;
using System.Collections;

public class CamFollow2D : MonoBehaviour {

	float yLock = 4;
	public Transform target = null;
	
	// Update is called once per frame
	void Update () {
		if (target)
			transform.position = new Vector3(target.position.x, target.position.y+yLock, -15.0F);
	}
}
