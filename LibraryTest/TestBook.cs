using NUnit.Framework;
using hw.Library;

namespace LibraryTest
{
    [TestFixture]
    public class TestBook
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
            this.bookRarity = true;

            this.custName = "Vasya";
            this.custNumber = "8(981)853-60-43";
        }

        [Test]
        public void Book_AddCustomer()
        {
            Book book = new Book(this.bookAuthor, this.bookTitle, this.bookRarity);
            Customer customer = new Customer(this.custName, this.custNumber);

            book.AddCustomer(customer);
            Assert.AreEqual(customer, book.Customer);
        }

        [Test]
        public void Book_DelCustomer()
        {
            Book book = new Book(this.bookAuthor, this.bookTitle, this.bookRarity);
            Customer customer = new Customer(this.custName, this.custNumber);
            book.AddCustomer(customer);
            book.DelCustomer();
        
            Assert.IsNull(book.Customer);
        }

        [Test]
        public void Book_GetDate()
        {
            Book book = new Book(this.bookAuthor, this.bookTitle, this.bookRarity);
            Customer customer = new Customer(this.custName, this.custNumber);

            book.AddCustomer(customer);
            Assert.LessOrEqual(System.DateTime.Now, book.DateOfIssue);
            Assert.Less(book.DateOfIssue, System.DateTime.MaxValue);
        }

        [Test]
        public void Book_GetDateIfBookInLibrary()
        {
            Book book = new Book(this.bookAuthor, this.bookTitle, this.bookRarity);
            Customer customer = new Customer(this.custName, this.custNumber);

            book.AddCustomer(customer);
            book.DelCustomer();
            Assert.AreEqual(System.DateTime.MaxValue, book.DateOfIssue);
        }
    }
}
