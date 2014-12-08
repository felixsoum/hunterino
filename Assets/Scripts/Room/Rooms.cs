using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rooms : MonoBehaviour
{
	public List<Room> rooms;

	void Awake()
	{
		foreach (Room room in rooms)
		{
			room.Init();
		}
	}

	public List<RoomTemplate> GetTemplates()
	{
		var templates = new List<RoomTemplate>();
		for (int i = 0; i < rooms.Count; i++)
		{
			templates.Add(new RoomTemplate(rooms[i], i));
		}
		return templates;
	}
}
