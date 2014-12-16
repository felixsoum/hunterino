using System.Collections;

public static class GraphBuilder
{
	public static Graph Build(int mainNodes, int branchNodes, int extraLeaves)
	{
		Graph graph = new Graph();
		graph.AddMainBranch(mainNodes);
		graph.AddSideBranch(branchNodes);
		graph.AddExtraLeaves(extraLeaves);
		return graph;
	}
}
