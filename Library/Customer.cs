using System;
using System.Collections.Generic;

namespace hw.Library
{
    public class Customer
    {
        public string Name { get; private set; }
        public string Number { get; private set; }
        public bool HasRarityBook { get; private set; }
        LinkedList<Book> books;

        public Customer(string name, string number)
        {
            this.Name = name;
            this.Number = number;
            this.HasRarityBook = false;
            this.books = new LinkedList<Book>();
        }

        public ICollection<Book> GetAllBooks()
        {
            return this.books;
        }

        public ICollection<Book> GetExpiredBooks()
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
            this.HasRarityBook = this.HasRarityBook | book.IsRarity;
        }

        public void DelBook(Book book)
        {
            this.books.Remove(book);
            this.HasRarityBook ^= book.IsRarity;
        }
    }
}
