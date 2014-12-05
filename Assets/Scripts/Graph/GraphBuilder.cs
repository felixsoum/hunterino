using System.Collections;

public static class GraphBuilder
{
	public static Graph Build()
	{
		Graph graph = new Graph();
		for (int i = 0; i < 4; i++)
		{
			graph.AddNode();
		}
		return graph;
	}
}
