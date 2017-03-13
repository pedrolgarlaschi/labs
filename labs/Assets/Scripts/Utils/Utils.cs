using UnityEngine;
using System.Collections;


public class Utils : MonoBehaviour {

	/// <summary>
	/// Map float value
	/// </summary>
	/// <returns>The map.</returns>
	/// <param name="val">Value.</param>
	/// <param name="inMin">In minimum.</param>
	/// <param name="inMax">In max.</param>
	/// <param name="outMin">Out minimum.</param>
	/// <param name="outMax">Out max.</param>
	public static float FloatMap(float val, float inMin, float inMax, float outMin, float outMax)
	{
		return outMin + (outMax - outMin) * ((val - inMin) / (inMax - inMin));
	}

	public static void DestroyChildes(GameObject go)
	{
		foreach (Transform t in go.transform) 
		{
			if(t.transform == go.transform)
				continue;


			Destroy(t.gameObject);
		}
	}


	public static Vector3 RandomVec(float _min , float _max)
	{
		return RandomVec (_min, _max, _min, _max, _min, _max);
	}

	public static Vector3 RandomVec(float _minX , float _maxX ,  float _minY , float _maxY , float _minZ , float _maxZ)
	{
		float x = Random.Range (_minX, _maxX);
		float y = Random.Range (_minY, _maxY);
		float z = Random.Range (_minZ, _maxZ);

		return new Vector3 (x,y,z);
	}


	public static T Find<T> (string name) where T : Component
	{
		return GameObject.Find (name).GetComponent<T> ();
	}

}
