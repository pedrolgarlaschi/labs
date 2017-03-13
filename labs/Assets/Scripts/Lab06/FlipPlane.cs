using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlipPlane : MonoBehaviour {

	private float m_timeToFlip;
	private float m_lastTime;

	// Use this for initialization
	void Start () {

		m_timeToFlip = Random.Range (0, 6);
		m_lastTime = Time.time;

	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > m_lastTime + m_timeToFlip) {


			m_lastTime = Time.time;

			float flip = Random.Range (0, 10) > 5 ? 180 : -180;

			Vector3 rot = transform.eulerAngles;

			if(Random.Range(0,10) > 5)
				rot.x += flip;
			else
				rot.z += flip;

			float animTime = Random.Range (1.3f, 2.5f);


			transform.DORotate (rot, animTime).SetEase (Ease.OutBounce);


			m_timeToFlip =animTime + Random.Range (1, 3);

		}

	}
}
