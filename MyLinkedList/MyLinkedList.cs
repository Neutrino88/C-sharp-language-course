using System;
using System.Collections;
using System.Collections.Generic;

namespace MyList
{
    public class MyLinkedList<T> : IEnumerable<T>, ICollection<T>
    {
        private class Node<T>
        {
            public T Value { get; set;  }
            public Node<T> Next { get; set;  }
            public Node<T> Prev { get; set;  }

            public Node(T value, Node<T> next, Node<T> prev)
            {
                this.Value = value;
                this.Next = next;
                this.Prev = prev;
            }

            public bool Equals(T item)
            {
                return (this.Value.Equals(item));
            }
        }

        private Node<T> firstNode;
        private Node<T> lastNode;
        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }
        public T First {
            get
            {
                if (this.firstNode != null)
                {
                    return firstNode.Value;
                } else
                {
                    return default(T);
                }
            }
        }
        public T Last
        {
            get
            {
                if (this.lastNode != null)
                {
                    return lastNode.Value;
                }
                else
                {
                    return default(T);
                }
            }
        }
        public event EventHandler<AddedEventsArgs<T>> Added;
        public event EventHandler<RemovedEventsArgs<T>> Removed;
        public event EventHandler<ClearedEventsArgs<T>> Cleared;

        public MyLinkedList()
        {
            this.Count = 0;
            this.firstNode = null;
            this.lastNode = null;
        }
        
        private IEnumerable<Node<T>> GetEnumerableNode<Node>()
        {
            var result = new List<Node<T>>();

            Node<T> curNode = this.firstNode;
            while (curNode != null)
            {
                result.Add(curNode);
                curNode = curNode.Next;
            }

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> curNode = this.firstNode;
            while (curNode != null)
            {
                yield return curNode.Value;
                curNode = curNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(T item)
        {
            Node<T> newNode = new Node<T>(item, null, null);

            if (this.Count == 0)
            {
                this.firstNode = newNode;
                this.lastNode = newNode;
            } else
            {
                newNode.Prev = this.lastNode;
                this.lastNode.Next = newNode;
                this.lastNode = newNode;
            }

            ++this.Count;
            this.Added?.Invoke(this, new AddedEventsArgs<T>(item, this.Count));
        }

        public void Clear()
        {
            this.firstNode = this.lastNode = null;

            int deletedItemCount = this.Count;
            this.Count = 0;

            this.Cleared?.Invoke(this, new ClearedEventsArgs<T>(deletedItemCount));
        }
        
        public bool Contains(T item)
        {
            foreach (var curNode in this)
            {
                if (curNode.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            int i = arrayIndex;
            foreach(var item in this)
            {
                array[i++] = item;
            }
        }

        public bool Remove(T item)
        {
            foreach (Node<T> curNode in this.GetEnumerableNode<Node<T>>())
            {
                if (curNode.Equals(item))
                {
                    Node<T> prevNode = curNode.Prev;
                    Node<T> nextNode = curNode.Next;

                    if (prevNode != null)
                        prevNode.Next = curNode.Next;
                    else
                        firstNode = curNode.Next;

                    if (nextNode != null)
                        nextNode.Prev = curNode.Prev;
                    else
                        lastNode = curNode.Prev;

                    curNode.Next = curNode.Prev = null;
                    curNode.Value = default(T);

                    --this.Count;
                    this.Removed?.Invoke(this, new RemovedEventsArgs<T>(item, this.Count));
                    return true;
                }
            }

            return false;
        }

        public bool Insert(T item, T value)
        {
            Node<T> curNode = this.firstNode;
            while (curNode != null)
            {
                if (curNode.Value.Equals(item))
                {
                    Node<T> newNode = new Node<T>(value, curNode, curNode.Prev);

                    if (newNode.Next != null)   newNode.Next.Prev = newNode;

                    if (newNode.Prev != null)   newNode.Prev.Next = newNode;
                    else
                    {
                        this.firstNode = newNode;
                    }

                    ++this.Count;
                    this.Added?.Invoke(this, new AddedEventsArgs<T>(value, this.Count));
                    return true;
                }
                curNode = curNode.Next;
            }

            return false;
        }
    }
}
