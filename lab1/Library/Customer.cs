using System;
using System.Collections.Generic;

namespace hw.Library
{
    public class Customer
    {
        LinkedList<Book> books;
        public string Name { get; private set; }
        public string Number { get; private set; }
        public bool HasRarityBook {
            get
            {
                bool hasRarityBook = false;

                foreach (var curBook in books)
                {
                    hasRarityBook |= curBook.IsRarity; 
                }

                return hasRarityBook;
            }
        }

        public Customer(string name, string number)
        {
            this.Name = name;
            this.Number = number;
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
                    TimeSpan ts = curBook.DateOfIssue - DateTime.Now;

                    if (ts.Days > 14 && curBook.DateOfIssue < DateTime.MaxValue)
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
