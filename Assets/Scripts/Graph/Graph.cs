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
	}

	public void AddNode()
	{
		var count = nodes.Count;
		if (count == 0)
		{
			nodes.Add(new GraphNode());
		}
		else
		{
			nodes.Add(new GraphNode(nodes[nodes.Count - 1], count));
		}
	}
}
