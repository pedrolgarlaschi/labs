using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLab01 : MonoBehaviour {


	public float Speed{ set; get;}

	private Renderer m_rend;
	private float m_ang;

	void Start()
	{
		m_rend = gameObject.GetComponent<Renderer> ();
	}

	void Update () {

		transform.Rotate (new Vector3 (0, Speed, 0));
		float r = transform.eulerAngles.y;

		m_rend.material.color = Color.HSVToRGB (Utils.FloatMap(r,0,360, 0,255) / 255.0f , 172 / 255.0f , 223/ 255.0f);



	}
}
