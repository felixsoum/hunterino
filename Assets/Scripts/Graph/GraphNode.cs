using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GraphNode
{
	public GraphNode Parent { get; set; }
	public List<GraphNode> Children { get; set; }
	public List<RoomTemplate> Rooms { get; set; }
	public RoomTemplate CurrentRoom { get; set; }
	public bool[,,] space;
	public List<int> ActiveDoorIndices { get; set; }
	public int nodeIndex = 0;
	public int DoorForParent { get; set; }
	private List<int> roomIndices = new List<int>();
	private List<int> doorIndices = new List<int>();
	private List<int> candidateParentDoors = new List<int>();
	private RoomTemplate currentParentRoomCandidate = null;
	private List<DoorTemplate> parentAvailableDoorsClone = new List<DoorTemplate>();
        
    public GraphNode() : this(null, 0) {}

	public GraphNode(GraphNode parent, int i)
	{
		Parent = parent;
		nodeIndex = i;
		if (parent != null)
		{
			parent.Children.Add(this);
		}
		Children = new List<GraphNode>();
		CurrentRoom = null;
		ActiveDoorIndices = new List<int>();
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
					space[CurrentRoom.cell.X + x, CurrentRoom.cell.Y + y, CurrentRoom.cell.Z + z] = value;
				}
			}
		}
	}

	private int CountSpace()
	{
		int count = 0;
		for (int x = 0; x < space.GetLength(0); x++)
		{
			for (int y = 0; y < space.GetLength(1); y++)
			{
				for (int z = 0; z < space.GetLength(2); z++)
				{
					if (space[x, y, z])
					{
						count++;
					}
				}
			}
		}
		return count;
	}

	public void SetRoom(int index)
	{
		CurrentRoom = Rooms[index];
		Rooms.Clear();
	}

	public bool TryRoom()
	{
//		Debug.Log("TryRoom node #" + nodeIndex + ", parent:" + Parent.CurrentRoom.Origin.name + Parent.nodeIndex);
		ActiveDoorIndices.Clear();
		if (currentParentRoomCandidate != Parent.CurrentRoom)
		{
			currentParentRoomCandidate = Parent.CurrentRoom;
			PutIndices(candidateParentDoors, Parent.CurrentRoom.AvailableDoors.Count);
			parentAvailableDoorsClone.Clear();
			parentAvailableDoorsClone = CloneDoors(Parent.CurrentRoom.AvailableDoors);
		}
//		var parentDoor = Parent.CurrentRoom.PopDoor();
		while (candidateParentDoors.Count > 0)
		{
			Parent.CurrentRoom.AvailableDoors = CloneDoors(parentAvailableDoorsClone);
			var randomParentDoorIndex = Random.Range(0, candidateParentDoors.Count);
			int parentDoorCandidateIndex = candidateParentDoors[randomParentDoorIndex];
			candidateParentDoors.RemoveAt(randomParentDoorIndex);
			var parentDoor = Parent.CurrentRoom.AvailableDoors[parentDoorCandidateIndex];
			Parent.CurrentRoom.AvailableDoors.RemoveAt(parentDoorCandidateIndex);
	//		Debug.Log("Parent door:" + parentDoor.Origin.orientation);
			PutIndices(roomIndices, Rooms.Count);
	//		Debug.Log("roomIndices1:" + roomIndices.Count);
			while (roomIndices.Count > 0)
			{
	//			Debug.Log("roomIndices2:" + roomIndices.Count);
				int randomRoom = Random.Range(0, roomIndices.Count);
				var currentRoom = Rooms[roomIndices[randomRoom]];
	//			Debug.Log("Trying current room: " + currentRoom.Origin.gameObject.name);
				roomIndices.RemoveAt(randomRoom);
				var matchingDoors = GetMatchingDoors(currentRoom, parentDoor.Origin.orientation);
				PutIndices(doorIndices, matchingDoors.Count);
	//			Debug.Log("doorIndices1:" + doorIndices.Count);
				while (doorIndices.Count > 0)
				{
	//				Debug.Log("doorIndices2:" + doorIndices.Count);
					int randomDoor = Random.Range(0, doorIndices.Count);
					var currentDoor = matchingDoors[doorIndices[randomDoor]];
	//				Debug.Log("Trying current door: " + currentDoor.Origin.orientation);
					doorIndices.RemoveAt(randomDoor);
					Triple parentDoorCell = parentDoor.Cell + Parent.CurrentRoom.cell;
					Triple currentRoomCell = FindDoorCell(parentDoorCell, currentDoor) - currentDoor.Cell;
					bool isPlaceable = IsPlaceable(currentRoomCell, currentRoom.length);
	//				Debug.Log("isPlaceable=" + isPlaceable);
					if (isPlaceable)
					{
	//					Debug.Log("s1=" + CountSpace());
						CurrentRoom = currentRoom;
						SetCell(currentRoomCell);
	//					Debug.Log("s2=" + CountSpace());
						var isChildrenPossible = true;
						int loopCheck = 0;
						for (int i = 0; i < Children.Count; i++)
						{
							if (loopCheck++ > 100)
							{
								Debug.LogError("Infinite detected");
								return false;
							}
							if (!Children[i].TryRoom())
							{
								if (i == 0)
								{
									isChildrenPossible = false;
									break;
								}
								else
								{
									i -= 2;
								}
							}
						}
						if (!isChildrenPossible)
						{
							SetSpace(false);
							RefreshDoors();
						}
						else
						{
	//						Debug.Log("All children possible");
							ActiveDoorIndices.Add(currentDoor.Index);
							DoorForParent = parentDoor.Index;
							return true;
						}
					}
				}
	//			Debug.Log("No more doorIndices");
			}
	//		Debug.Log("No more roomIndices");
//			return TryRoom();
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

	public void SetDoorFromChildren()
	{
		foreach (var child in Children)
		{
			ActiveDoorIndices.Add(child.DoorForParent);
		}
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

	private void PutIndices(List<int> list, int count)
	{
		list.Clear();
		for (int i = 0; i < count; i++)
		{
			list.Add(i);
		}
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

	private void RefreshDoors()
	{
		foreach(RoomTemplate room in Rooms)
		{
			room.RefreshAvailability();
		}
	}

	private List<DoorTemplate> CloneDoors(List<DoorTemplate> doors)
	{
		var list = new List<DoorTemplate>();
		foreach (var door in doors)
		{
			list.Add(door);
		}
		return list;
	}
}
