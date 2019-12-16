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

        public GenericGraphEdge[] Edges { get => edges; }
        GenericGraphEdge[] edges;

        public GenericGraphNode()
        {
            this.edges = new GenericGraphEdge[] { };
        }

        public GenericGraphNode(GenericGraphEdge[] edges)
        {
            this.edges = edges;
        }

        public GenericGraphNode(GenericGraphNode node)
        {
            this.edges = new GenericGraphEdge[] { };
            AddEdgeTowardsNode(node);
        }

        public GenericGraphNode(GenericGraphNode[] nodes)
        {
            this.edges = new GenericGraphEdge[] { };
            for (int i = 0; i < nodes.Length; i++)
            {
                AddEdgeTowardsNode(nodes[i]);
            }
        }

        public void AddEdgeTowardsNode(GenericGraphNode targetNode)
        {
            GenericGraphEdge newEdge = new GenericGraphEdge(targetNode, this);

            AddEdge(newEdge);
            targetNode.AddEdge(newEdge);
        }

        public void AddEdge(GenericGraphEdge edge)
        {
            GenericGraphEdge[] newEdges = new GenericGraphEdge[edges.Length + 1];

            for (int i = 0; i < edges.Length; i++)
            {
                newEdges[i] = edges[i];
            }

            newEdges[newEdges.Length - 1] = edge;
            edges = newEdges;
        }
    }
}
