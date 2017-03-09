using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(AudioSource))]
public class Lab03 : MonoBehaviour {


	[SerializeField]
	private int sItem = 0;
	[SerializeField]
	SphereLab03 m_ref;


	private SphereLab03[] m_spheres;

	private float m_min = 0;
	private float m_max = 0;

	void Awake () {
		m_spheres = gameObject.GetComponentsInChildren<SphereLab03> ();
	}
		
	void Update( )
	{
		int sLen = m_spheres.Length;
		float[] spectrum = new float[256];

		int len = spectrum.Length;

		int num = len / sLen;

		int cNum = 0;
		int count = 0;

		AudioListener.GetSpectrumData( spectrum, 0, FFTWindow.Blackman );

		float totSum = 0;

		for( int i = sItem; i < sItem + sLen; i++ )
		{
			float s = spectrum [i];
			totSum += s;

			if (s < m_min)
				m_min = s;
			if (s > m_max)
				m_max = s;

			SphereLab03 sphere = m_spheres [count];

			s *= 3;
			s = Mathf.Clamp (s, m_min, m_max);

			float scale = Utils.FloatMap (s, m_min, m_max, sphere.Scale * 0.2f, sphere.Scale);
			sphere.transform.localScale += (new Vector3 (scale, scale, scale)- sphere.transform.localScale) * 0.1f;

			count++;
		}
			


	}

}
