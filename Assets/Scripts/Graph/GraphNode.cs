using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GraphNode
{
	public GraphNode Parent { get; set; }
	public List<GraphNode> Children { get; set; }

	public GraphNode() : this(null) {}

	public GraphNode(GraphNode parent)
	{
		Parent = parent;
		Children = new List<GraphNode>();
	}
}
