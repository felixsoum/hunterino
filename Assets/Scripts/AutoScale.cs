using UnityEngine;
using System.Collections;

public class AutoScale : MonoBehaviour
{
	public float rate;

	void Update()
	{
		transform.localScale *= rate;
	}
}
