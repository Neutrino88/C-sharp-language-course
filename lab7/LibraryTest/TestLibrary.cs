﻿using NUnit.Framework;
using System.Collections.Generic;
using hw.Library;

namespace LibraryTest
{
    [TestFixture]
    class TestLibrary
    {
        private string bookAuthor;
        private bool bookRarity;

        private string custName;
        private string custNumber;

        [SetUp]
        public void SetUp()
        {
            this.bookAuthor = "Author";
            this.bookRarity = false;

            this.custName = "Vasya";
            this.custNumber = "8(981)853-60-43";
        }

        [Test]
        public void Library_AddBook()
        {
            Library lib = new Library();

            LinkedList<Book> listBook = new LinkedList<Book>();
            listBook.AddLast(new Book(this.bookAuthor, "Book 1", this.bookRarity));
            listBook.AddLast(new Book(this.bookAuthor, "Book 2", this.bookRarity));
            listBook.AddLast(new Book(this.bookAuthor, "Book 3", this.bookRarity));

            foreach (var curBook in listBook)
            {
                lib.AddBook(curBook);
            }

            CollectionAssert.AreEqual(lib.GetAllBooks(), listBook);
        }

        [Test]
        public void Library_GetAllBooks()
        {
            Library lib = new Library();
            LinkedList<Book> listBook = new LinkedList<Book>();

            listBook.AddLast(new Book(this.bookAuthor, "Book 1", this.bookRarity));
            listBook.AddLast(new Book(this.bookAuthor, "Book 2", this.bookRarity));
            listBook.AddLast(new Book(this.bookAuthor, "Book 3", this.bookRarity));

            foreach (var curBook in listBook)
            {
                lib.AddBook(curBook);
            }

            Assert.AreEqual(3, lib.GetAllBooks().Count);
            CollectionAssert.AreEqual(lib.GetAllBooks(), listBook);
        }

        [Test]
        public void Library_GetFreeBooks()
        {
            Library lib = new Library();
            Customer customer = new Customer(this.custName, this.custNumber);

            LinkedList<Book> listBook = new LinkedList<Book>();
            listBook.AddLast(new Book(this.bookAuthor, "Book 1", this.bookRarity));
            listBook.AddLast(new Book(this.bookAuthor, "Book 2", this.bookRarity));
            listBook.AddLast(new Book(this.bookAuthor, "Book 3", this.bookRarity));

            foreach (var curBook in listBook)
            {
                lib.AddBook(curBook);
            }

            lib.GiveOutBook(listBook.First.Value, customer);
            lib.GiveOutBook(listBook.Last.Value, customer);

            foreach(var curBook in lib.GetFreeBooks())
            {
                Assert.AreNotEqual(listBook.First.Value, curBook);
                Assert.AreNotEqual(listBook.Last.Value, curBook);
            }
        }

        [Test]
        public void Library_GetTakenBooks()
        {
            Library lib = new Library();
            Customer customer = new Customer(this.custName, this.custNumber);

            LinkedList<Book> listBook = new LinkedList<Book>();
            listBook.AddLast(new Book(this.bookAuthor, "Book 1", this.bookRarity));
            listBook.AddLast(new Book(this.bookAuthor, "Book 2", this.bookRarity));
            listBook.AddLast(new Book(this.bookAuthor, "Book 3", this.bookRarity));

            foreach (var curBook in listBook)
            {
                lib.AddBook(curBook);
            }

            lib.GiveOutBook(listBook.First.Next.Value, customer);

            foreach (var curBook in lib.GetTakenBooks())
            {
                Assert.AreNotEqual(listBook.First.Value, curBook);
                Assert.AreNotEqual(listBook.Last.Value, curBook);
            }
        }

        [Test]
        public void Library_FindBooksByAuthor()
        {
            Library lib = new Library();
            Customer customer = new Customer(this.custName, this.custNumber);

            LinkedList<Book> listBook = new LinkedList<Book>();
            listBook.AddLast(new Book("Author 1", "Book 1", this.bookRarity));
            listBook.AddLast(new Book("Author 1", "Book 2", this.bookRarity));

            lib.AddBook(listBook.First.Value);
            lib.AddBook(listBook.Last.Value);

            lib.GiveOutBook(listBook.First.Value, customer);

            CollectionAssert.AreEqual(lib.FindBooksByAuthor("Author 1"), listBook);
        }

