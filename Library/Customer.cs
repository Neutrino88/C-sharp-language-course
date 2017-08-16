using System;
using System.Linq;
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
                foreach (var curBook in books)
                {
                    if (curBook.IsRarity)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public Customer(string name, string number)
        {
            this.Name = name;
            this.Number = number;
            this.books = new List<Book>();
        }

        public IReadOnlyList<Book> GetAllBooks()
        {
            return this.books;
        }

        public IReadOnlyList<Book> GetExpiredBooks()
        {
            var books =
                from book in this.books
                let ts = book.DateOfIssue - DateTime.Now
                where (ts.Days > 14 && book.DateOfIssue < DateTime.MaxValue)
                select book;

            return books.ToList();
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

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() * 10000 + this.Number.GetHashCode();
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
