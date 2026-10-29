using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class missile : MonoBehaviour {

    //  no max distance, the Rect3D will cull shaders and GameObjects

    public GameObject Instantiate(GameObject obj, Vector3 pos, Quaternion rot)
    {
        GameObject new_obj = obj;
        new_obj.transform.position = pos;
        new_obj.transform.rotation = rot;
        return new_obj;
    }

	public GameObject heli;
	public GameObject self;
	public float t = 1f;

	IEnumerator fire_missile()
	{
		GameObject new_missile = Instantiate(self, self.transform.position, self.transform.rotation); // create a copy of the missile
		float y = Mathf.MoveTowards(1.33f, 0f, 0.0000000000005f) * Time.deltaTime;
		new_missile.transform.position = new Vector3(new_missile.transform.position.x, y, new_missile.transform.position.z);
		float x = Mathf.MoveTowards(new_missile.transform.position.x, new_missile.transform.position.x + 30, 0.000000000005f) * Time.deltaTime;
		transform.position = new Vector3(x, new_missile.transform.position.y, new_missile.transform.position.z);

		yield return new WaitForSeconds(1f);
	}

	// Use this for initialization
	void redirect()
	{
		transform.position = new Vector3(heli.transform.position.x, 1.33f, heli.transform.position.z);
		Vector3 rot = heli.transform.rotation.eulerAngles;
		transform.rotation = Quaternion.Euler(0, rot.y+90, 90);
	}
	void Start () {
		redirect();
	}
	
	// Update is called once per frame
	void Update () {
        self.SetActive(false);
		Time.timeScale = t;
		if (!Input.GetButtonDown("missile"))
		{
			redirect();
		}     
		else
		{
			StartCoroutine(fire_missile());
		}
	}
}
