using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour {

	public delegate void OnHitFloorDelegate(Drop _d);

	public event OnHitFloorDelegate OnHitFloor;

	private float m_accell = -0.05f;
	private Vector3 m_speed = Vector3.zero;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		m_speed.y += m_accell;
		m_speed.y *= 0.98f;

		transform.position += m_speed;

		if (transform.position.y <= 0) {
			OnHitFloor (this);
			Destroy (gameObject);
		}
		 
			
			


			
			

	}
}
