using hw.Library;
using NUnit.Framework;
using System.Collections.Generic;

namespace LibraryTest
{
    [TestFixture]
    class TestCustomer
    {
        private string bookTitle;
        private string bookAuthor;
        private bool bookRarity;

        private string custName;
        private string custNumber;

        [SetUp]
        public void SetUp()
        {
            this.bookTitle = "War and Peace";
            this.bookAuthor = "Leo Tolstoy";
            this.bookRarity = false;

            this.custName = "Vasya";
            this.custNumber = "8(981)853-60-43";
        }

        [Test]
        public void Customer_AddBook()
        {
            Customer customer = new Customer(this.custName, this.custNumber);
            Book book = new Book(this.bookAuthor, this.bookTitle, this.bookRarity);

            customer.AddBook(book);

            foreach(var curBook in customer.GetAllBooks())
            {
                Assert.AreEqual(curBook, book);
            }
        }

        [Test]
        public void Customer_DelBook()
        {
            Customer customer = new Customer(this.custName, this.custNumber);
            Book book = new Book(this.bookAuthor, "Book 1", this.bookRarity);

            customer.AddBook(book);
            customer.AddBook(new Book(this.bookAuthor, "Book 2", this.bookRarity));
            customer.AddBook(new Book(this.bookAuthor, "Book 3", this.bookRarity));

            foreach (var curBook in customer.GetAllBooks())
            {
                Assert.AreEqual(curBook, book);
                break;
            }

            customer.DelBook(book);

            foreach (var curBook in customer.GetAllBooks())
            {
                Assert.AreNotEqual(curBook, book);
            }
        }

        [Test]
        public void Customer_GetAllBooks()
        {
            Customer customer = new Customer(this.custName, this.custNumber);

            LinkedList<Book> listBook = new LinkedList<Book>();
            listBook.AddLast(new Book(this.bookAuthor, "Book 1", this.bookRarity));
            listBook.AddLast(new Book(this.bookAuthor, "Book 2", this.bookRarity));
            listBook.AddLast(new Book(this.bookAuthor, "Book 3", this.bookRarity));

            foreach(var curBook in listBook)
            {
                customer.AddBook(curBook);
            }
            
            CollectionAssert.AreEqual(customer.GetAllBooks(), listBook);
        }

        [Test]
        public void Customer_GetExpiredBooks()
        {
            Customer customer = new Customer(this.custName, this.custNumber);

            LinkedList<Book> listBook = new LinkedList<Book>();
            listBook.AddLast(new Book(this.bookAuthor, "Book 1", this.bookRarity));
            listBook.AddLast(new Book(this.bookAuthor, "Book 2", this.bookRarity));
            listBook.AddLast(new Book(this.bookAuthor, "Book 3", this.bookRarity));

            foreach (var curBook in listBook)
            {
                customer.AddBook(curBook);
            }

            Assert.AreEqual(0, customer.GetExpiredBooks().Count);
            CollectionAssert.AreEqual(customer.GetExpiredBooks(), new LinkedList<Book>());
        }

        [Test]
        public void Customer_GetBookWithIndexer()
        {
            Customer customer = new Customer(this.custName, this.custNumber);

            Book book0 = new Book(this.bookAuthor, "book 1", this.bookRarity);
            Book book1 = new Book(this.bookAuthor, "book 2", this.bookRarity);
            Book book2 = new Book(this.bookAuthor, "book 3", this.bookRarity);
            Book book3 = new Book(this.bookAuthor, "book 4", this.bookRarity);

            customer.AddBook(book0);
            customer.AddBook(book1);
            customer.AddBook(book2);
            customer.AddBook(book3);

            Assert.AreEqual(customer[0], book0);
            Assert.AreEqual(customer[1], book1);
            Assert.AreEqual(customer[2], book2);
            Assert.AreEqual(customer[3], book3);
        }
    }
}
