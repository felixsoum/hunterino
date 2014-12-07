public static class LevelBuilder
{
	const int SIZE_MAX = 20;
	static bool[,,] space;

	public static bool Build(Graph graph, Rooms rooms)
	{
		space = new bool[SIZE_MAX, SIZE_MAX, SIZE_MAX];
		for (int x = 0; x < SIZE_MAX; x++)
		{
			for (int y = 0; y < SIZE_MAX; y++)
			{
				for (int z = 0; z < SIZE_MAX; z++)
				{
					space[x, y, z] = false;
				}
			}
		}
		foreach (GraphNode node in graph.Nodes)
		{
			node.Rooms = rooms.GetTemplates();
			node.space = space;
		}
		SetRoot(graph.Nodes[0]);
		return graph.Nodes[1].TryRoom();
	}

	public static void SetRoot(GraphNode node)
	{
		node.SetRoom(0);
		node.SetCell(new Triple(SIZE_MAX/2));
		node.CurrentRoom.KeepDoor(0);
	}
}
