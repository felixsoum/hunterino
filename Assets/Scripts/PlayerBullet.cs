using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour
{
	public float force = 2000f;
	public GameObject hitEffect;

	void Start()
	{
		rigidbody.AddForce(transform.rotation * Vector3.forward * force);
	}

	void OnCollisionEnter(Collision c)
	{
		c.gameObject.SendMessage("OnHit", 1, SendMessageOptions.DontRequireReceiver);
		Quaternion hitRot = transform.rotation;
		hitRot.SetLookRotation(c.contacts[0].normal);
		Vector3 hitPos = c.contacts[0].point + c.contacts[0].normal*0.1f;
		Debug.DrawRay(hitPos, c.contacts[0].normal, Color.white, 1);
		Instantiate(hitEffect, hitPos, hitRot);
		Destroy(gameObject);
	}
}
