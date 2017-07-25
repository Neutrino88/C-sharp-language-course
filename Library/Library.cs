using System;
using System.Collections.Generic;
using hw.Library;

public class Library
{
    private LinkedList<Book> booksList;

    public Library()
    {
        booksList = new LinkedList<Book>();
    }

    public void AddBook(Book newBook)
    {
        this.booksList.AddLast(newBook);
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
        return true;
    }

    public void ReturnBookToLibrary(Book book)
    {
        book.Customer.DelBook(book);
        book.DelCustomer();
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