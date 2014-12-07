using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour
{
	public Rooms rooms;

	void Start() {
		Graph graph = GraphBuilder.Build();
		bool isBuilt = LevelBuilder.Build(graph, rooms);
		Debug.Log(isBuilt ? "Success!" : "Something terrible happened...");
	}
}
