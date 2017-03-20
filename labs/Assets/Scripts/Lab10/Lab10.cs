using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab10 : MonoBehaviour {

	[SerializeField]
	private int m_lin = 10;
	[SerializeField]
	private int m_col = 10;

	[SerializeField]
	private GameObject m_ref;

	[SerializeField]
	private Vector3 m_startPos;

	[SerializeField]
	private Material m_mat;
	[SerializeField]
	private Color m_startColor;
	[SerializeField]
	private Color m_endColor;

	[SerializeField]
	private Transform m_target;


	private List<Transform> m_spheres;
	private Dictionary<Transform , float> m_scale;
	private Dictionary<Transform , Vector3> m_pos;


	// Use this for initialization
	void Start () {
		CreateSpheres ();

	}

	void Update()
	{
		foreach(Transform t in m_spheres)
		{
			Vector3 dist = m_target.position - t.position;
			float mag = dist.magnitude;

			Vector3 posTo = m_pos [t];
			Vector3 dir = posTo - m_target.position;

			mag = Mathf.Clamp (mag, 0, 5);

			float map = Utils.FloatMap (mag , 0, 5, 0, 1);

			float scale =  (1 -map) * m_scale[t] + 0.0f;

			t.localScale += (new Vector3 (scale, scale, scale) - t.localScale) * 0.5f;

			Renderer r = t.GetComponent<Renderer> ();
			r.material.color = (map * m_startColor) + ((1 -map) * m_endColor);


			mag = Mathf.Clamp (dir.magnitude, 0, 5);
			map = Utils.FloatMap (mag , 0, 5, 0, 1);

			posTo = (dir * map);
			t.position += (posTo - t.position) * 0.3f;
		}
	}

	private void CreateSpheres()
	{
		m_spheres = new List<Transform> ();
		m_scale = new Dictionary<Transform, float> ();
		m_pos = new Dictionary<Transform, Vector3> ();

		for(int x =  0 ; x < m_col ;x++)
		{
			for(int z =  0 ; z < m_lin ;z++)
			{
				GameObject go = Instantiate (m_ref);


				Vector3 pos = new Vector3 (x, 0, z);
				pos += m_startPos;

				go.transform.position = pos;
				go.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);

				Renderer r = go.GetComponent<Renderer> ();
				r.material = m_mat;

				m_spheres.Add (go.transform);

				m_scale.Add(go.transform , Random.Range(0.5f,2.5f));

				m_pos.Add (go.transform, pos);

				go.transform.SetParent (transform);
			}
		}
	}
	

}
