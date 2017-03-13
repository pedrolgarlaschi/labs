using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab05 : MonoBehaviour {

	[SerializeField]
	private int m_numParticles;
	[SerializeField]
	private CircleLab05 m_ref;

	private List<CircleLab05> m_circles;

	// Use this for initialization
	void Start () {

		CreateParticles ();
	}


	private void CreateParticles()
	{
		m_circles = new List<CircleLab05> ();

		for (int i = 0; i < m_numParticles; i++) {


			CircleLab05 c = Instantiate (m_ref, Vector3.zero, Quaternion.identity) as CircleLab05;

			float s = Random.Range (0.01f, 0.1f);

			c.transform.localScale = new Vector3 (s,s,s);
			m_circles.Add (c);

			c.OnCatch += (CircleLab05 _c) => {
				Catch(_c);
			};
		}

		for (int i = 0; i < m_numParticles; i++) {

			CircleLab05 c1 = m_circles [i];

			for (int j = 0; j < m_numParticles; j++) {

				CircleLab05 c2 = m_circles [j];

				if (c1 != c2)
					c1.AddEnemy (c2);


			}
		}
	}

	private void Catch(CircleLab05 _c)
	{
		m_circles.Remove (_c);

		foreach (CircleLab05 c in m_circles)
			c.Remove (_c);

		Destroy (_c.gameObject);

		if (m_circles.Count == 0)
			CreateParticles ();

	}

}
