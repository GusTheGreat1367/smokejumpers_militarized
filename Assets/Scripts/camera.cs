using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

	// Use this for initialization
	// camera cannon local position: (0, 5.15, -3.75) with no rotation
	// camera behind position: (0, 8, -20) with the rotation of (8.5, 0, 0)
	bool topDown = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("camera"))
		{
			// your camera movement code here
			if(topDown)
			{
				transform.localPosition = new Vector3(0, 8, -20);
				transform.localRotation = Quaternion.Euler(8.5f, 0, 0);
			}
			else
			{
				transform.localPosition = new Vector3(0, 5.15f, -3.75f);
				transform.localRotation = Quaternion.identity;
			}
			topDown = !topDown;
			
		}
	}
}
