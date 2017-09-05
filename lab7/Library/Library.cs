using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Xml;

namespace hw.Library
{
    [DataContract, KnownType(typeof(Book))]
    public class Library
    {
        [DataMember] private LinkedList<Book> booksList;
        public event EventHandler<BookAddedEventArgs> BookAdded;
        public event EventHandler<CustomerAddedEventArgs> CustomerAdded;
        public event EventHandler<BookStateChangedEventArgs> BookStateChanged;

        public Library()
        {
            booksList = new LinkedList<Book>();
        }

        public void AddBook(Book newBook)
        {
            this.booksList.AddLast(newBook);
            this.BookAdded?.Invoke(this, new BookAddedEventArgs(newBook, $"A new book \"{newBook.Title}\" by {newBook.Author} was added to library"));
        }

        public IReadOnlyList<Book> GetAllBooks()
        {
            return this.booksList.ToList();
        }

        public IReadOnlyList<Book> GetFreeBooks()
        {
            return this.booksList.Where(book => book.Customer == null).ToList();
        }

        public IReadOnlyList<Book> GetTakenBooks()
        {
            return this.booksList.Where(book => book.Customer != null).ToList();
        }

        public IReadOnlyList<Book> FindBooksByAuthor(String author)
        {
            return this.booksList.Where(book => book.Author == author).ToList();
        }

        public IReadOnlyList<Book> FindBooksByTitle(String title)
        {
            return this.booksList.Where(book => book.Title == title).ToList();
        }

        public void GiveOutBook(Book book, Customer customer)
        {
            if (book == null)     throw new ArgumentNullException("book");
            if (customer == null) throw new ArgumentNullException("customer");
            if (this.booksList.Find(book) == null) throw new ArgumentException("Book isn't found in library");
            if (book.Customer != null) throw new InvalidOperationException("Book has been issued another customer");
            if (customer.GetExpiredBooks().Count > 0)    throw new InvalidOperationException("Customer has an expired book");
            if (customer.HasRarityBook && book.IsRarity) throw new InvalidOperationException("Customer already has a rarity book");
            if (customer.GetAllBooks().Count == 5)       throw new InvalidOperationException("Customer has the maximum amount of books");
            
            book.AddCustomer(customer);
            customer.AddBook(book);

            if (customer.GetAllBooks().Count == 1)
            {
                this.CustomerAdded?.Invoke(this, new CustomerAddedEventArgs(customer, $"A new customer {customer.Name} with number {customer.Number} was added to library"));
            }

            this.BookStateChanged?.Invoke(this, new BookStateChangedEventArgs(book, BookState.IssuedToCustomer, $"A book \"{book.Title}\" by {book.Author} was issued to the customer", customer));
        }

        public void ReturnBookToLibrary(Book book)
        {
            if (book == null) throw new ArgumentNullException("book");
            if (this.booksList.Find(book) == null) throw new ArgumentException("Book isn't belong this library");
            if (this.booksList.Find(book).Value.Customer == null) throw new ArgumentException("Book is already in the library");
            
            book.Customer.DelBook(book);
            book.DelCustomer();
            this.BookStateChanged?.Invoke(this, new BookStateChangedEventArgs(book, BookState.ReturnedToLibrary, $"A book \"{book.Title}\" by {book.Author} was returned to the library"));
        }

        public void WriteToFile(String fileName)
        {
            DataContractSerializer ds = new DataContractSerializer(typeof(Library), null, 1000, false, true, null);
            XmlWriterSettings setting = new XmlWriterSettings() { Indent = true };

            using (XmlWriter writer = XmlWriter.Create(fileName, setting))
            {
                ds.WriteObject(writer, this);
            }
        }

        public static Library ReadFromFile(String fileName)
        {
            DataContractSerializer ds = new DataContractSerializer(typeof(Library), null, 1000, false, true, null);

            using (XmlReader reader = XmlReader.Create(fileName))
            {
                return (Library)ds.ReadObject(reader);
            }
        }

        public Book this[String author, String title]
        {
            get
            {
                foreach (var book in this.booksList)
                {
                    if (book.Author == author && book.Title == title)
                    {
                        return book;
                    }
                }

                return null;
            }
        }
    }
}