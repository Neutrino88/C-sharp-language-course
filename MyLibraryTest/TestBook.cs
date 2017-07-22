using MyUnitTestingLibrary;
using hw.Library;

namespace MyLibraryTest
{
    [TestClass]
    public class TestBook
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
            Test.AreNotEqual(customer, book.Customer);
        }

        [Test]
        public void Book_DelCustomer()
        {
            Book book = new Book(this.bookAuthor, this.bookTitle, this.bookRarity);
            Customer customer = new Customer(this.custName, this.custNumber);
            book.AddCustomer(customer);
            book.DelCustomer();

            Test.IsNull(book.Customer);
        }

        [Test]
        public void Book_GetDate()
        {
            Book book = new Book(this.bookAuthor, this.bookTitle, this.bookRarity);
            Customer customer = new Customer(this.custName, this.custNumber);

            book.AddCustomer(customer);
            Test.LessOrEqual(System.DateTime.Now, book.DateOfIssue);
            Test.Less(book.DateOfIssue, System.DateTime.MaxValue);
        }

        [Test]
        public void Book_GetDateIfBookInLibrary()
        {
            Book book = new Book(this.bookAuthor, this.bookTitle, this.bookRarity);
            Customer customer = new Customer(this.custName, this.custNumber);

            book.AddCustomer(customer);
            book.DelCustomer();
            Test.AreEqual(System.DateTime.MaxValue, book.DateOfIssue);
        }
    }
}
