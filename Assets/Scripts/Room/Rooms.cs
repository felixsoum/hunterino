using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rooms : MonoBehaviour
{
	public List<Room> rooms;

	void Start()
	{
		rooms[0].Bang();
	}
}
