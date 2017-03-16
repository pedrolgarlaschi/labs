using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab08 : MonoBehaviour {

	[SerializeField]
	private int m_numLines;
	[SerializeField]
	private Material m_lineMate;
	[SerializeField]
	private Drop m_dropRef;
	[SerializeField]
	private GameObject m_particles;

	private List<LineCircle> m_circles;

	private float m_lastDrop;
	private float m_timeToDrip;


	void Start () {


		CreateLines ();
	}


	
	private void CreateLines()
	{
		m_circles = new List<LineCircle> ();

		for (int i = 0; i < m_numLines; i++) {
		
			GameObject go = new GameObject ();
			go.transform.SetParent (transform);


			LineCircle l = go.AddComponent<LineCircle> ();
			l.Radius = (i + 1) * Random.Range(10,20) / 10.0f;
			l.SetMaterial (m_lineMate);

			m_circles.Add (l);
		}
	}

	void Update()
	{
		if (Input.GetButtonDown ("Fire1"))
			AddDropForce ();

		ForceToCenter ();

		if(Time.time > m_lastDrop + m_timeToDrip)
			CreateDrop ();
	}

	private void CreateDrop()
	{
		m_lastDrop = Time.time;
		m_timeToDrip = Random.Range (1, 3);


		Vector3 pos = new Vector3 (0, 50, 0);
		Drop d = Instantiate (m_dropRef, pos, Quaternion.identity) as Drop;
		d.OnHitFloor += (Drop _d) => {

			AddDropForce();

		};
	}

	private void AddDropForce()
	{
		for (int i = 0; i < m_numLines; i++) {

			LineCircle l = m_circles [i];
			l.AddForce (new Vector3 (0, -40.0f / (float)(i + 1), 0));

		}
	}


	private void ForceToCenter()
	{
		for (int i = 0; i < m_numLines; i++) {

			LineCircle l = m_circles [i];

			Vector3 d = Vector3.zero - l.transform.position;
			float mag = Mathf.Abs(l.transform.position.y * 0.5f);
			d.Normalize ();



			l.AddForce (d * mag);

		}
	}


}
