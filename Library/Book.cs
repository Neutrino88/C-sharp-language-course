using System;

namespace hw.Library
{
    public class Book
    {
        public string Author { get; private set; }
        public string Title  { get; private set; }
        public bool IsRarity { get; private set; }
        public Customer Customer { get; private set; } = null;
        private DateTime _date;
        public DateTime Date{
            get
            {
                if (this.Customer != null)
                {
                    return this._date;
                }
                
                throw new NullReferenceException();
            }
            
            private set
            {
                this._date = value;
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
            this.Date = DateTime.Now;
        }

        public void DelCustomer()
        {
            this.Customer = null;
        }
    }

}
