using System.Collections;
using System.Collections.Generic;

public class RoomTemplate
{
	public Room Origin { get; set; }
	public int Index { get; set; }
	public List<DoorTemplate> doors;
	public int cellX;
	public int cellY;
	public int cellZ;

	public RoomTemplate(Room room, int index)
	{
		Origin = room;
		Index = index;
		doors = new List<DoorTemplate>();
		for (int i = 0; i < Origin.doors.Count; i++)
		{
			doors.Add(new DoorTemplate(Origin.doors[i], i));
		}
	}
}
