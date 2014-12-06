using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Graph
{
	public List<GraphNode> Nodes
	{
		get
		{
			return nodes;
		}
	}
	public int Count
	{
		get
		{
			return nodes.Count;
		}
	}
	List<GraphNode> nodes;

	public Graph()
	{
		nodes = new List<GraphNode>();
		nodes.Add(new GraphNode());
	}

	public void AddNode()
	{
		nodes.Add(new GraphNode(nodes[nodes.Count - 1]));
	}
}
