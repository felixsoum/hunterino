using System.Collections;

public static class GraphBuilder
{
	public static Graph Build(int n)
	{
		Graph graph = new Graph();
		for (int i = 0; i < n; i++)
		{
			graph.AddNode();
		}
		return graph;
	}
}
