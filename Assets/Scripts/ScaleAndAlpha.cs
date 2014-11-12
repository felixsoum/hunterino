using UnityEngine;
using System.Collections;

public class ScaleAndAlpha : MonoBehaviour
{
	void Update()
	{
		var color = renderer.material.GetColor("_TintColor");
		color.a = Mathf.Max(color.a -Time.deltaTime, 0);
		renderer.material.SetColor("_TintColor", color);
		transform.localScale *= 1.05f;
	}
}
