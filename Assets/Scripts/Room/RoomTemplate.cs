using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomTemplate
{
	public Room Origin { get; set; }
	public int Index { get; set; }
	public List<DoorTemplate> Doors { get; set; }
	public Triple cell = new Triple();
	public Triple length = new Triple(1);
	public DoorTemplate CurrentDoor { get; set; }
	public List<DoorTemplate> NorthDoors { get; set; }
	public List<DoorTemplate> EastDoors { get; set; }
	public List<DoorTemplate> SouthDoors { get; set; }
	public List<DoorTemplate> WestDoors { get; set; }
	public List<DoorTemplate> AvailableDoors { get; set; }

	public RoomTemplate(Room room, int index)
	{
		Origin = room;
		Index = index;
		length = room.length;
		Doors = new List<DoorTemplate>();
		NorthDoors = new List<DoorTemplate>();
		EastDoors = new List<DoorTemplate>();
		SouthDoors = new List<DoorTemplate>();
		WestDoors = new List<DoorTemplate>();
		CurrentDoor = null;
		AvailableDoors = new List<DoorTemplate>();
		RefreshAvailability();
		for (int i = 0; i < room.doors.Count; i++)
		{
			var template = new DoorTemplate(room.doors[i], i);
			Doors.Add(template);
			switch (Origin.doors[i].orientation)
			{
			case Door.Orientation.North:
				NorthDoors.Add(template);
				break;
			case Door.Orientation.East:
				EastDoors.Add(template);
				break;
			case Door.Orientation.South:
				SouthDoors.Add(template);
				break;
			case Door.Orientation.West:
				WestDoors.Add(template);
				break;
			}
		}
	}

	public void KeepDoor(int index)
	{
		var door = Doors[index];
		AvailableDoors.Clear();
		AvailableDoors.Add(door);
	}

	public DoorTemplate PopDoor()
	{
		int count = AvailableDoors.Count;
//		Debug.Log("Popping one door out of: " + count);
		if (count == 0)
		{
			return null;
		}
		int index = Random.Range(0, count);
		var door = AvailableDoors[index];
		AvailableDoors.RemoveAt(index);
		return door;
	}

	public void RefreshAvailability()
	{
		AvailableDoors.Clear();
		foreach (DoorTemplate door in Doors)
		{
			AvailableDoors.Add(door);
		}
	}
}
