using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomStructures;

namespace QueueTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IndexingTest()
        {
            Queue<int> a = new Queue<int>(1);
            a.Enqueue(2);
            Assert.AreEqual(a[0], 2);
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void FailWithWrongIndexing1()
        {
            var a = new Queue<int>(10, 3);
            Assert.Equals(typeof(IndexOutOfRangeException), a[-1]);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void FailWithWrongIndexing2()
        {
            var a = new Queue<int>(10, 3);
            Assert.Equals(typeof(IndexOutOfRangeException), a[10]);
        }

        [TestMethod]
        public void RightLengthTest()
        {
            Queue<int> a = new Queue<int>(10, 3);
            Assert.AreEqual(a.Count, 10);
        }
        [TestMethod]
        public void RightLengthTest2()
        {
            Queue<int> a = new Queue<int>(1);
            Assert.AreEqual(a.Count, 0);
        }

        [TestMethod]
        public void ConstructorTest2()
        {
            Queue<int> a = new Queue<int>(1, 2);
            Assert.AreEqual(2, a[0]);
        }

        [TestMethod]
        public void DequeueTest1()
        {
            Queue<int> a = new Queue<int>(1, 2);
            a.Dequeue();
            Assert.AreEqual(0, a.Count);
        }

        [TestMethod]
        public void BinaryTreeComparationTest1()
        {
            BinaryTree<int> a = new BinaryTree<int>(1);
            a.Add(1);
            Assert.IsTrue(a[0] == a[1]);
        }

        [TestMethod]
        public void SearchTest()
        {
            BinaryTree<int> a = new BinaryTree<int>(5);
            a.Add(2);
            a.Add(6);
            Assert.AreEqual(2, a.Search(6));
        }

        [TestMethod]
        public void StringTest()
        {
            BinaryTree<int> a = new BinaryTree<int>(5);
            a.Add(2);
            a.Add(6);
            Assert.AreEqual("5 2 6 ", a.ToString());
        }

        [TestMethod]
        public void LeavesTest()
        {
            BinaryTree<int> a = new BinaryTree<int>(5);
            a.Add(2);
            a.Add(6);
            Assert.AreEqual("2 6 ", a.Leaves());
        }


    }
}