        [Test]
        public void Library_FindBooksByTitle()
        {
            Library lib = new Library();
            Customer customer = new Customer(this.custName, this.custNumber);

            LinkedList<Book> listBook = new LinkedList<Book>();
            listBook.AddLast(new Book("Author 1", "Book 1", this.bookRarity));
            listBook.AddLast(new Book("Author 2", "Book 1", this.bookRarity));

            lib.AddBook(listBook.First.Value);
            lib.AddBook(listBook.Last.Value);

            lib.GiveOutBook(listBook.First.Value, customer);

            CollectionAssert.AreEqual(lib.FindBooksByTitle("Book 1"), listBook);
        }

        [Test]
        public void Library_GiveOutBook()
        {
            Library lib = new Library();
            Customer customer = new Customer(this.custName, this.custNumber);
            LinkedList<Book> listTakenBook = new LinkedList<Book>();

            listTakenBook.AddLast(new Book(this.bookAuthor, "Book 1", this.bookRarity));
            listTakenBook.AddLast(new Book(this.bookAuthor, "Book 2", this.bookRarity));

            lib.AddBook(listTakenBook.First.Value);
            lib.AddBook(listTakenBook.Last.Value);
            lib.AddBook(new Book(this.bookAuthor, "Book 3", this.bookRarity));

            lib.GiveOutBook(listTakenBook.First.Value, customer);
            lib.GiveOutBook(listTakenBook.Last.Value, customer);

            CollectionAssert.AreEqual(customer.GetAllBooks(), listTakenBook);
            CollectionAssert.AreEqual(lib.GetTakenBooks(), listTakenBook);
        }

        [Test]
        public void Library_ReturnBookToLibrary()
        {
            Library lib = new Library();
            Customer customer = new Customer(this.custName, this.custNumber);
            Book takenBook = new Book(this.bookAuthor, "Book 3", this.bookRarity);

            LinkedList<Book> listFreeBook = new LinkedList<Book>();
            listFreeBook.AddLast(new Book(this.bookAuthor, "Book 1", this.bookRarity));
            listFreeBook.AddLast(new Book(this.bookAuthor, "Book 2", this.bookRarity));

            lib.AddBook(listFreeBook.First.Value);
            lib.AddBook(listFreeBook.Last.Value);
            lib.AddBook(takenBook);

            lib.GiveOutBook(listFreeBook.First.Value, customer);
            lib.GiveOutBook(listFreeBook.Last.Value, customer);
            lib.GiveOutBook(takenBook, customer);

            lib.ReturnBookToLibrary(listFreeBook.First.Value);
            lib.ReturnBookToLibrary(listFreeBook.Last.Value);

            CollectionAssert.AreEqual(lib.GetFreeBooks(), listFreeBook);
        }

        [Test]
        public void Library_GetBookByAuthorAndTitleWithIndexer()
        {
            Library lib = new Library();

            Book book1 = new Book("Author 1", "Book 1", this.bookRarity);
            Book book2 = new Book("Author 1", "Book 2", this.bookRarity);
            Book book3 = new Book("Author 2", "Book 1", this.bookRarity);
            Book book4 = new Book("Author 2", "Book 2", this.bookRarity);

            lib.AddBook(book1);
            lib.AddBook(book2);
            lib.AddBook(book3);
            lib.AddBook(book4);

            Assert.AreEqual(lib["Author 1", "Book 1"], book1);
            Assert.AreEqual(lib["Author 1", "Book 2"], book2);
            Assert.AreEqual(lib["Author 2", "Book 1"], book3);
            Assert.AreEqual(lib["Author 2", "Book 2"], book4);
            Assert.AreEqual(lib["Author", "Book"], null);
        }

