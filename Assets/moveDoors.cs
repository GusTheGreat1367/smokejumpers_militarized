using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDoors : MonoBehaviour {

	// Use this for initialization
	public bool doorsMoved = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("move_doors")) // later make these animations/smooth with Mathf.MoveTowards(originalPosition, targetPosition, speed * Time.deltaTime)
		{
			if(!doorsMoved)
			{
				transform.position -= new Vector3(transform.forward.x, 0f, transform.forward.z) * 3f;
				doorsMoved = true;
			}
			else
			{
				transform.position += new Vector3(transform.forward.x, 0f, transform.forward.z) * 3f;
				doorsMoved = false;
			}
		}
	}
}
