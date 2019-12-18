using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PathfindingConsoleProject.DataStructures;
using PathfindingConsoleProject.Algorithms;

namespace PathfindingUnitTestProject
{
    [TestClass]
    public class BreathFirstUnitTest
    {
        #region BreathFirstSeach Unit Test
        [TestMethod]
        public void IsNullWhenGoalIsNull()
        {
            GenericGraph map = GenerateGraph();

            GenericGraphNode startNode = map[0];
            GenericGraphNode endNode = null;

            Assert.IsNull(BreathFirstSeach.FindPathBetweenNodes(startNode, endNode));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void IsNullWhenSourceIsNull()
        {
            GenericGraph map = GenerateGraph();

            GenericGraphNode startNode = null;
            GenericGraphNode endNode = map[10];

            GenericList<GenericGraphNode> actual = BreathFirstSeach.FindPathBetweenNodes(startNode, endNode);
        }

        [TestMethod]
        public void IsNullWhenSourceIsNewNode()
        {
            GenericGraph map = GenerateGraph();

            GenericGraphNode startNode = new GenericGraphNode();
            GenericGraphNode endNode = map[10];

            Assert.IsNull(BreathFirstSeach.FindPathBetweenNodes(startNode, endNode));
        }

        [TestMethod]
        public void IsNullWhenGoalIsNewNode()
        {
            GenericGraph map = GenerateGraph();

            GenericGraphNode startNode = map[10];
            GenericGraphNode endNode = new GenericGraphNode();
            
            Assert.IsNull(BreathFirstSeach.FindPathBetweenNodes(startNode, endNode));
        }

        [TestMethod]
        public void AreEqualWhenExpectedIsTheFirstElementOfActual()
        {
            GenericGraph map = GenerateGraph();

            GenericGraphNode startNode = map[1];
            GenericGraphNode expected = map[3];

            GenericList<GenericGraphNode> actual = BreathFirstSeach.FindPathBetweenNodes(startNode, expected);

            Assert.AreEqual(expected, actual[0]);
        }

        [TestMethod]
        public void AreEqualWhenCollectionExpecteIsActual()
        {
            GenericGraph map = GenerateGraph();

            GenericGraphNode startNode = map[1];
            GenericGraphNode endNode = map[3];

            GenericList<GenericGraphNode> expected = new GenericList<GenericGraphNode>();
            expected.AddRange(new GenericGraphNode[] { map[3], map[2], map[1] });

            GenericList<GenericGraphNode> actual = BreathFirstSeach.FindPathBetweenNodes(startNode, endNode);

            CollectionAssert.AreEqual(expected, actual);

            // [0, 3]
            // [4, 9]
            // 9-3 + 4-0 = count
        }

        [TestMethod]
        public void AreEqualManhattenDistance()
        {
            GenericGraph map = GenerateGraph();
            int x = 4;
            int y = 9;
            int height = 10;
            int mapIndex = (y * height) + x;

            GenericGraphNode startNode = map[3];
            GenericGraphNode endNode = map[mapIndex];

            int expected = ((9 - 0) + (4 - 3)) + 1;

            GenericList<GenericGraphNode> actual = BreathFirstSeach.FindPathBetweenNodes(startNode, endNode);

            Assert.AreEqual(expected, actual.Count);
        }
        #endregion

        #region Test map Creator
        private static GenericGraph GenerateGraph()
        {
            GenericGraph map = new GenericGraph();
            int height = 10;
            int width = 10;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int thisIndex = (i * height) + j;

                    if (i == 0 && j == 0)
                    {
                        GenericGraphNode node = map.Add(); // Add upper left corner
                        node.x = j;
                        node.y = i;
                    }
                    else if (i == 0 && j > 0)
                    {
                        GenericGraphNode node = map.Add(map[j - 1]); // add first row
                        node.x = j;
                        node.y = i;
                    }
                    else
                    {
                        int upperIndex = ((i - 1) * height) + j;

                        if (j == 0)
                        {
                            GenericGraphNode node = map.Add(map[upperIndex]);
                            node.x = j;
                            node.y = i;
                        }
                        else if (j > 0)
                        {
                            GenericGraphNode[] targetNodes = new GenericGraphNode[2]
                            {
                                map[upperIndex],
                                map[thisIndex -1]
                            };

                            GenericGraphNode node = map.Add(targetNodes);
                            node.x = j;
                            node.y = i;
                        }
                    }
                }
            }

            return map;
        }
        #endregion
    }
}
