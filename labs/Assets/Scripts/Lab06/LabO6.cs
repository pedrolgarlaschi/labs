using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabO6 : MonoBehaviour {



	[SerializeField]
	private Transform m_start;
	[SerializeField]
	private GameObject m_planeRef;

	[SerializeField]
	private int m_lines;
	[SerializeField]
	private int m_cols;

	// Use this for initialization
	void Start () {

		for (int x = 0; x < m_cols; x++) {
			for (int z = 0; z < m_lines; z++) {

				Vector3 pos = m_start.transform.position;
				pos.x += x * 10;
				pos.z -= z * 10;
				GameObject go = Instantiate (m_planeRef, pos, Quaternion.identity);
				go.AddComponent<FlipPlane> ();

			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
