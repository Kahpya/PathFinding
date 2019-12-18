using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PathfindingConsoleProject.Algorithms;
using PathfindingConsoleProject.DataStructures;

namespace PathfindingUnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        #region GenericList Unit Tests
        [TestMethod]
        public void GenericList_Not_Null()
        {
            GenericList<int> list = new GenericList<int>();

            Assert.IsNotNull(list);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GenericList_Index_Out_Range_MinusOne()
        {
            GenericList<int> list = new GenericList<int>();

            int actual = list[-1];
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GenericList_Index_Out_Range_Count_Plus_One()
        {
            GenericList<int> list = new GenericList<int>();
            list.Add(4);

            int actual = list[1];
        }

        [TestMethod]
        public void GenericList_Index_Inside_Of_Range()
        {
            GenericList<int> actual = new GenericList<int>();
            actual.Add(4);
            int[] exptected = new int[] { 4 };

            CollectionAssert.AreEqual(exptected, actual);
        }

        [TestMethod]
        public void GenericList_Int_Contains()
        {
            GenericList<int> actual = new GenericList<int>();
            actual.Add(5);

            CollectionAssert.Contains(actual, 5);
        }

        [TestMethod]
        public void GenericList_Int_AddRange()
        {
            int[] expected = new int[] { 7, 20, 12, -11 };
            GenericList<int> actual = new GenericList<int>(5);
            actual.AddRange(expected);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GenericList_Int_Resize()
        {
            int initalSize = 2;
            int[] expected = new int[] { 5, 2, 6, 14, 14, 123, 5511, 231 };
            GenericList<int> actual = new GenericList<int>(initalSize);

            actual.AddRange(expected);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GenericList_Int_Count()
        {
            int[] expected = new int[] { 4, 18, 32, 1 };
            GenericList<int> actual = new GenericList<int>();

            actual.AddRange(expected);

            Assert.AreEqual(expected.Length, actual.Count);
        }

        [TestMethod]
        public void GenericList_Int_CopyTo()
        {
            int[] actual = new int[3];
            GenericList<int> expected = new GenericList<int>(3);

            expected.AddRange(new int[] { 4, 1, 16 });

            expected.CopyTo(actual, 0);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GenericList_Clear()
        {
            GenericList<int> expected = new GenericList<int>();
            GenericList<int> actual = new GenericList<int>();
            actual.AddRange(new int[] { 4, 1, 2, 5 });
            actual.Add(6);

            actual.Clear();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GenericList_Contains_True()
        {
            int value = 5;
            GenericList<int> actual = new GenericList<int>();
            actual.Add(value);

            Assert.IsTrue(actual.Contains(value));
        }

        [TestMethod]
        public void GenericList_Contains_False()
        {
            int actual = 5;
            GenericList<int> list = new GenericList<int>();
            list.Add(actual);

            Assert.IsFalse(list.Contains(7));
        }

        [TestMethod]
        public void GenericList_Remove_True()
        {
            int[] expected = new int[] { 4, 2, 1, 5 };
            GenericList<int> actual = new GenericList<int>();
            actual.AddRange(new int[] { 4, 2, 1, 7, 5 });

            Assert.IsTrue(actual.Remove(7));
        }

        [TestMethod]
        public void GenericList_Remove_False()
        {
            int[] expected = new int[] { 4, 2, 1, 5 };
            GenericList<int> actual = new GenericList<int>();
            actual.AddRange(expected);

            Assert.IsFalse(actual.Remove(7));
        }

        [TestMethod]
        public void GenericList_Remove_Default()
        {
            int[] expected = new int[] { 4 };
            GenericList<int> actual = new GenericList<int>();
            actual.AddRange(expected);

            Assert.IsTrue(actual.Remove(4));
        }

        [TestMethod]
        public void GenericList_Get_Item_By_Index()
        {
            int[] expected = new int[] { 4, 1, 2, 5 };
            GenericList<int> actual = new GenericList<int>();
            actual.AddRange(expected);

            Assert.AreEqual(expected[2], actual[2]);
        }

        [TestMethod]
        public void GenericList_Set_Item_At_Index()
        {
            int value = 10;
            int[] expected = new int[] { 4, 1, 2, 5 };
            GenericList<int> actual = new GenericList<int>();
            actual.AddRange(expected);

            expected[3] = value;
            actual[3] = value;

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GenericList_Set_Item_Outside_Of_Bounds_MinusOne()
        {
            GenericList<int> actual = new GenericList<int>();
            actual.AddRange(new int[] { 4, 1, 2, 5 });

            actual[-1] = 10;
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GenericList_Set_Item_Outside_Of_Bounds_PlusOne()
        {
            GenericList<int> actual = new GenericList<int>();
            actual.AddRange(new int[] { 4, 1, 2, 5 });

            actual[4] = 10;
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void GenericList_GetIsReadOnly()
        {
            GenericList<int> actual = new GenericList<int>();
            actual.AddRange(new int[] { 4, 1, 2, 5 });

            bool isReadonly = actual.IsReadOnly;
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void GenericList_GetIsSynchronized()
        {
            GenericList<int> actual = new GenericList<int>();
            actual.AddRange(new int[] { 4, 1, 2, 5 });

            bool isReadonly = actual.IsSynchronized;
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void GenericList_GetSyncRoot()
        {
            GenericList<int> actual = new GenericList<int>();
            actual.AddRange(new int[] { 4, 1, 2, 5 });

            object sync = actual.SyncRoot;
        }
        #endregion
    }
}
