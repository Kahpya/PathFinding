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

            Assert.IsTrue(map[0].Edges.Length == 0);
        }

        [TestMethod]
        public void GenericGraph_GraphNodeEdgesAreAvailableOnBothNodesOnInstantiation()
        {
            GenericGraphNode firstNode = new GenericGraphNode();
            GenericGraphNode secondNode = new GenericGraphNode(firstNode);

            Assert.IsTrue(firstNode.Edges.Length > 0 && secondNode.Edges.Length > 0);
        }

        [TestMethod]
        public void GenericGraph_GraphNodeEdgesAreAvailableOnBothNodesOnAssignment()
        {
            GenericGraphNode firstNode = new GenericGraphNode();
            GenericGraphNode secondNode = new GenericGraphNode();

            secondNode.AddEdgeTowardsNode(firstNode);

            Assert.IsTrue(firstNode.Edges.Length > 0 && secondNode.Edges.Length > 0);
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

            Assert.IsTrue(firstNode.Edges.Length == 1 
                && secondNode.Edges.Length == 1
                && fourthNode.Edges.Length == 2);
        }

        [TestMethod]
        public void GenericGraph_FirstTwoNodesHaveAutomaticEdgesBetweenThem()
        {
            GenericGraph map = new GenericGraph();

            GenericGraphNode firstNode = map.Add();
            GenericGraphNode secondNode = map.Add();

            Assert.AreEqual(firstNode.Edges[0].FromNode, secondNode);
        }

        [TestMethod]
        public void GenericGraph_CanCreateEdgesBetweenSpecificNodes()
        {
            GenericGraph map = new GenericGraph();

            GenericGraphNode firstNode = map.Add();
            GenericGraphNode secondNode = map.Add();
            GenericGraphNode thirdNode = map.Add(firstNode);

            Assert.AreEqual(firstNode.Edges[1].FromNode, thirdNode);
        }
    }
}
