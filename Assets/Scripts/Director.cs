using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour
{
	public Rooms rooms;
	private const int BUILD_ATTEMPTS = 1;
	void Start()
	{
		Graph graph;
		bool isBuilt;
		int i = 1;
		do
		{
			graph = GraphBuilder.Build(20);
			isBuilt = LevelBuilder.Build(graph, rooms);
			Debug.Log(isBuilt ? "Success!" : "Something terrible happened...");
		}
		while (i++ < BUILD_ATTEMPTS);
		if (isBuilt)
		{
			foreach (GraphNode node in graph.Nodes)
			{
				Triple cell = node.CurrentRoom.cell;
				Vector3 position = new Vector3(
					(cell.X - LevelBuilder.SIZE_MAX/2) * Room.ROOM_SIZE,
					(cell.Y - LevelBuilder.SIZE_MAX/2) * Room.ROOM_HEIGHT, 
					(cell.Z - LevelBuilder.SIZE_MAX/2) * Room.ROOM_SIZE);
				Room room = ((GameObject) Instantiate(node.CurrentRoom.Origin.gameObject, position, Quaternion.identity)).GetComponent<Room>();

				foreach (int doorIndex in node.ActiveDoorIndices)
				{
					room.ActivateDoor(doorIndex);
				}
			}
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
