using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		float x = Mathf.Clamp(Input.mousePosition.x / Screen.width , 0.01f, 1);
		float rotY = x * 359;

		Vector3 rot = transform.eulerAngles;

		rot.y += (rotY - rot.y) * 0.3f;


		transform.rotation = Quaternion.Euler (rot);


	}
}
