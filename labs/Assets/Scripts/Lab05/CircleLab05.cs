using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLab05 : MonoBehaviour {


	public delegate void OnCatchDelegate(CircleLab05 _c);

	private List<CircleLab05> m_enemys;
	private float m_scale;
	private float m_mass;

	private Vector3 m_speed = Vector3.zero;
	private Vector3 m_accel = Vector3.zero;

	public event OnCatchDelegate OnCatch;
	private Vector3			m_startPos;


	float m_fric = 0.98f;


	public float Scale
	{
		get{ 
		
			return m_scale;
		}
	}

	public float Mass
	{
		get{ 

			return m_mass;
		}
	}

	private bool avoiding;

	void Awake()
	{
		m_startPos = new Vector3 (Random.Range (-10, 10), Random.Range (-5, 5), 0);
	}

	void Start()
	{
		m_scale = transform.localScale.x;
		m_mass = m_scale * 0.5f;
	}


	public void AddEnemy(CircleLab05 _enemy)
	{
		if (m_enemys == null)
			m_enemys = new List<CircleLab05> ();

		m_enemys.Add (_enemy);
	}


	void Update()
	{
		Move ();

		if(avoiding)
			Catch ();	
	}


	private void Move()
	{
		if (!avoiding) {

			Vector3 d = (m_startPos - transform.position);
			transform.position += d * 0.1f;

			if (d.magnitude < 0.1f)
				avoiding = true;

			return;
		}
		else if (m_enemys.Count == 0) {


			Vector3 d = (Vector3.zero - transform.position);
			transform.position += d * 0.1f;

			if (d.magnitude < 0.1f)
				OnCatch (this);

			return;
		}

		CircleLab05 c = GetCloser ();

		Vector3 dir = c.transform.position - transform.position;
		dir.Normalize ();
		float mag = dir.magnitude;
		mag = Utils.FloatMap (mag, 0, 1, 0.2f, 0.5f) / 100.0f;

		if (Scale > c.Scale)
			AddForce (dir * mag);
		else
			AddForce (dir * mag * - 1);


		m_speed += m_accel;
		m_speed *= m_fric;

		if (m_speed.magnitude > 3)
			m_speed = m_speed.normalized * 3;

		transform.position += m_speed;

		m_accel *= 0;

		Vector3 pos = transform.position;

		if (transform.position.x > 12) {
			pos.x = 12;
			transform.position = pos;
			m_speed.x *= -1;
		}
		else if (transform.position.x < -12) {
			pos.x = -12;
			transform.position = pos;
			m_speed.x *= -1;
		}
			

		if (transform.position.y > 8) {
			pos.y = 8;
			transform.position = pos;
			m_speed.y *= -1;
		}
		else if (transform.position.y < -8) {
			pos.y = -8;
			transform.position = pos;
			m_speed.y *= -1;
		}
			
	}

	private void Catch()
	{
		CircleLab05 c = GetCloser ();
		if (c == null)
			return;

		Vector3 dir = c.transform.position - transform.position;

		if (dir.magnitude < 1) {

			transform.localScale += c.transform.localScale;
			m_scale += c.transform.localScale.x;
			m_mass = m_scale * 0.5f;

			OnCatch (c);
			return;
		}
	}


	public void Remove(CircleLab05 _c)
	{
		m_enemys.Remove (_c);
	}

	private void AddForce(Vector3 f)
	{
		m_accel += f / m_mass;
	}
		
	private CircleLab05 GetCloser()
	{
		float dist = 100000;
		CircleLab05 enemy = null;

		foreach (CircleLab05 c in m_enemys) {

			if (c == null)
				break;

			float d = (c.transform.position - transform.position).magnitude;

			if (d < dist) {
				dist = d;
				enemy = c;
			}

		}


		return enemy;

	}

}
