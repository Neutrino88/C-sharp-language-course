using System;

namespace hw.Library
{ 
    public class Book
    {
        public string Author { get; private set; }
        public string Title  { get; private set; }
        public bool IsRarity { get; private set; }
        public Customer Customer { get; private set; } = null;
        private DateTime _dateOfIssue;
        public DateTime DateOfIssue{
            get
            {
                if (this.Customer != null)
                {
                    return this._dateOfIssue;
                }

                return DateTime.MaxValue;
            }
            
            private set
            {
                this._dateOfIssue = value;
            }
        }
        
        public Book(string author, string title, bool isRarity)
        {
            this.Author = author;
            this.Title = title;
            this.IsRarity = isRarity;
        }

        public void AddCustomer(Customer customer)
        {
            this.Customer = customer;
            this.DateOfIssue = DateTime.Now;
        }

        public void DelCustomer()
        {
            this.Customer = null;
        }

        public override bool Equals(object obj)
        {
            if (obj is Book)
            {
                Book book = (Book)obj;

                return (this.Author.Equals(book.Author) &&
                    this.Title.Equals(book.Title) &&
                    this.IsRarity == book.IsRarity);
            }

            return false;
        }
        
        public override int GetHashCode()
        {
            return this.Author.GetHashCode() * 10000 + this.Title.GetHashCode();
        }
    }

}
