using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereLab03 : MonoBehaviour {


	private Rigidbody m_body;
	private float 		m_startScale;
	public float Scale
	{
		get{return m_startScale; }
	}

	void Awake()
	{
		float s = Random.Range (0.3f, 3);
		m_startScale = transform.localScale.x;
		m_body = gameObject.GetComponent<Rigidbody> ();
	}


}
