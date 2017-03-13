using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab07 : MonoBehaviour {

	[SerializeField]
	private Material[] m_materials;
	[SerializeField]
	private SphereLab07 m_sRef;



	private float m_spawnTime = 0.5f;
	private float m_lastTime;


	void Start()
	{
		SpawnSphere (1, 4);
	}


	void Update()
	{
		if (Time.time > m_spawnTime + m_lastTime)
			SpawnSphere (5, 10);
	}

	private void SpawnSphere(float _min , float _max)
	{
		m_lastTime = Time.time;

		float rand = Random.Range (_min, _max);

		for (int i = 0; i < rand; i++) {

			SphereLab07 s = Instantiate (m_sRef) as SphereLab07;
			s.SetMaterial(m_materials[Random.Range(0,m_materials.Length)]);

			Vector3 pos =new Vector3 ();
			pos.y =  Random.Range (-10, -5);
			pos.x = Random.Range (-8, 8);
			pos.z = Random.Range (-1, 5);
			s.transform.position = pos;
		}
	}



}
