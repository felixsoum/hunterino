using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
	public List<Door> doors;
	public int lengthX = 1;
	public int lengthY = 1;
	public int lengthZ = 1;
	const float ROOM_SIZE = 25f;
	const float ROOM_HEIGHT = 12f;

	public void Start()
	{
		foreach (Door door in doors)
		{
			SetCellPosition(door);
		}

		foreach (Door door in doors)
		{
			lengthX = Mathf.Max(lengthX, door.cellX + 1);
			lengthY = Mathf.Max(lengthY, door.cellY + 1);
			lengthZ = Mathf.Max(lengthZ, door.cellZ + 1);
		}
	}

	public void Bang()
	{
		Debug.Log("Bang");
	}

	public void SetCellPosition(Door door)
	{
		Vector3 pos = door.transform.position - transform.position;
	    door.cellX = (int) ((pos.x + ROOM_SIZE/2)/ROOM_SIZE);
		door.cellY = (int) (pos.y/ROOM_HEIGHT);
		door.cellZ = (int) ((pos.z + ROOM_SIZE/2)/ROOM_SIZE);
	}
}
