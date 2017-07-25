using hw.Library;
using MyUnitTestingLibrary;
using System.Collections.Generic;

namespace MyLibraryTest
{
    [TestClass]
    class TestCustomer
    {
        private string bookTitle;
        private string bookAuthor;
        private bool bookRarity;

        private string custName;
        private string custNumber;

        [TestInit]
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

            foreach (var curBook in customer.GetAllBooks())
            {
                Test.AreEqual(curBook, book);
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
                Test.AreEqual(curBook, book);
                break;
            }

            customer.DelBook(book);

            foreach (var curBook in customer.GetAllBooks())
            {
                Test.AreNotEqual(curBook, book);
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

            foreach (var curBook in listBook)
            {
                customer.AddBook(curBook);
            }

            CollectionTest.AreEqual(customer.GetAllBooks(), listBook);
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

            Test.AreEqual(0, customer.GetExpiredBooks().Count);
            CollectionTest.AreEqual(customer.GetExpiredBooks(), new LinkedList<Book>());
        }
    }
}
