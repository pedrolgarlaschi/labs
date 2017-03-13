using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeLab07 : MonoBehaviour
{
	public CubeLab07 ()
	{
	}


	void Update()
	{
		Vector3 pos = transform.position;
		if (pos.y < -12)
			Destroy (gameObject);


	}
}


