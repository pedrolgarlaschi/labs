using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lab11 : MonoBehaviour {


	private GameObject 		m_obj;
	private Mesh			m_mesh;
	private MeshRenderer	m_rend;
	private MeshFilter		m_filter;

	private List<Vector3> 	m_vertices;
	private List<Vector3> 	m_normals;
	private List<int> 		m_tri;
	private List<Vector3>	m_line;

	[SerializeField]
	private Material	m_mat1;
	[SerializeField]
	private Material	m_mat2;

	[SerializeField]
	private int m_ringNum = 5;
	[SerializeField]
	private float m_rad = 0.5f;
	[SerializeField]
	private int m_splineSeg = 100;

	 
	void Awake()
	{
		m_obj = new GameObject ("go");
		m_mesh = new Mesh ();
		m_rend = m_obj.AddComponent<MeshRenderer> ();
		m_filter = m_obj.AddComponent<MeshFilter> ();

		m_filter.mesh = m_mesh;
	}


	void Start () {

		m_line = new List<Vector3> ();
		Vector3[] line = new Vector3[4]{ new Vector3 (0, -2, -2), new Vector3 (2, 2, 1), new Vector3 (0, 4, 4), new Vector3 (-2, 8, 0) };


		for (int i = 0; i < m_splineSeg; i++) {

			m_line.Add(iTween.PointOnPath(line,(float)i/(float)m_splineSeg));
		
		}



		CreateTrunk (m_line.ToArray());
	}


	private void CreateTrunk(Vector3[] _pos)
	{

		float angPlus = 360.0f / (float)(m_ringNum - 1);
		
		int len = _pos.Length;

		m_vertices = new List<Vector3> ();


		for (int i = 0; i < len; i++) {

			Vector3 node = _pos [i];
			Vector3 next = i < len - 1 ? _pos[i + 1] : _pos[i - 1];
			Vector3 ant = i > 0 ? _pos[i - 1] : Vector3.zero;

			Vector3 dir1 = next - node;
			Vector3 dir2 = node - ant;

			GameObject go = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			go.transform.localScale = Vector3.zero;
			go.transform.position = node;
			go.transform.forward = dir1;

			go.GetComponent<MeshRenderer> ().sharedMaterial = m_mat1;

			go.transform.DOScale (0.1f,0.1f).SetDelay(i * 0.05f);

		
			float ang = 0;
			for (int j = 0; j < m_ringNum; j++) {


				Vector3 pos = _pos [i];


				Vector3 v = Quaternion.LookRotation (dir1) * new Vector3 (Mathf.Cos (ang * Mathf.Deg2Rad) * m_rad, Mathf.Sin (ang * Mathf.Deg2Rad) * m_rad);
				pos += v;

				m_vertices.Add (pos);

				GameObject go2 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
				go2.transform.localScale = Vector3.zero;
				go2.transform.position = pos;
				go2.transform.DOScale (0.06f,0.1f).SetDelay(i * 0.05f);
				ang += angPlus;

				go2.GetComponent<MeshRenderer> ().sharedMaterial = m_mat2;
			}
		
		}

		m_mesh.vertices = m_vertices.ToArray ();
	}

	/*void OnDrawGizmos()
	{
		if (m_line == null)
			return;
		Gizmos.color = Color.red;
		foreach (Vector3 v in m_line)
			Gizmos.DrawSphere (v, 0.05f);

		Gizmos.color = Color.white;
		foreach (Vector3 v in m_vertices)
			Gizmos.DrawSphere (v, 0.01f);
	}*/
	

}



/*


for (int j = 0; j < nbSideVerts; j++)
  {
      vertices[nbCentVerts + j + l] = vertices[k] + (Mathf.Sin(j * angle) * a + Mathf.Cos(j * angle) * b ) * 0.1f;
  }

*/
