using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
	public float minSpawnPeriod = 1f;
	public float maxSpawnPeriod = 4f;
	public GameObject spawnee;
	Transform spawnPoint;
	Transform enemies;

	void Start()
	{
		spawnPoint = GameObject.Find("SpawnPoint").transform;
		Invoke("OnSpawn", Random.Range(minSpawnPeriod, maxSpawnPeriod));
		enemies = GameObject.FindGameObjectWithTag("Enemies").transform;
	}

	void OnSpawn()
	{
		var go = (GameObject) Instantiate(spawnee, spawnPoint.position, spawnPoint.rotation);
		go.transform.parent = enemies.transform;
		Invoke("OnSpawn", Random.Range(minSpawnPeriod, maxSpawnPeriod));
	}
}
