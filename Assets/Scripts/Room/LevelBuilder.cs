public static class LevelBuilder
{
	const int SIZE_MAX = 20;
	static bool[, ,] space;

	static void Build(Graph graph, Rooms rooms)
	{
		space = new bool[SIZE_MAX, SIZE_MAX, SIZE_MAX];
		foreach (GraphNode node in graph.Nodes)
		{
			node.Rooms = rooms.GetTemplates();
		}
		GraphNode root = graph.Nodes[0];
	}

	static void SetRoot(GraphNode node)
	{
		node.SetCell(SIZE_MAX/2, SIZE_MAX/2, SIZE_MAX/2);

	}
}
