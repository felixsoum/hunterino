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
	int branch;
	List<int> leaves = new List<int>();

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
		branch = nodes.Count / 2;
		var midNode = nodes[branch];
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
			int randInt;
			do 
			{
				randInt = Random.Range(1, nodes.Count);
				randomNode = nodes[randInt];
			}
			while(randomNode.Parent == null || randInt == 1 || randInt == branch - 1 || randomNode.Children.Count != 1);
			leaves.Add(randInt);
			AddNode(randomNode);
		}
	}

	public void PrintDebug()
	{
		Debug.Log("Leaves at: ");
		foreach (var leaf in leaves)
		{
			Debug.Log(leaf);
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
