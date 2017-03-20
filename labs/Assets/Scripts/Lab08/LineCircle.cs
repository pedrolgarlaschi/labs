using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineCircle : MonoBehaviour {

	private int 			m_numSeg = 100;
	private LineRenderer 	m_line;
	private Vector3 		m_speed;
	private Vector3 		m_accell;
	private float 			m_fric = 0.95f;




	public float Radius { get; set;}

	void Awake()
	{


		m_line = gameObject.GetComponent<LineRenderer> ();
		m_line.numPositions = m_numSeg;

		float thick = Random.Range (1, 10) / 20.0f;
		m_line.startWidth = thick;
		m_line.endWidth = thick;
		m_line.useWorldSpace = false;
		Radius = 1;

	}

	void Start()
	{
		CreateLine ();
	}

	private void CreateLine()
	{
		float n = Random.Range (1, 10);

		float a = 0;
		float ang = 360.0f / (float)(m_numSeg - 1);
		for (int i = 0; i < m_numSeg; i++) {

			Vector3 pos = Vector3.zero;
			pos.x = Mathf.Cos (a * Mathf.Deg2Rad) * Radius;
			pos.z = Mathf.Sin (a * Mathf.Deg2Rad) * Radius;



			m_line.SetPosition (i, pos);

			a += ang;
		}
	}

	public void SetMaterial(Material _mat)
	{
		m_line.material = _mat;
	}

	public void AddForce(Vector3 _f)
	{
		m_accell += _f / 10.0f;
	}

	void Update()
	{
		m_speed += m_accell;
		transform.position += m_speed;

		m_speed *= m_fric;
		m_accell *= 0;

	}



		

}
