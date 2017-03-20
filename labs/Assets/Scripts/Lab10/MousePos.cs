using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {


		Vector3 pos = transform.position;

		pos.x = Utils.FloatMap (Input.mousePosition.x, 0, Screen.width, -2, 2);
		pos.z = Utils.FloatMap (Input.mousePosition.y, 0, Screen.height, -5, 10);


		transform.position += (pos - transform.position) * 0.3f;
	}
}
