using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab01 : MonoBehaviour {

	[SerializeField]
	private float m_radius 		= 200;
	[SerializeField]
	private int m_numSquares 	= 200;
	[SerializeField]
	private Material m_material;


	private List<CubeLab01> m_cubes;

	void Start () {

		Cursor.visible = false;

		CreateCubes ();
		StartCoroutine(StartRotation ());
	}
	
	private void CreateCubes()
	{
		m_cubes = new List<CubeLab01> ();

		float num = 360.0f / (float)(m_numSquares - 1);
		float ang = 0;

		for (int i = 0; i < m_numSquares; i++) {

			GameObject go = GameObject.CreatePrimitive (PrimitiveType.Cube);

			Vector3 pos = new Vector3 ();
			pos.x = Mathf.Cos (ang * Mathf.Deg2Rad) * m_radius;
			pos.y = Mathf.Sin (ang * Mathf.Deg2Rad) * m_radius;

			go.transform.position = pos;
			go.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, ang));

			CubeLab01 cube = go.AddComponent<CubeLab01> ();
			cube.Speed = Utils.FloatMap (ang, 0, 360, 3, 1);

			float rand = Random.Range (0.1f, 0.4f);
			go.transform.localScale = new Vector3( Random.Range (2, 6) , rand,rand);

			Destroy(go.GetComponent<BoxCollider> ());

			go.transform.SetParent (transform);

			Renderer r = go.GetComponent<Renderer> ();
			r.material = m_material;

			ang += num;

			m_cubes.Add (cube);
		
		}
	}


	private IEnumerator StartRotation()
	{
		
		foreach (CubeLab01 c in m_cubes) {
		
			c.Speed = 5;
			yield return new WaitForSeconds (0.01f);
		}
	}

	void Update()
	{
		m_radius = Utils.FloatMap (Input.mousePosition.x, 0, Screen.width, 1, 6);

		float num = 360.0f / (float)(m_numSquares - 1);
		float ang = 0;

		for (int i = 0; i < m_numSquares; i++) {

			CubeLab01 c = m_cubes [i];
			Vector3 pos = new Vector3 ();
			pos.x = Mathf.Cos (ang * Mathf.Deg2Rad) * m_radius;
			pos.y = Mathf.Sin (ang * Mathf.Deg2Rad) * m_radius;
			c.transform.position = pos; 

			ang += num;
		}
	}


}
