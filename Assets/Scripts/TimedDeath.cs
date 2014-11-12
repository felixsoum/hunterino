using UnityEngine;
using System.Collections;

public class TimedDeath : MonoBehaviour
{
	public float lifeTime = 4f;

	void Start()
	{
		Invoke("Die", lifeTime);
	}
	
	void Die()
	{
		Destroy(gameObject);
	}
}
