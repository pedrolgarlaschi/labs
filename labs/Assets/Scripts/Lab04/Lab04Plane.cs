using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab04Plane : MonoBehaviour {

	[SerializeField]
	private bool m_createMag;

	private float[] size;
	private Mesh mesh;
	private Transform m_magneto;


	private Vector2 m_startSeedX;
	private Vector2 m_sumX;

	private Vector2 m_startSeedY;
	private Vector2 m_sumY;

	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
		size = new float[vertices.Length];

		for(int i = 0 ; i < vertices.Length;i++)
			size[i] = Random.Range (0.5f, 5);

		if (!m_createMag)
			return;

		GameObject go = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		go.GetComponent<MeshRenderer> ().enabled = false;
		m_magneto = go.transform;
		Vector3 posTo  = transform.position;
		posTo.y = Random.Range (-2.5f, 2.5f);
		posTo.x = Random.Range (-25f, 25f);
		m_magneto.position = posTo;

		m_startSeedX = new Vector2(Random.Range(0,100),Random.Range(0,1000));
		m_sumX = new Vector2(Random.Range(0,100),Random.Range(0,100)) * 0.0001f;

		m_startSeedY = new Vector2(Random.Range(0,100),Random.Range(0,1000));
		m_sumY = new Vector2(Random.Range(0,100),Random.Range(0,100)) * 0.0001f;


	}
	
	void Update() {

		if (!m_magneto)
			return;
			
		UpdateMesh ();
		UpdatePos ();
	}

	private void UpdatePos()
	{
		float nX = Mathf.PerlinNoise (m_startSeedX.x, m_startSeedX.y);
		m_startSeedX += m_sumX;

		float nY = Mathf.PerlinNoise (m_startSeedY.x, m_startSeedY.y);
		m_startSeedY += m_sumY;

		Vector3 pos = m_magneto.position;
		pos.x = Utils.FloatMap (nX, 0, 1, -25, 25);
		pos.y = Utils.FloatMap (nY, 0, 1, 0, 5);
		m_magneto.position = pos;

	}

	private void UpdateMesh()
	{
		Vector3 magPos = m_magneto.position;
		Vector3[] vertices = mesh.vertices;
		int i = 0;
		while (i < vertices.Length) {

			Vector3 pos = transform.position + vertices [i];
			Vector3 dir = magPos - pos;


			float mag = dir.magnitude;
			mag = Mathf.Clamp (mag, 0, 6);


			pos.y = Utils.FloatMap (mag, 0, 6, 8, 0);
			


			pos -= transform.position;
			vertices [i] += (pos - vertices [i]) * 0.1f;

			i++;
		}
		mesh.vertices = vertices;
		mesh.RecalculateBounds();
	}
}
