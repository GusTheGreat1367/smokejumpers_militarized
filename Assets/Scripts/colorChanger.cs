using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChanger : MonoBehaviour {

	// Use this for initialization
	public Color32 col = new Color32(75, 83, 32, 255);
	void Start () {
		Renderer objRenderer = GetComponent<Renderer>();

        // For Built-in Render Pipeline:
        objRenderer.material.SetColor("_Color", col);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
