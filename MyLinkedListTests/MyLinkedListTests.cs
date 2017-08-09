using NUnit.Framework;
using MyList;

namespace MyLinkedListTests
{
    [TestFixture]
    public class MyLinkedListTests
    {
        [Test]
        public void MyLinkedList_Add()
        {
            MyLinkedList<int> myLinkedList = new MyLinkedList<int>();

            myLinkedList.Add(1);
            myLinkedList.Add(2);
            myLinkedList.Add(3);

            Assert.AreEqual(myLinkedList.Count, 3);
            CollectionAssert.AreEqual(myLinkedList, new int[] { 1, 2, 3 });
        }

        [Test]
        public void MyLinkedList_Clear()
        {
            MyLinkedList<int> myLinkedList = new MyLinkedList<int>() { 1, 2, 3 };
            myLinkedList.Clear();

            CollectionAssert.AreEqual(myLinkedList, new int[] { });
            Assert.AreEqual(myLinkedList.Count, 0);
        }

        [Test]
        public void MyLinkedList_Contains()
        {
            MyLinkedList<int> myLinkedList = new MyLinkedList<int>();

            Assert.IsFalse(myLinkedList.Contains(123));   
            myLinkedList.Add(123);
            Assert.IsTrue(myLinkedList.Contains(123));
            myLinkedList.Remove(123);
            Assert.IsFalse(myLinkedList.Contains(123));
        }

        [Test]
        public void MyLinkedList_CopyTo()
        {
            MyLinkedList<int> myLinkedList = new MyLinkedList<int>() { 4, 5, 6 };
            int[] array = new int[6] { 1, 2, 3, -1, -1, -1 };

            myLinkedList.CopyTo(array, 3);
            CollectionAssert.AreEqual(array, new int[] { 1, 2, 3, 4, 5, 6 });
        }

        [Test]
        public void MyLinkedList_Remove()
        {
            MyLinkedList<int> myLinkedList = new MyLinkedList<int>() { 1, 2, 3, 4 };

            Assert.IsTrue(myLinkedList.Remove(2));
            Assert.AreEqual(myLinkedList.Count, 3);
            CollectionAssert.AreEqual(myLinkedList, new int[] { 1, 3, 4 });

            Assert.IsTrue(myLinkedList.Remove(1));
            Assert.AreEqual(myLinkedList.Count, 2);
            CollectionAssert.AreEqual(myLinkedList, new int[] { 3, 4 });

            Assert.IsTrue(myLinkedList.Remove(4));
            Assert.AreEqual(myLinkedList.Count, 1);
            CollectionAssert.AreEqual(myLinkedList, new int[] { 3 });

            Assert.IsFalse(myLinkedList.Remove(2987));
            Assert.AreEqual(myLinkedList.Count, 1);
            CollectionAssert.AreEqual(myLinkedList, new int[] { 3 });

            Assert.IsTrue(myLinkedList.Remove(3));
            Assert.AreEqual(myLinkedList.Count, 0);
            CollectionAssert.AreEqual(myLinkedList, new int[] { });
        }

        [Test]
        public void MyLinkedList_Insert()
        {
            MyLinkedList<int> myLinkedList = new MyLinkedList<int>() { 1, 2, 4, 5 };
            
            Assert.IsTrue(myLinkedList.Insert(4, 3));
            Assert.AreEqual(myLinkedList.Count, 5);
            CollectionAssert.AreEqual(myLinkedList, new int[] { 1, 2, 3, 4, 5 });

            Assert.IsTrue(myLinkedList.Insert(1, 0));
            Assert.AreEqual(myLinkedList.Count, 6);
            CollectionAssert.AreEqual(myLinkedList, new int[] { 0, 1, 2, 3, 4, 5 });

            Assert.IsFalse(myLinkedList.Insert(2987, 2986));
            Assert.AreEqual(myLinkedList.Count, 6);
            CollectionAssert.AreEqual(myLinkedList, new int[] { 0, 1, 2, 3, 4, 5 });
        }

        [Test]
        public void MyLinkedList_GetEnumerator()
        {
            MyLinkedList<int> myLinkedList = new MyLinkedList<int>() { 10, 21, 32, 43, 54 };

            int i = 10;
            foreach (var item in myLinkedList)
            {
                if (i != item)
                {
                    Assert.Fail();
                }

                i += 11;
            }
        }

        [Test]
        public void MyLinkedList_AddedEvent()
        {
            MyLinkedList<int> myLinkedList = new MyLinkedList<int>();
            int number = 2987;
            
            myLinkedList.Added += (sender, args) =>
            {
                Assert.AreEqual(args.Element, number);
                Assert.AreEqual(args.LinkedListLength, myLinkedList.Count);
            };

            myLinkedList.Add(number);
            ++number;
            myLinkedList.Insert(number-1, number);
        }

        [Test]
        public void MyLinkedList_RemovedEvent()
        {
            MyLinkedList<int> myLinkedList = new MyLinkedList<int>() { 1, 2, 3 };
            int number = 1;

            myLinkedList.Removed += (sender, args) =>
            {
                Assert.AreEqual(args.Element, number);
                Assert.AreEqual(args.LinkedListLength, myLinkedList.Count);
            };

            myLinkedList.Remove(number);
            myLinkedList.Remove(++number);
            myLinkedList.Remove(++number);
            myLinkedList.Remove(++number);
        }

        [Test]
        public void MyLinkedList_ClearedEvent()
        {
            MyLinkedList<int> myLinkedList = new MyLinkedList<int>() { 1, 2, 3 };

            myLinkedList.Cleared += (sender, args) =>
            {
                Assert.AreEqual(args.DeletedItemCount, 3);
                Assert.AreEqual(myLinkedList.Count, 0);
 
            };

            myLinkedList.Clear();
        }
    }
}
