using System;

using PathfindingConsoleProject.DataStructures;

namespace PathfindingConsoleProject.Algorithms
{
    public class BreathFirstSeach
    {
        private GenericList<GenericGraph> nodes;

        public BreathFirstSeach()
        { }

        public GenericGraphNode[] BFS(GenericGraph graph, GenericGraphNode source, GenericGraph goal)
        {
            GenericGraphNode[] path;

            GenericList<GenericGraphNode> currentPath = new GenericList<GenericGraphNode>();
            currentPath.Add(source);

            if (source.Equals(goal))
            {
                path = new GenericGraphNode[currentPath.Count];
                currentPath.CopyTo(path, 0);
                return path;
            }





            return BFS(graph, parrent, goal);
        }
    }
}
