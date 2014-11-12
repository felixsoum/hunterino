using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
	GameObject playerObject;

	void Awake()
	{
		playerObject = GameObject.FindGameObjectWithTag("PlayerHead");
	}

	void Update()
	{
		transform.position = playerObject.transform.position;
		transform.rotation = playerObject.transform.rotation;
		Screen.lockCursor = true;
	}
}
