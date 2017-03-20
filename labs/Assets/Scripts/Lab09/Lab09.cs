using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab09 : MonoBehaviour {

	[SerializeField]
	private Material 		m_mat;
	[SerializeField]
	private int	 			m_num = 10;


	void Start()
	{
		CreateLines ();
	}

	private void CreateLines()
	{

		for (int i = 0; i < m_num; i++) {
		
			GameObject go = new GameObject ("line" + (i + 1));
			go.transform.SetParent (transform);

			SphereLine s = go.AddComponent<SphereLine> ();
			s.SetMaterial (m_mat);

		}
	}


}
