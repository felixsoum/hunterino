using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
	public List<Door> doors;
	public Triple length = new Triple(1);
	public const float ROOM_SIZE = 25f;
	public const float ROOM_HEIGHT = 12f;
	public int lengthX;
	public int lengthY;
	public int lengthZ;

	public void Init()
	{
		foreach (Door door in doors)
		{
			SetCellPosition(door);
		}

		foreach (Door door in doors)
		{
			length.X = Mathf.Max(length.X, door.cell.X + 1);
			length.Y = Mathf.Max(length.Y, door.cell.Y + 1);
			length.Z = Mathf.Max(length.Z, door.cell.Z + 1);
		}
		Debug.Log("Length for " + gameObject.name + " " + length);
		// For inspector
		lengthX = length.X;
		lengthY = length.Y;
		lengthZ = length.Z;
	}

	public void SetCellPosition(Door door)
	{
		Vector3 pos = door.transform.position - transform.position;
	    door.cell.X = (int) ((pos.x + ROOM_SIZE/2)/ROOM_SIZE);
		door.cell.Y = (int) (pos.y/ROOM_HEIGHT);
		door.cell.Z = (int) ((pos.z + ROOM_SIZE/2)/ROOM_SIZE);
	}
}
