using MyUnitTestingLibrary;
using System.Collections.Generic;
using hw.Library;

namespace MyLibraryTest
{
    [TestClass]
    class TestLibrary
    {
        private string bookAuthor;
        private bool bookRarity;

        private string custName;
        private string custNumber;

        [TestInit]
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

            CollectionTest.AreEqual(lib.GetAllBooks(), listBook);
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

            Test.AreEqual(3, lib.GetAllBooks().Count);
            CollectionTest.AreEqual(lib.GetAllBooks(), listBook);
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

            foreach (var curBook in lib.GetFreeBooks())
            {
                Test.AreNotEqual(listBook.First.Value, curBook);
                Test.AreNotEqual(listBook.Last.Value, curBook);
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
                Test.AreNotEqual(listBook.First.Value, curBook);
                Test.AreNotEqual(listBook.Last.Value, curBook);
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

            CollectionTest.AreEqual(lib.FindBooksByAuthor("Author 1"), listBook);
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

            CollectionTest.AreEqual(lib.FindBooksByTitle("Book 1"), listBook);
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

            CollectionTest.AreEqual(customer.GetAllBooks(), listTakenBook);
            CollectionTest.AreEqual(lib.GetTakenBooks(), listTakenBook);
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

            CollectionTest.AreEqual(lib.GetFreeBooks(), listFreeBook);
        }
    }
}
