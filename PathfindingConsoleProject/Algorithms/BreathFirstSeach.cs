using System;

using PathfindingConsoleProject.DataStructures;

namespace PathfindingConsoleProject.Algorithms
{
    public static class BreathFirstSeach
    {
        public static GenericList<GenericGraphNode> FindPathBetweenNodes(GenericGraphNode source, GenericGraphNode goal)
        {
            if (source.Equals(goal))
            {
                return new GenericList<GenericGraphNode>() { source };
            }

            // Tilføj start noden til søgeliste
            GenericList<GenericGraphNode> searchList = new GenericList<GenericGraphNode>();
            searchList.Add(source);

            // Hjælpelister til at holde styr på, hvad der er besøgt allerede
            // Bruges også til at beregne, hvilke noder som er sammenhængende til korteste path
            GenericList<GenericGraphNode> cameFromKey = new GenericList<GenericGraphNode>();
            GenericList<GenericGraphNode> cameFromValue = new GenericList<GenericGraphNode>();

            // Listen over, hvilken path der skal returneres
            GenericList<GenericGraphNode> pathList = new GenericList<GenericGraphNode>();
            int iterations = 0;
            while (searchList.Count > 0)
            {
                GenericGraphNode current = searchList[0];
                searchList.Remove(current);
                GenericGraphNode[] validNeighbours = Array.FindAll(current.Neighbours, n => !cameFromValue.Contains(n));

                foreach (GenericGraphNode neighbour in validNeighbours)
                {
                    iterations += 1;

                    // Find næste node fra hver edge, som ikke er den samme som Current
                    GenericGraphNode nextNode = neighbour;

                    if (!cameFromKey.Contains(nextNode))
                    {
                        searchList.Add(nextNode);
                        cameFromKey.Add(nextNode);
                        cameFromValue.Add(current);

                        if (nextNode.Equals(goal))
                        {
                            pathList.Add(nextNode);
                            while (cameFromKey.IndexOf(pathList[pathList.Count - 1]) > -1)
                            {
                                int keyIndex = cameFromKey.IndexOf(pathList[pathList.Count - 1]);
                                pathList.Add(cameFromValue[keyIndex]);
                            }
                            return pathList;
                        }
                    }
                }
            }

            return null;
        }
    }
}
