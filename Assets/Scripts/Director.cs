using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour
{
	void Start() {
		Graph graph = GraphBuilder.Build();
		Debug.Log(graph.Count);
	}
}
