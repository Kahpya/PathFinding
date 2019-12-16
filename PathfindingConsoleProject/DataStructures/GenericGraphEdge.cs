using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingConsoleProject.DataStructures
{
    public struct GenericGraphEdge
    {
        public GenericGraphNode FromNode { get => from; }
        public GenericGraphNode ToNode { get => to; }

        GenericGraphNode from;
        GenericGraphNode to;

        public GenericGraphEdge (GenericGraphNode to, GenericGraphNode from)
        {
            this.from = from;
            this.to = to;
        }
    }
}
