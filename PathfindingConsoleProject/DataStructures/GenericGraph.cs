using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingConsoleProject.DataStructures
{
    public class GenericGraph : ICollection
    {
        public GenericGraphNode this[int index]
        {
            get
            {
                if (Count != 0 && 0 <= index && index < Count)
                {
                    return list[index];
                }

                throw new IndexOutOfRangeException();

            }
            set
            {
                if (0 <= index && index < Count)
                {
                    list[index] = value;
                }

                throw new IndexOutOfRangeException();
            }
        }

        public int Count
        {
            get;
            private set;
        } = 0;

        public object SyncRoot => list.SyncRoot;

        public bool IsSynchronized => list.IsSynchronized;

        private GenericGraphNode[] list;
        private int size = 4;

        public GenericGraph()
        {
            this.list = new GenericGraphNode[size];
        }

        public GenericGraph(int size)
        {
            this.size = size;
            this.list = new GenericGraphNode[size];
        }

        public GenericGraphNode Add()
        {
            GenericGraphNode newNode;
            if (Count == 0)
            {
                newNode = new GenericGraphNode();
            }
            else
            {
                newNode = new GenericGraphNode(list[Count - 1]);
            }

            list[Count] = newNode;
            Count++;

            Resize();
            return newNode;
        }

        public GenericGraphNode Add(GenericGraphNode nodeToCreateEdgeTowards)
        {
            GenericGraphNode newNode = new GenericGraphNode(nodeToCreateEdgeTowards);

            list[Count] = newNode;
            Count++;

            Resize();
            return newNode;
        }

        public GenericGraphNode Add(GenericGraphNode[] nodesToCreateEdgesTowards)
        {
            GenericGraphNode newNode = new GenericGraphNode(nodesToCreateEdgesTowards);

            list[Count] = newNode;
            Count++;

            Resize();
            return newNode;
        }

        public void CopyTo(Array array, int index)
        {
            list.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return list.GetEnumerator();
        }

        private void Resize()
        {
            if (Count % size == 0)
            {
                GenericGraphNode[] temp = new GenericGraphNode[Count + size];
                list.CopyTo(temp, 0);

                list = temp;
            }
        }
    }
}
