using System;

namespace hw.Library
{
    public class BookAddedEventArgs : System.EventArgs
    {
        public Book Book { get; }
        public String Message { get; }

        public BookAddedEventArgs(Book book, String message)
        {
            this.Book = book;
            this.Message = message;
        }
    }

    public class CustomerAddedEventArgs : System.EventArgs
    {
        public Customer Customer { get; }
        public String Message { get; }

        public CustomerAddedEventArgs(Customer customer, String message)
        {
            this.Customer = customer;
            this.Message = message;
        }
    }

    public class BookStateChangedEventArgs : System.EventArgs
    {
        public Book Book { get; }
        public String Message { get; }
        public BookState State { get; }
        public Customer Customer { get; }

        public BookStateChangedEventArgs(Book book, BookState bookState, String message, Customer customer = null)
        {
            this.Book = book;
            this.Message = message;
            this.State = bookState;
            this.Customer = customer;
        }
    }

    public enum BookState
    {
        IssuedToCustomer,
        ReturnedToLibrary
    }
}
