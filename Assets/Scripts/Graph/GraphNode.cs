using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GraphNode
{
	public GraphNode Parent { get; set; }
	public List<GraphNode> Children { get; set; }
	public List<RoomTemplate> Rooms { get; set; }
	public RoomTemplate CurrentTemplate { get; set; }
        
    public GraphNode() : this(null) {}

	public GraphNode(GraphNode parent)
	{
		Parent = parent;
		Children = new List<GraphNode>();
		CurrentTemplate = null;
	}

	public void SetCell(int x, int y, int z)
	{
		CurrentTemplate.cellX = x;
		CurrentTemplate.cellY = y;
		CurrentTemplate.cellZ = z;
	}

	public void SetRoom(int index)
	{
		CurrentTemplate = Rooms[index];
		Rooms.Clear();
	}
}
