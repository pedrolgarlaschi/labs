using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereLine : MonoBehaviour {

	private LineRenderer m_line;
	private Vector3[] m_points;
	private Vector3 m_sPoint;
	private List<Vector3> m_path;
	private float m_ang;
	private float m_sizeAng;
	private int m_cVert;
	private int m_numVert = 100;
	private int m_side = 0;
	private int m_max = 0;
	private int m_min = 0;
	private float m_speed;

	private bool m_rand;
	private float m_maxAngle;

	private LineRenderer Line
	{
		get{ 
			if (m_line == null) {
				m_line = gameObject.AddComponent<LineRenderer> ();
				m_line.startWidth = 0.2f;
				m_line.endWidth = 0.2f;
				m_line.useWorldSpace = false;

				m_side = Random.Range (0, 100) > 50 ? 1 : -1;

				m_min = m_side == 1 ? 0 : -Random.Range(10,180);
				m_max = m_side == 1 ? Random.Range(10,120) : 0;

				m_rand = Random.Range (0, 10) > 5 ? true : false;

				m_speed = Random.Range (1.5f, 3.0f);

			}

			return m_line;
		}

	}

	void Start()
	{
		m_path = new List<Vector3>();

		m_sizeAng = 360 / (m_numVert - 1);

		transform.rotation = Quaternion.Euler(new Vector3(Random.Range(0,360) ,Random.Range(0,30) ,0 ));
	}
		
	public void SetMaterial(Material _mat)
	{
		Line.material = _mat;
	}


	void Update()
	{
		m_ang = Mathf.Clamp (m_ang , m_min , m_max);
		m_ang += m_side * m_speed;


		int i = Mathf.Abs((int)(m_ang / m_sizeAng));

		Vector3 pos = Vector3.zero;//new Vector3 (, 0, Mathf.Sin (m_ang * Mathf.Deg2Rad) * 2); 
		pos.x = Mathf.Cos (m_ang * Mathf.Deg2Rad) * 2;
		pos.z = Mathf.Sin (m_ang * Mathf.Deg2Rad) * 2;

		if (i == 0)
			transform.position = pos;
		else if(i == 1)
			m_line.SetPosition (0 , pos);

		m_line.numPositions = i + 1;
		m_line.SetPosition (i , pos);

	}
		
}
