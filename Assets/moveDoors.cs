using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDoors : MonoBehaviour {

	// Use this for initialization
	public bool doorsMoved = false;
	public float rate = 3f;
	bool move = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("move_doors")) // bugged
		{
			doorsMoved = !doorsMoved;
			move = true;
		}
		if(move && doorsMoved)
		{
			var pos = Mathf.MoveTowards(transform.localPosition.z, rate, rate * Time.deltaTime);
			transform.localPosition = new Vector3(0f, 0f, pos);
		}
		else if(move && !doorsMoved)
		{
			var pos = Mathf.MoveTowards(transform.localPosition.z, 0f, rate * Time.deltaTime);
			transform.localPosition = new Vector3(0f, 0f, pos);
		}
		if(transform.localPosition.z == rate || transform.localPosition.z == 0f)
		{
			move = false;
		}
	}
}
