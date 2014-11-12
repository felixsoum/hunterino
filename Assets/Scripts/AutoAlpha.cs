using UnityEngine;
using System.Collections;

public class AutoAlpha : MonoBehaviour
{
	public float rate = 1f;

	void Update()
	{
		var color = renderer.material.GetColor("_TintColor");
		color.a = Mathf.Max(color.a -Time.deltaTime * rate, 0);
		renderer.material.SetColor("_TintColor", color);
	}
}
