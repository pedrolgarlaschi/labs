using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid {

	private Vector3 m_location;
	public Vector3 Location
	{
		get{ 
			return m_location;
		}
	}


	private Vector3 m_velocity;
	public Vector3 Velocity
	{
		get{ 
			return m_velocity;
		}
	}

	public Quaternion Rotation
	{
		get{ 
		
			return Quaternion.LookRotation (-m_velocity);
		
		}
	}


	private Vector3 m_acceleration;
	private float m_rad;
	private float m_maxforce;
	private float m_maxspeed;
	private Vector3 m_bounds;
	private Vector3 m_center;
	private Vector3 m_seedX;
	private Vector3 m_seedY;
	private Vector3 m_seedZ;
	private Vector3 m_noiseFactor;




	public void Init(Vector3 _bounds , Vector3 _center)
	{
		m_rad = 0.1f;
		m_bounds = _bounds;
		m_center = _center;

		m_seedX = Utils.RandomVec (0, 100) / 100.0f;
		m_seedY = Utils.RandomVec (0, 100) / 100.0f;
		m_seedZ = Utils.RandomVec (0, 100) / 100.0f;
		m_noiseFactor = Utils.RandomVec (0, 100) / 1000.0f;

		m_acceleration = new Vector3(0, 0 , 0);

		m_velocity = new Vector3(Random.Range(-100,100) / 10.0f, Random.Range(-100,100) / 10.0f , Random.Range(-100,100) / 10.0f );

		m_location = new Vector3(0, 0,0);

		m_maxspeed = 0.04f;
		m_maxforce = 0.02f;

		m_location = m_center + new Vector3(Random.Range(-_bounds.x,_bounds.x) , Random.Range(-_bounds.y,_bounds.y) , Random.Range(-_bounds.z,_bounds.z));
	}

	public void Run(List<Boid> _boids) {
		Flock(_boids);
		UpdateBehaviour();
		Borders();
	}

	public void ApplyForce(Vector3 _force) {
		m_acceleration += _force;
	}

	
	private void Flock(List<Boid> _boids) {
		Vector3 sep = Separate(_boids);  
		Vector3 ali = Align(_boids);     
		Vector3 coh = Cohesion(_boids);  
		Vector3 noise = Noise ();

		sep *= 0.2f;
		ali *= 0.02f;
		coh *= 0.001f;
		noise *= 0.001f;

		ApplyForce(sep);
		ApplyForce(ali);
		ApplyForce(coh);
		ApplyForce(noise);
	}




	
	private void UpdateBehaviour() {
		m_velocity += m_acceleration;

		if (m_velocity.magnitude > m_maxspeed)
			m_velocity = m_velocity.normalized * m_maxspeed;

		m_location += m_velocity;
		m_location += m_velocity;

		m_acceleration *= 0;


	}


	private Vector3 Noise()
	{
		Vector3 noise;

		m_seedX += m_noiseFactor;
		m_seedY += m_noiseFactor;
		m_seedZ += m_noiseFactor;

		float nX = Mathf.PerlinNoise (m_seedX.x, m_seedX.y);
		float nY = Mathf.PerlinNoise (m_seedY.x, m_seedY.y);
		float nZ = Mathf.PerlinNoise (m_seedZ.x, m_seedZ.y);

		noise.x = Utils.FloatMap(nX , 0, 1, -1,1);
		noise.y = Utils.FloatMap(nY , 0, 1, -1,1);
		noise.z = Utils.FloatMap(nZ , 0, 1, -1,1);


		return noise;
	}
	
	private Vector3 Seek(Vector3 _target) {
		Vector3 desired = _target - m_location;
			
		desired.Normalize ();
		desired *= m_maxspeed;

		Vector3 steer = desired - m_velocity;

		if (steer.magnitude > m_maxforce)
			steer = steer.normalized * m_maxforce;

		return steer;
	}

	void Borders() {

		float x = (m_bounds.x / 2);
		float y = (m_bounds.y / 2);
		float z = (m_bounds.z / 2);


		Vector3 dir = m_center - m_location;

		if (dir.magnitude < 1)
			ApplyForce (-dir.normalized * 0.01f);

		if (dir.magnitude > m_bounds.magnitude * 0.7)
			ApplyForce (dir.normalized * 0.001f);

		if (m_location.y > m_center.y + y)
			ApplyForce (new Vector3(0,-0.001f,0));

		if (m_location.y < m_center.y - y)
			ApplyForce (new Vector3(0,0.001f,0));



	}

	
	private Vector3 Separate (List<Boid> boids) {
		float desiredseparation = 0.1f;
		Vector3 steer = new Vector3(0, 0, 0);
		int count = 0;
		
		foreach (Boid other in  boids) {
			float d = (m_location - other.Location).magnitude;
			
			if ((d > 0) && (d < desiredseparation)) {
				
				Vector3 diff = m_location -  other.Location;
				diff.Normalize();
				diff /= d;
				steer += diff;
				count++;          
			}
		}

		if (count > 0) {
			steer /= (float)count;
		}

		
		if (steer.magnitude > 0) {
			
			steer.Normalize ();
			steer *= m_maxspeed;
			steer -= m_velocity;

			if(steer.magnitude > m_maxforce)
				steer = steer.normalized * m_maxforce;
		}
		return steer;
	}

	
	private Vector3 Align (List<Boid> boids) {
		float neighbordist = 3f;
		Vector3 sum = new Vector3(0, 0,0);
		int count = 0;
		foreach (Boid other in boids) {
			float d = (m_location - other.Location).magnitude;
			if ((d > 0) && (d < neighbordist)) {
				sum += other.Velocity;
				count++;
			}
		}
		if (count > 0) {
			sum /= (float)count;
			sum.Normalize();
			sum *= m_maxspeed;
			Vector3 steer = sum - m_velocity;

			if(steer.magnitude > m_maxforce)
				steer = steer.normalized * m_maxforce;
			
			return steer;
		} 
		else {
			return new Vector3(0, 0 , 0);
		}
	}

	
	Vector3 Cohesion (List<Boid> boids) {
		float neighbordist = 3f;
		Vector3 sum = new Vector3(0, 0,0);  
		int count = 0;
		foreach (Boid other in boids) {
			float d = (m_location- other.Location).magnitude;
			if ((d > 0) && (d < neighbordist)) {
				sum += other.Location; 
				count++;
			}
		}
		if (count > 0) {
			sum /= count;
			return Seek(sum);
		} 
		else {
			return new Vector3(0, 0,0);
		}
	}
}