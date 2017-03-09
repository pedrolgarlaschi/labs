using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab02 : MonoBehaviour {

	[SerializeField]
	private Material m_material;

	[SerializeField]
	private int m_lines;

	[SerializeField]
	private int m_cols;

	[SerializeField]
	private Vector3 m_startPos;



	private List<GameObject> m_cubes;

	void Start () {

		Cursor.visible = false;

		CreateCubes ();
	}


	void Update()
	{
		float ang = Utils.FloatMap(Mathf.Sin (Time.time * 0.5f)  , -1,1 , -0.2f,2);Utils.FloatMap (Input.mousePosition.x, 0, Screen.width, 0, 3);

		int i = 0;
		float ap = 360 / (m_lines - 1);


		for (int x = 0; x < m_cols; x++) {
				for (int y = 0; y < m_lines; y++) {


				float angle = ang * ap * y * Mathf.Deg2Rad;

				GameObject go = m_cubes[i];

				Vector3 pos = new Vector3 ();
				pos.x = x * 1;
				pos.z = Mathf.Cos (angle) * y * 0.7f;
				pos.y = Mathf.Sin (angle) * y * 0.7f;

				pos += m_startPos;


				go.transform.position = pos;

				go.transform.rotation = Quaternion.Euler (new Vector3 (ang * Mathf.Rad2Deg , 0, 0));

				i++;

			}
		}
	}

	private void CreateCubes()
	{
		m_cubes = new List<GameObject> ();

		for (int x = 0; x < m_cols; x++) {
			for (int y = 0; y < m_cols; y++) {

				GameObject go = GameObject.CreatePrimitive (PrimitiveType.Cube);

				float rand = Random.Range (0.5f, 3.0f);
				go.transform.localScale = new Vector3 (1, 1, rand);

				Destroy (go.GetComponent<BoxCollider> ());

				go.transform.SetParent (transform);

				Renderer r = go.GetComponent<Renderer> ();
				r.material = m_material;

				go.transform.position = new Vector3 (x * 1f, y * 1f, 0) + m_startPos;

				m_cubes.Add (go);
			}
		}
	}
}
