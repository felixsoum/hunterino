using UnityEngine;
using System.Collections;

public class BalloonEnemy : MonoBehaviour
{
	public float speed = 10f;
	public int life = 4;
	public AudioSource pushAudio;
	public GameObject deathEffect;
	GameObject playerObject;

	void Awake()
	{
		playerObject = GameObject.FindGameObjectWithTag("Player");
		ScaleToLife();
	}

	void Update()
	{
		UpdateMove();
		UpdateLook();
	}

	void UpdateMove()
	{
		var velocity = rigidbody.velocity;
		var seek = playerObject.transform.position - transform.position;
		seek.Normalize();
		seek *= speed * Time.deltaTime;
		velocity += seek;
		if (velocity.magnitude > GetMaxSpeed())
		{
			velocity = velocity.normalized * GetMaxSpeed();
        }
        rigidbody.velocity = velocity;
	}

	void UpdateLook()
	{
		rigidbody.MoveRotation(Quaternion.LookRotation(rigidbody.velocity.normalized));
	}

	void OnCollisionEnter(Collision c)
	{
		if (c.gameObject == playerObject)
		{
			playerObject.SendMessage("OnHit", 10);
			playerObject.SendMessage("OnPush", transform.position);
			pushAudio.Play();
		}
	}

	void OnHit(int damage)
	{
		life -= damage;
		if (life <= 0)
		{
			Die();
		}
		else
		{
			ScaleToLife();
		}
	}

	void ScaleToLife()
	{
		transform.localScale = Vector3.one * (0.5f + 0.5f*life);
	}

	float GetMaxSpeed()
	{
		return speed*10;
	}
	
	void Die()
	{
		Instantiate(deathEffect, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
