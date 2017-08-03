using System;
using System.Collections.Generic;
using hw.Library;

public class Library
{
    private LinkedList<Book> booksList;
    public event EventHandler<BookAddedEventArgs> BookAdded;
    public event EventHandler<CustomerAddedEventArgs> CustomerAdded;
    public event EventHandler<BookStateChangedEventArgs> BookStateChanged;

    public Library()
    {
        booksList = new LinkedList<Book>();
    }

    protected virtual void OnBookAdded(BookAddedEventArgs args)
    {
        this.BookAdded?.Invoke(this, args);
    }

    protected virtual void OnCustomerAdded(CustomerAddedEventArgs args)
    {
        this.CustomerAdded?.Invoke(this, args);
    }

    protected virtual void OnBookStateChanged(BookStateChangedEventArgs args)
    {
        this.BookStateChanged?.Invoke(this, args);
    }

    public void AddBook(Book newBook)
    {
        this.booksList.AddLast(newBook);
        this.OnBookAdded(new BookAddedEventArgs(newBook, $"Added a new book \"{newBook.Title}\" by {newBook.Author}"));
    }

    public IReadOnlyCollection<Book> GetAllBooks()
    {
        return this.booksList;
    }

    public IReadOnlyCollection<Book> GetFreeBooks()
    {
        LinkedList<Book> freeBooksList = new LinkedList<Book>();

        foreach (var curBook in this.booksList)
        {
            if (curBook.Customer == null)
            {
                freeBooksList.AddLast(curBook);
            }
        }
        
        return freeBooksList;
    }

    public IReadOnlyCollection<Book> GetTakenBooks()
    {
        LinkedList<Book> takenBooksList = new LinkedList<Book>();

        foreach (var curBook in this.booksList)
        {
            if (curBook.Customer != null)
            {
                takenBooksList.AddLast(curBook);
            }
        }

        return takenBooksList;
    }

    public IReadOnlyCollection<Book> FindBooksByAuthor(String author)
    {
        LinkedList<Book> trueBooks = new LinkedList<Book>();

        foreach (var curBook in this.booksList)
        {
            if (curBook.Author == author)
            {
                trueBooks.AddLast(curBook);
            }
        }

        return trueBooks;
    }

    public IReadOnlyCollection<Book> FindBooksByTitle(String title)
    {
        LinkedList<Book> trueBooks = new LinkedList<Book>();

        foreach (var curBook in this.booksList)
        {
            if (curBook.Title == title)
            {
                trueBooks.AddLast(curBook);
            }
        }

        return trueBooks;
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
            this.OnCustomerAdded(new CustomerAddedEventArgs(customer, $"Added a new customer {customer.Name} with number {customer.Number}"));
        }

        this.OnBookStateChanged(new BookStateChangedEventArgs(book, BookState.IssuedToCustomer, $"A book \"{book.Title}\" by {book.Author} issued to the customer", customer));

        return true;
    }

    public void ReturnBookToLibrary(Book book)
    {
        book.Customer.DelBook(book);
        book.DelCustomer();
        this.OnBookStateChanged(new BookStateChangedEventArgs(book, BookState.ReturnedToLibrary, $"A book \"{book.Title}\" by {book.Author} returned to the library"));

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
