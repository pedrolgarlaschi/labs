using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {
	private List<Boid> m_boids;

	void FixedUpdate() {

		foreach (Boid b in m_boids) {
			b.Run(m_boids);
		}
	}

	public void AddBoid(Boid b) {

		if(m_boids == null)
			m_boids = new List<Boid>();

		m_boids.Add(b);
	}

	public void DestroyBoyds(int _num)
	{
		for (int i = 0; i < _num; i++) {
			m_boids.RemoveAt (i);
		}
	}
		
}