        [Test]
        public void Library_OnAddedBookEvent()
        {
            Library lib = new Library();

            Book book = new Book("Author 1", "Book 1", true);

            lib.BookAdded += (sender, eventArgs) =>
            {
                Assert.AreEqual(book, eventArgs.Book);
            };

            lib.AddBook(book);
        }

        [Test]
        public void Library_OnAddedCustomerEvent()
        {
            Library lib = new Library();
            Book book = new Book("Author 1", "Book 1", true);
            Customer customer = new Customer("Customer 1", "22-34-56");

            lib.CustomerAdded += (sender, eventArgs) =>
            {
                Assert.AreEqual(customer, eventArgs.Customer);
            };

            lib.AddBook(book);
            lib.GiveOutBook(book, customer);
        }

        [Test]
        public void Library_OnBookStateChangedEvent()
        {
            Library lib = new Library();
            Book book = new Book("Author 1", "Book 1", true);
            Customer customer = new Customer("Customer 1", "22-34-56");

            lib.BookStateChanged += (sender, eventArgs) =>
            {
                if (eventArgs.State == BookState.IssuedToCustomer)
                {
                    Assert.AreEqual(customer, eventArgs.Customer);
                    Assert.AreEqual(book, eventArgs.Book);
                } else if (eventArgs.State == BookState.ReturnedToLibrary)
                {
                    Assert.Null(eventArgs.Customer);
                    Assert.AreEqual(book, eventArgs.Book);
                } else
                {
                    Assert.Fail();
                }
            };

            lib.AddBook(book);
            lib.GiveOutBook(book, customer);
        }

        [Test]
        public void Library_Serialization()
        {
            Library lib = new Library();

            Book book1 = new Book("Author 1", "Title 1", true);
            Book book2 = new Book("Author 1", "Title 2", false);
            Book book3 = new Book("Author 2", "Title 2", true);

            Customer customer1 = new Customer("name 1", "22-33-44");
            Customer customer2 = new Customer("name 2", "22-44-33");

            lib.AddBook(book1);
            lib.AddBook(book2);
            lib.AddBook(book3);

            lib.GiveOutBook(book1, customer1);
            lib.GiveOutBook(book2, customer2);

            lib.WriteToFile("Test_Libraty_WriteToObject.xml");

            Library lib2 = Library.ReadFromFile("Test_Libraty_WriteToObject.xml");

            Assert.NotNull(lib2);
            Assert.AreEqual(3, lib2.GetAllBooks().Count);

            Assert.AreEqual(book1, lib2[book1.Author, book1.Title]);
            Assert.AreEqual(book2, lib2[book2.Author, book2.Title]);
            Assert.AreEqual(book3, lib2[book3.Author, book3.Title]);

            Assert.AreEqual(book1.IsRarity, lib2[book1.Author, book1.Title].IsRarity);
            Assert.AreEqual(book2.IsRarity, lib2[book2.Author, book2.Title].IsRarity);
            Assert.AreEqual(book3.IsRarity, lib2[book3.Author, book3.Title].IsRarity);

            Assert.AreEqual(book1.DateOfIssue, lib2[book1.Author, book1.Title].DateOfIssue);
            Assert.AreEqual(book2.DateOfIssue, lib2[book2.Author, book2.Title].DateOfIssue);
            Assert.AreEqual(book3.DateOfIssue, lib2[book3.Author, book3.Title].DateOfIssue);
            
            Assert.AreEqual(book1.Customer, lib2[book1.Author, book1.Title].Customer);
            Assert.AreEqual(book2.Customer, lib2[book2.Author, book2.Title].Customer);
            Assert.AreEqual(book3.Customer, lib2[book3.Author, book3.Title].Customer);
            Assert.IsNull(book3.Customer);

            Assert.AreEqual(book1.Customer.GetAllBooks(), lib2[book1.Author, book1.Title].Customer.GetAllBooks());
            Assert.AreEqual(book2.Customer.GetAllBooks(), lib2[book2.Author, book2.Title].Customer.GetAllBooks());

            System.IO.File.Delete("Test_Libraty_WriteToObject.xml");
        }
    }
}