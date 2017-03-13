using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereLab07 : MonoBehaviour {

	//private float m_speedY = 0;
	//private float m_accellY = 0;
	private Transform m_cube;
	//private Vector3 m_startPos;
	//private float m_sinRadius;
	//private Vector3 m_randRot;
	private Rigidbody m_body;

	private float m_forceY;
	private bool m_boom;
	private Vector3  m_sTo;

	void Awake () {
		m_cube = transform.GetChieldFromName<Transform> ("m_cube");
		float s = Random.Range (0.5f, 2);
		transform.localScale = Vector3.zero;
		m_sTo = new Vector3 (s, s, s);
		m_body = gameObject.AddComponent<Rigidbody> ();
		m_body.mass = s * 0.8f;

		m_forceY = s * 10;
	}

	void Start()
	{
		//§m_startPos = transform.position;
	}

	void OnCollisionEnter(Collision c)
	{
		if (!m_boom && c.gameObject.name == "m_cube" && c.gameObject != m_cube)
			Expode ();
	}

	public void SetMaterial(Material _mat)
	{
		Renderer r = m_cube.GetComponent<Renderer> ();
		r.material = _mat;
	}

	void FixedUpdate () {

		transform.localScale += (m_sTo - transform.localScale) * 0.3f;

		if(!m_boom)
			m_body.AddForce(new Vector3(0,m_forceY,0));


		if (m_boom)
			return;


		if (transform.position.y > 10)
			Expode ();



	}

	private void Expode()
	{
		m_boom = true;

		m_cube.SetParent (null);


		Rigidbody cr = m_cube.gameObject.AddComponent<Rigidbody> ();
	
			cr.AddForce(new Vector3(0,Random.Range(10,35) * 40,0));
			cr.mass = 2;
			Destroy (gameObject);

			m_cube.gameObject.AddComponent<CubeLab07>();
	}
}
