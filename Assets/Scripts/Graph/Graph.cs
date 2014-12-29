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

	public void AddMainBranch(int n)
	{
		nodes.Add(new GraphNode());
		for (int i = 1; i < n; i++)
		{
			AppendLastNode();
		}
	}

	public void AddSideBranch(int n)
	{
		if (n < 1)
		{
			return;
		}
		var midNode = nodes[nodes.Count / 2];
		AddNode(midNode);
		for (int i = 1; i < n; i++)
		{
			AppendLastNode();
		}

	}

	public void AddExtraLeaves(int n)
	{
		if (n < 1)
		{
			return;
		}
		for (int i = 0; i < n; i++)
		{
			GraphNode randomNode;
			do 
			{
				randomNode = nodes[Random.Range(1, nodes.Count)];
			}
			while(randomNode.Parent == null || randomNode.Children.Count != 1);
			AddNode(randomNode);
		}
	}

	private void AddNode(GraphNode parent)
	{
		nodes.Add(new GraphNode(parent, nodes.Count));
	}

	private void AppendLastNode()
	{
		nodes.Add(new GraphNode(nodes[nodes.Count - 1], nodes.Count));
	}
}
