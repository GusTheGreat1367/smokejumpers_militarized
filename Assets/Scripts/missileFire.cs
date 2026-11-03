using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileFire : MonoBehaviour { // attach to the recticle NOT THE HELI

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("fire"))
		{
			Debug.Log("Fire button pressed");
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(transform.position);
			if(Physics.Raycast(ray, out hit))
			{
				// Do something with the hit point, e.g., spawn a missile at hit.point
				Debug.Log("Fire");
				Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red); // fix this, later make this fire the shader that is a missile that explodes on impact to a target
			}
		}
	}
}
