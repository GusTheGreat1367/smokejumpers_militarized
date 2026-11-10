using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO (top > down = importance):

// make the rotations go to the "else" block in the Update() instead of if the key is pressed go to rotaion then reset rotation to 0 anfter the key is released
// make the movement speed increase/decrease and be more realistic
// for more realism, make it so there's motion, meaning that if you are ex: going left and release the left arrow, you continue moving left until the motion == 0
// fix the door movement
// other movement bugs are prevalent like the barrel roll one
// shaders for cannon fire
// Rect3D isn't needed anymore
// add missiles, targeting from where you clicked the mouse on screen - click on screen > spawn a missile dropping from heli > vector to mouse click position and back of Rect3D > explode missile when you hit back of Rect3D
// make it so the heli's angle can be the backward and side 30 degrees when you press ex: s and a



public class HeliMovement : MonoBehaviour 
{

	// Use this for initialization
	public GameObject heli;
	public GameObject prefab; // the missile prefab
	public float speed = 2f;
	public float moveSpeed = 10f;
	//Rect3D screen = new Rect3D(55f, 25f, 40f, new Vector3(0,0,0));

	public List<GameObject> GOs;

	void Start ()
	{
		prefab.SetActive(false);
	}
    void Update () 
	{
		Vector3 rotNow = transform.rotation.eulerAngles;
		Vector3 newRot = new Vector3(0, transform.rotation.eulerAngles.y, 0);
		/*
		var xxx = transform.rotation.eulerAngles.x;
		var zzz = transform.rotation.eulerAngles.z;
		xxx = Mathf.Clamp(xxx, 0f, 30f);
		zzz = Mathf.Clamp(zzz, 0f, 30f);
		Vector3 rotee = new Vector3(xxx, transform.rotation.eulerAngles.y, zzz);
		Quaternion roteeee = Quaternion.Euler(rotee);
		transform.rotation = roteeee;
		*/
		
		if(Input.GetButton("rotate_right"))
		{
			Vector3 rot = new Vector3(0, 5f, 0)  * moveSpeed * Time.deltaTime;
			newRot.y += rot.y;
		}
		if(Input.GetButton("rotate_left"))
		{
			Vector3 rot = new Vector3(0, -5f, 0) * moveSpeed * Time.deltaTime;
			heli.transform.rotation *= Quaternion.Euler(rot);
			newRot.y += rot.y;
		}
		if(Input.GetButton("up")){
			Vector3 rate = new Vector3(0, 1f, 0) * moveSpeed * Time.deltaTime; 
			heli.transform.position += rate;
		}
		if(Input.GetButton("down")){
			Vector3 rate = new Vector3(0, -1f, 0) * moveSpeed * Time.deltaTime;
			heli.transform.position += rate;
		}
        if(Input.GetButton("Right"))
        {
            //Vector3 rate = new Vector3(1f, 0, 0) * moveSpeed * Time.deltaTime; 
			heli.transform.position += moveRight();
			//tiltRight();
			Vector3 rotN = TiltSide();
			//Vector3 rot = new Vector3(rotN.z, rotN.y, rotN.x);
			//float xx = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.x, 30f, 1f);
			float zz = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, -30f, speed/2); // why doesn't this work??!!
			newRot.z += zz;
        }
        if(Input.GetButton("Left"))
        {
            //Vector3 rate = new Vector3(-1f, 0, 0) * moveSpeed * Time.deltaTime;
			heli.transform.position += moveLeft();
			//tiltLeft();
			Vector3 rotN = TiltSide();
			//Vector3 rot = new Vector3(rotN.z, rotN.y, rotN.x);
			//float xx = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.x, rotN.z, 1f);
			float zz = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, 30f, speed/2);
			newRot.z += zz;
        }
		if(Input.GetButton("forward"))
		{
			//Vector3 rate = new Vector3(0, 0, 1f) * moveSpeed * Time.deltaTime;
			heli.transform.position += moveForward();
			Vector3 rotN = TiltSide();
			float zz = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.x, 30f, speed/2);
			newRot.x += zz;
		}
		if(Input.GetButton("backward")) 
		{
			//Vector3 rate = new Vector3(0, 0, -1f) * moveSpeed * Time.deltaTime;
			heli.transform.position += moveBackward();
			Vector3 rotN = TiltSide();
			float zz = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.x, -30f, speed/2);
			newRot.x += zz;
		}
		else
		{
			float x = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.x, 0f, 1f); // or speed * Time.deltaTime
			float z = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, 0f, 1f);
			Quaternion r = Quaternion.Euler(new Vector3(x, transform.rotation.eulerAngles.y, z));
			transform.rotation = r;
		}
		// not really working, allows the full rotation, but when you aren't holding the key, the rotation is instantly set to 0 instead of going to the "else" block
		Quaternion r2 = Quaternion.Euler(newRot);
		transform.rotation = r2;
        // add the shoot and fire commands here, just make a class for each in Classes.cs and then start their coroutines from code like "StartCoroutine(bullet.fire())"



		/*
        foreach (var gameObject in GOs) // inefficient to do each update, but for now, it is needed
		{
			if (!screen.Contains(gameObject.transform.position))
			{
				if(gameObject.tag != "heli")
				{
					Destroy(gameObject);
				}
				else
				{
					speed = 0; // if the helicopter is at the out of bounds area, it wont be able to move further in that direction
				}
			}
		}
		*/
	}
	Vector3 TiltSide() // returns for the left side, do negative return value for right
	// really bugged
	{
		float weight = transform.rotation.eulerAngles.y; // the more the y rotation value is, the larger split there will be between changing the x and z rotation values
		//float weight = 45f;
			// max rotation angle will be 30 degrees
			// weight is between 0 and 360 degrees
			// if weight = 90/270 degrees, rot.x will be 30/-30
			// if weight = 0/180 degrees, rot.z will be 30/-30
		float x = 0;
		float z = 0;
		Vector2 angle = new Vector2(x, z);
				// the equation(s), https://www.desmos.com/calculator/p3trqrec0h:
				// the desmos is the graph mapped to the weight (or vehicle rotation (0>360)) and when you do the equation, do it to the 90 degree mapped equation, divide the returned x value by 90, then do the steps below equations
				
				// we have the y value (weight) and just find what equation its apart of and solve for x, then find z from that

				// 1. (X & Y are + ( 90 > weight > 0 ))      ->  x = -y + 1 { 0 < x < 1 }
				// 2. (X is - ( 360 > weight > 270 ))        ->  x = y - 1 { -1 < x < 0 }
				// 3. (Y is - ( 270 > weight > 180 ))        ->  x = y + 1 { 0 < x < 1 }
				// 4. (X & Y are - ( 180 > weight > 90 ))    ->  x = -y - 1 {-1 < x < 0 } 

				// for the equations the return value (x) is between 0 and 1, and then y = 1 - x, then the angle.x = x*30 and the angle.y = y*30
				// find a way to map the weight to 1 
		float y = weight;
		if(weight >= 0 && weight < 90) // equation 1
		{
			x = (-y + 90)/90; // get the x value from 0 to 1
			z = 1-x;
			angle = new Vector2(x*30, z*30);
		}
		else if(weight >= 90 && weight < 180) // equation 4
		{
			x = (-y - 90)/90; // get the x value from 0 to 1
			z = 1-x;
			angle = new Vector2(x*30, z*30);
		}
		else if(weight >= 180 && weight < 270) // equation 3
		{
			x = (y + 90)/90; // get the x value from 0 to 1
			z = 1-x;
			angle = new Vector2(x*30, z*30);
		}
		else if(weight >= 270 && weight < 360) // equation 2
		{
			x = (y - 90)/90; // get the x value from 0 to 1
			z = 1-x;
			angle = new Vector2(x*30, z*30);
		}
		Vector3 rot = new Vector3(angle.x, weight, angle.y);
		Vector3 rot_fix = new Vector3(30f, weight, 0f);
		return rot_fix;
	}


	// the transform. stuff means with the rotation of the heli, you always go down and forward/backward/left/right
	Vector3 moveLeft() { Vector3 direction = new Vector3(transform.right.x, 0, transform.right.z) * -moveSpeed * Time.deltaTime; return direction; }
	Vector3 moveRight() { Vector3 direction = new Vector3(transform.right.x, 0, transform.right.z) * moveSpeed * Time.deltaTime; return direction; }
	Vector3 moveForward() { Vector3 direction = new Vector3(transform.forward.x, 0, transform.forward.z)* moveSpeed * Time.deltaTime; return direction; }
	Vector3 moveBackward() { Vector3 direction = new Vector3(transform.forward.x, 0, transform.forward.z) * -moveSpeed * Time.deltaTime; return direction; }

}