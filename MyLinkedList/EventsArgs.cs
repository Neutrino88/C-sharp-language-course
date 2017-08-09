namespace MyList
{
    public class AddedEventsArgs<T> : System.EventArgs
    {
        public T Element { get; }
        public int LinkedListLength { get; }

        public AddedEventsArgs(T elem, int linkedListLength)
        {
            this.Element = elem;
            this.LinkedListLength = linkedListLength;
        }
    }

    public class RemovedEventsArgs<T> : System.EventArgs
    {
        public T Element { get; }
        public int LinkedListLength { get; }

        public RemovedEventsArgs(T elem, int linkedListLength)
        {
            this.Element = elem;
            this.LinkedListLength = linkedListLength;
        }
    }

    public class ClearedEventsArgs<T> : System.EventArgs
    {
        public int DeletedItemCount { get; }

        public ClearedEventsArgs(int deletedItemCount)
        {
            this.DeletedItemCount = deletedItemCount;
        }
    }
}
