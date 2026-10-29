using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class float3 // Vector3 remake, why not?
{
	public float x;
	public float y;
	public float z;

	public float3(float one, float two, float three)
	{
		this.x = one;
		this.y = two;
		this.z = three;
	}
}

public class Rect3D
{
	public float width; // the "X" value of a screen
	public float height; // the "Y" value of a screen
	public float length; // the "Z" value of a screen
	public float3 posi;

	public Rect3D(float width, float height, float length, Vector3 pos)
	{
		this.width = width;
		this.height = height;
		this.length = length;
		this.posi = new float3(pos.x, pos.y, pos.z);
	}
    public void draw(float x, float y, float z)
    {
        float area = 6 * (width * height);
        Gizmos.color = Color.blue;
        //Gizmos.DrawWireCube(float3_to_vector3(pos), new Vector3(width/2, height/2, length/2));
        Gizmos.DrawWireCube(float3_to_vector3(posi), new Vector3(x,y,z));

    }
    Vector3 float3_to_vector3(float3 fl3)
	{
		return new Vector3(fl3.x, fl3.y, fl3.z);
	}

    // bugged, there isn't a 3D rectangle drawn and I always get the "Craft in area" 
	public bool Contains(Vector3 po) // error here i believe
	{
		float3 pos = new float3(po.x, po.y, po.z);
        // pos is the GameObject's position
        float3 screen_pos = new float3(width/2, height/2, length/2);
        // the screen position - w1, w2 / h1, h2 / l1, 12 -> check paper notes
    
        // error here, it dont work ):
		if (!((pos.x < (posi.x - screen_pos.x)) && (pos.x > (posi.x + screen_pos.x))))
		{
            if (!((pos.y < (posi.y - screen_pos.y)) && (pos.y > (posi.y + screen_pos.y))))
            {
                if (!((pos.z < (posi.x - screen_pos.z)) && (pos.z > (posi.z + screen_pos.z))))
                {
                    Gizmos.color = Color.green;
                    Debug.Log("Craft in area");
                    return true;
                }
                else { Debug.Log("No craft"); return false; }
            }
            else { Debug.Log("No craft"); return false; }
        }
        else { Debug.Log("No craft"); return false; }
	}
}

public class MiscFunctions
{
	public GameObject Instantiate(GameObject obj, Vector3 pos, Quaternion rot)
    {
        GameObject new_obj = obj;
        new_obj.transform.position = pos;
        new_obj.transform.rotation = rot;
        return new_obj;
    }
}

public class Bullet
{
    //
}

public class Missile
{
    public GameObject prefab;
    public Vector3 startPos;
    public Vector3 endPos;
    public GameObject text;
    public float rate; // y = e^x is th equation for what rate/speed is

    public Missile(GameObject prefab, Vector3 startPos, Vector3 endPos, GameObject text)
    {
        this.prefab = prefab;
        this.startPos = startPos;
        this.endPos = endPos;
        this.text = text;
    }

    public GameObject fire()
    {
        GameObject missile = GameObject.Instantiate(prefab, startPos, prefab.transform.rotation);
        GameObject teckst = GameObject.Instantiate(text, endPos, Quaternion.identity);
        missile.tag = "Untagged";
        teckst.tag = "Untagged";
        Vector3 direction = (endPos - startPos).normalized;
        float distance = Vector3.Distance(startPos, endPos);
        float speed = 10f; // later change to the y = e^x equation
        float travelTime = distance / speed;

        missile.transform.position = startPos;
        missile.transform.rotation = Quaternion.LookRotation(direction);

        // Move the missile towards the target position over time
        return prefab;
    }

}
