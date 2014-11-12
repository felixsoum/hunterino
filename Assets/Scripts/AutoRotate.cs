using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour
{
	public float rate = 100f;

	void Update()
	{
		transform.Rotate(0, rate * Time.deltaTime, 0);
	}
}
