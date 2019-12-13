using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PathfindingConsoleProject.DataStructures;

namespace PathfindingUnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GenericList_Not_Null()
        {
            GenericList<int> list = new GenericList<int>();

            Assert.IsNotNull(list);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GenericList_Int_Zero_Count()
        {
            GenericList<int> list = new GenericList<int>();

            int actual = list[2];
        }

        [TestMethod]
        public void GenericList_Int_Contains()
        {
            GenericList<int> list = new GenericList<int>();
            int value = 5;

            list.Add(value);

            CollectionAssert.Contains(list, value);
        }
    }
}
