using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Lab12 : MonoBehaviour {

	[SerializeField]
	private int m_num = 100;
	[SerializeField]
	private Vector3 m_bounds;
	[SerializeField]
	private Vector3 m_center;
	[SerializeField]
	private ParticleSystem m_system;

	private Flock m_flock;
	private List<Boid> m_boids;
	private ParticleSystem.Particle[] m_particles;

	void Start () 
	{
		m_boids = new List<Boid> ();

		GameObject f = new GameObject ("Flock");
		m_flock = f.AddComponent<Flock> ();
		for (int i = 0; i < m_num; i++)
			AddBoid ();


	}
		
	private void AddBoid()
	{
		Boid b = new Boid ();
		b.Init (m_bounds , m_center);
		m_flock.AddBoid(b);
		m_boids.Add (b);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (m_center, m_bounds);
	}


	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space))
			ExplodeBoid ();
	}


	private void ExplodeBoid()
	{
		for (int i = 0; i < m_num; i++)
			m_boids [i].ApplyForce (Utils.RandomVec (-100, 100) * 100.0f);
	}

	private void LateUpdate()
	{
		InitializeIfNeeded();

		int numParticlesAlive = m_system.GetParticles(m_particles);

		for (int i = 0; i < numParticlesAlive; i++) {

			m_particles [i].position = m_boids [i].Location;
		}
			

		m_system.SetParticles(m_particles, numParticlesAlive);
	}

	void InitializeIfNeeded()
	{
		if (m_system == null) {
			
			m_system = GetComponent<ParticleSystem> ();

		}

		if (m_particles == null || m_particles.Length < m_system.maxParticles)
			m_particles = new ParticleSystem.Particle[m_system.maxParticles]; 
	}
}

