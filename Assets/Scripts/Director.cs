using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour
{
	public Rooms rooms;

	void Start() {
		Graph graph = GraphBuilder.Build();
		bool isBuilt = LevelBuilder.Build(graph, rooms);
		Debug.Log(isBuilt ? "Success!" : "Something terrible happened...");
		if (isBuilt)
		{
			foreach (GraphNode node in graph.Nodes)
			{
				Triple cell = node.CurrentRoom.cell;
				Vector3 position = new Vector3(
					(cell.X - LevelBuilder.SIZE_MAX/2) * Room.ROOM_SIZE,
					(cell.Y - LevelBuilder.SIZE_MAX/2) * Room.ROOM_HEIGHT, 
					(cell.Z - LevelBuilder.SIZE_MAX/2) * Room.ROOM_SIZE);
				Instantiate(node.CurrentRoom.Origin, position, Quaternion.identity);
			}
		}
	}
}
