using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using hw.Library;
using System.Linq;
using System.Xml;

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

    public bool GiveOutBook(Book book, Customer customer)
    {
        if (customer.GetAllBooks().Count == 5 || 
            customer.GetExpiredBooks().Count > 0 ||
            customer.GetAllBooks().Count > 0 && customer.HasRarityBook && book.IsRarity)
        {
            return false;
        }

        book.AddCustomer(customer);
        customer.AddBook(book);

        if (customer.GetAllBooks().Count == 1)
        {
            this.CustomerAdded?.Invoke(this, new CustomerAddedEventArgs(customer, $"A new customer {customer.Name} with number {customer.Number} was added to library"));
        }

        this.BookStateChanged?.Invoke(this, new BookStateChangedEventArgs(book, BookState.IssuedToCustomer, $"A book \"{book.Title}\" by {book.Author} was issued to the customer", customer));

        return true;
    }

    public void ReturnBookToLibrary(Book book)
    {
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
            return (Library) ds.ReadObject(reader);
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
