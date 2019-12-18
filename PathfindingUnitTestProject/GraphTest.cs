using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathfindingConsoleProject.DataStructures;

namespace PathfindingUnitTestProject
{
    [TestClass]
    public class GraphTest
    {
        [TestMethod]
        public void GenericGraph_Not_Null()
        {
            GenericGraph list = new GenericGraph();

            Assert.IsNotNull(list);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GenericGraph_Index_Out_Range_MinusOne()
        {
            GenericGraph list = new GenericGraph();

            GenericGraphNode actual = list[-1];
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GenericGraph_Index_Out_Range_PlusOne()
        {
            GenericGraph list = new GenericGraph();

            GenericGraphNode actual = list[1];
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GenericGraph_Set_Index_Out_Range_MinusOne()
        {
            GenericGraph list = new GenericGraph();

            GenericGraphNode actual = list[1] = null;
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GenericGraph_Set_Index_Out_Range_PlusOne()
        {
            GenericGraph list = new GenericGraph();

            GenericGraphNode actual = list[1] = null;
        }

        [TestMethod]
        public void GenericGraph_CountOnInstantiation()
        {
            int expected = 0;
            GenericGraph list = new GenericGraph();

            Assert.AreEqual(expected, list.Count);
        }

        [TestMethod]
        public void GenericGraph_CountOnAdding()
        {
            int expected = 6;
            GenericGraph list = new GenericGraph();
            for (int i = 0; i < expected; i++)
            {
                list.Add();
            }

            Assert.AreEqual(expected, list.Count);
        }

        [TestMethod]
        public void GenericGraph_Resize()
        {
            int initalSize = 2;
            int expectedCount = 7;
            GenericGraph actual = new GenericGraph(initalSize);

            for (int i = 0; i < expectedCount; i++)
            {
                actual.Add();
            }

            Assert.IsTrue(actual.Count == expectedCount);
        }

        [TestMethod]
        public void GenericGraph_FirstNodeHasNoEdges()
        {
            GenericGraph map = new GenericGraph();
            map.Add();

            Assert.IsTrue(map[0].Neighbours.Length == 0);
        }

        [TestMethod]
        public void GenericGraph_GraphNodeEdgesAreAvailableOnBothNodesOnInstantiation()
        {
            GenericGraphNode firstNode = new GenericGraphNode();
            GenericGraphNode secondNode = new GenericGraphNode(firstNode);

            Assert.IsTrue(firstNode.Neighbours.Length > 0 && secondNode.Neighbours.Length > 0);
        }

        [TestMethod]
        public void GenericGraph_GraphNodeEdgesAreAvailableOnBothNodesOnAssignment()
        {
            GenericGraphNode firstNode = new GenericGraphNode();
            GenericGraphNode secondNode = new GenericGraphNode();

            secondNode.AddEdgeTowardsNode(firstNode);

            Assert.IsTrue(firstNode.Neighbours.Length > 0 && secondNode.Neighbours.Length > 0);
        }

        [TestMethod]
        public void GenericGraph_GraphNodeEdgesAreAvailableOnBothNodesOnArrayInstantiation()
        {
            GenericGraphNode firstNode = new GenericGraphNode();
            GenericGraphNode secondNode = new GenericGraphNode();
            GenericGraphNode thirdNode = new GenericGraphNode();

            GenericGraphNode[] nodeArray = new GenericGraphNode[]
            {
                firstNode,
                secondNode
            };
            GenericGraphNode fourthNode = new GenericGraphNode(nodeArray);

            Assert.IsTrue(firstNode.Neighbours.Length == 1 
                && secondNode.Neighbours.Length == 1
                && fourthNode.Neighbours.Length == 2);
        }

        [TestMethod]
        public void GenericGraph_FirstTwoNodesHaveAutomaticEdgesBetweenThem()
        {
            GenericGraph map = new GenericGraph();

            GenericGraphNode firstNode = map.Add();
            GenericGraphNode secondNode = map.Add();

            Assert.AreEqual(firstNode.Neighbours[0], secondNode);
        }

        [TestMethod]
        public void GenericGraph_AllNodesHaveAutomaticEdgesBetweenThem()
        {
            GenericGraph map = new GenericGraph();

            GenericGraphNode firstNode = map.Add();
            GenericGraphNode secondNode = map.Add();
            GenericGraphNode third = map.Add();
            GenericGraphNode fourth = map.Add();

            Assert.IsTrue(firstNode.Neighbours[0].Equals( secondNode) 
                && secondNode.Neighbours[0].Equals(firstNode) 
                && secondNode.Neighbours[1].Equals(third)
                && third.Neighbours[0].Equals(secondNode)
                && third.Neighbours[1].Equals(fourth)
                && fourth.Neighbours[0].Equals(third));
        }

        [TestMethod]
        public void GenericGraph_CanCreateEdgesBetweenSpecificNodes()
        {
            GenericGraph map = new GenericGraph();

            GenericGraphNode firstNode = map.Add();
            GenericGraphNode secondNode = map.Add();
            GenericGraphNode thirdNode = map.Add(firstNode);

            Assert.AreEqual(firstNode.Neighbours[1], thirdNode);
        }
    }
}
