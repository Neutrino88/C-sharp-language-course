using System;
using System.Collections.Generic;

namespace hw.Library
{
    public class Customer
    {
        private List<Book> books;
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
            this.books = new List<Book>();
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
                } catch(Exception)
                {

                }
            }

            return books;
        }

        public void AddBook(Book book)
        {
            this.books.Add(book);
        }

        public void DelBook(Book book)
        {
            this.books.Remove(book);
        }

        public override bool Equals(object obj)
        {
            if (obj is Customer)
            {
                Customer customer = (Customer)obj;

                return (this.Name.Equals(customer.Name) &&
                    this.Number.Equals(customer.Number));
            }

            return false;
        }

        public Book this[int index]
        {
            get
            {
                return this.books[index];
            }
        }
    }
}
