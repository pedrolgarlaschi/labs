using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace star.vehicle
{
	public class XWing : MonoBehaviour {

		[SerializeField]
		private float m_maxX;
		[SerializeField]
		private float m_maxSpeed = 3;
		[SerializeField]
		private float m_maxAngle = 30;
		[SerializeField]
		private float m_speed = 0;
		[SerializeField]
		private float m_accell = 0.2f;
		[SerializeField]
		private float m_friction = 0.2f;

		private Vector3 m_startPos; 

		void Start () {

			m_startPos = transform.position;
		}

		void Update () {

			if (Input.GetKey(KeyCode.RightArrow))
				m_speed += m_accell;
			else if (Input.GetKey(KeyCode.LeftArrow))
				m_speed -= m_accell;

			m_speed = Mathf.Clamp (m_speed, -m_maxSpeed, m_maxSpeed);

		}

		private void FixedUpdate()
		{
			Vector3 pos = transform.position;
			pos.x += m_speed;
			pos.x = Mathf.Clamp (pos.x, -m_maxX, m_maxX);

			Vector3 locPos = transform.position;
			pos.y = m_startPos.y + Mathf.Sin (Time.time * 5) * 0.6f;


			float rotZ = (pos.x / m_maxX) * m_maxAngle;
			Vector3 rot = transform.eulerAngles;
			rot.z = rotZ;

			transform.rotation = Quaternion.Euler (rot);
			transform.position = pos;

			m_speed *= m_friction;
		}
	}
}

