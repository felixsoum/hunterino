﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GraphNode
{
	public GraphNode Parent { get; set; }
	public List<GraphNode> Children { get; set; }
	public List<RoomTemplate> Rooms { get; set; }
	public RoomTemplate CurrentRoom { get; set; }
	public bool[,,] space;
	public DoorTemplate ParentDoor { get; set; }
        
    public GraphNode() : this(null) {}

	public GraphNode(GraphNode parent)
	{
		Parent = parent;
		if (parent != null)
		{
			parent.Children.Add(this);
		}
		Children = new List<GraphNode>();
		CurrentRoom = null;
	}

	public void SetCell(Triple cell)
	{
		CurrentRoom.cell = cell;
		SetSpace(true);
	}

	private void SetSpace(bool value)
	{
		for (int x = 0; x < CurrentRoom.length.X; x++)
		{
			for (int y = 0; y < CurrentRoom.length.Y; y++)
			{
				for (int z = 0; z < CurrentRoom.length.Z; z++)
				{
					space[x, y, z] = value;
				}
			}
		}
	}

	public void SetRoom(int index)
	{
		CurrentRoom = Rooms[index];
		Rooms.Clear();
	}

	public bool TryRoom()
	{
		var parentDoor = Parent.CurrentRoom.PopDoor();
		if (parentDoor == null)
		{
			Debug.LogError("Parent door is null");
			return false;
		}
		var roomIndices = GetIndices(Rooms.Count);
		while (roomIndices.Count > 0)
		{
			int randomRoom = Random.Range(0, roomIndices.Count - 1);
			var currentRoom = Rooms[roomIndices[randomRoom]];
			Debug.Log("Trying room: " + currentRoom.Origin.gameObject.name);
			roomIndices.RemoveAt(randomRoom);
			var matchingDoors = GetMatchingDoors(currentRoom, parentDoor.Origin.orientation);
			var doorIndices = GetIndices(matchingDoors.Count);
			while (doorIndices.Count > 0)
			{
				int randomDoor = Random.Range(0, doorIndices.Count - 1);
				var currentDoor = matchingDoors[doorIndices[randomDoor]];
				Debug.Log("Trying door: " + currentDoor.Origin.gameObject.name);
				doorIndices.RemoveAt(randomDoor);
				Triple parentDoorCell = parentDoor.Cell + Parent.CurrentRoom.cell;
				Triple currentRoomCell = FindDoorCell(parentDoorCell, currentDoor) - currentDoor.Cell;
				if (IsPlaceable(currentRoomCell, currentRoom.length))
				{
					CurrentRoom = currentRoom;
					SetCell(currentRoomCell);
					var isChildrenPossible = true;
					for (int i = 0; i < Children.Count; i++)
					{
						if (!Children[i].TryRoom())
						{
							isChildrenPossible = false;
							break;
						}
					}
					return isChildrenPossible ? true : TryRoom();
				}
			}
		}
		return false;
	}

	public bool IsPlaceable(Triple cell, Triple length)
	{
		for (int x = 0; x < length.X; x++)
		{
			for (int y = 0; y < length.Y; y++)
			{
				for (int z = 0; z < length.Z; z++)
				{
					if (space[cell.X + x, cell.Y + y, cell.Z + z])
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	private List<DoorTemplate> GetMatchingDoors(RoomTemplate room, Door.Orientation orientation)
	{
		switch (orientation)
		{
		case Door.Orientation.North:
			return room.SouthDoors;
		case Door.Orientation.East:
			return room.WestDoors;
		case Door.Orientation.South:
			return room.NorthDoors;
		case Door.Orientation.West:
			return room.EastDoors;
		default:
			return null;
		}
	}

	private List<int> GetIndices(int count)
	{
		var list = new List<int>();
		for (int i = 0; i < count; i++)
		{
			list.Add(i);
		}
		return list;
	}

	private Triple FindDoorCell(Triple matchingCell, DoorTemplate door)
	{
		switch (door.Origin.orientation)
		{
		case Door.Orientation.North:
			return matchingCell + new Triple(0, 0, -1);
		case Door.Orientation.East:
			return matchingCell + new Triple(-1, 0, 0);
		case Door.Orientation.South:
			return matchingCell + new Triple(0, 0, 1);
		case Door.Orientation.West:
			return matchingCell + new Triple(1, 0, 0);
		default:
			return matchingCell;
		}
	}
}
