using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
	public List<Door> doors;

	public void Bang()
	{
		Debug.Log("Bang");
	}
}
