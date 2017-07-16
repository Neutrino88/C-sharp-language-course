using System;
using System.Collections.Generic;

namespace hw.Library
{
    public class Customer
    {
        LinkedList<Book> books;
        public string Name { get; private set; }
        public string Number { get; private set; }
        private bool _hasRarityBook;
        public bool HasRarityBook {
            get
            {
                this._hasRarityBook = false;

                foreach (var curBook in books)
                {
                    this._hasRarityBook |= curBook.IsRarity; 
                }

                return this._hasRarityBook;
            }
            private set
            {
                this._hasRarityBook = value;
            }
        }

        public Customer(string name, string number)
        {
            this.Name = name;
            this.Number = number;
            this.HasRarityBook = false;
            this.books = new LinkedList<Book>();
        }

        public IReadOnlyCollection<Book> GetAllBooks()
        {
            return this.books;
        }

        public IReadOnlyCollection<Book> GetExpiredBooks()
        {
            LinkedList<Book> books = new LinkedList<Book>();

            foreach(var curBook in this.books)
            {
                try
                {
                    TimeSpan ts = curBook.Date - DateTime.Now;

                    if (ts.Days > 14)
                    {
                        books.AddLast(curBook);
                    }
                } catch(Exception ignored)
                {

                }
            }

            return books;
        }

        public void AddBook(Book book)
        {
            this.books.AddLast(book);
        }

        public void DelBook(Book book)
        {
            this.books.Remove(book);
        }
    }
}
