using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingConsoleProject.DataStructures
{
    public class GenericGraphNode
    {
        public int x;
        public int y;

        public GenericGraphNode[] Neighbours { get => neighbours; }
        GenericGraphNode[] neighbours;

        public GenericGraphNode()
        {
            this.neighbours = new GenericGraphNode[] { };
        }

        public GenericGraphNode(GenericGraphNode node)
        {
            this.neighbours = new GenericGraphNode[] { };
            AddEdgeTowardsNode(node);
        }

        public GenericGraphNode(GenericGraphNode[] nodes)
        {
            this.neighbours = new GenericGraphNode[] { };
            for (int i = 0; i < nodes.Length; i++)
            {
                AddEdgeTowardsNode(nodes[i]);
            }
        }

        public void AddEdgeTowardsNode(GenericGraphNode targetNode)
        {
            AddEdge(targetNode);
            targetNode.AddEdge(this);
        }

        public void AddEdge(GenericGraphNode neighbour)
        {
            if (neighbour.Equals(this))
            {
                return;
            }
            GenericGraphNode[] newEdges = new GenericGraphNode[neighbours.Length + 1];

            for (int i = 0; i < neighbours.Length; i++)
            {
                newEdges[i] = neighbours[i];
            }

            newEdges[newEdges.Length - 1] = neighbour;
            neighbours = newEdges;
        }
    }
}
