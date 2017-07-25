using System;
using hw.Library;
using System.Collections.Generic;

public class Program
{
	public static void Main(string[] args)
	{
        Library lib = new Library();

        Book book1 = new Book("author", "title1", false);
        Book book2 = new Book("author", "title2", true);
        Book book3 = new Book("author", "title3", false);
        Book book4 = new Book("author", "title4", false);
        Book book5 = new Book("author", "title5", true);
        Book book6 = new Book("author", "title6", false);
        Book book7 = new Book("author", "Kolobok", false);
        Book book8 = new Book("author", "title8", false);
        Book book9 = new Book("author", "title9", false);
        Book book0 = new Book("Author", "Kolobok", true);

        lib.AddBook(book1);
        lib.AddBook(book2);
        lib.AddBook(book3);
        lib.AddBook(book4); 
        lib.AddBook(book5);
        lib.AddBook(book6);
        lib.AddBook(book7);
        lib.AddBook(book8);
        lib.AddBook(book9);
        lib.AddBook(book0);
        PrintBooks(lib.GetAllBooks());

        // User 1
        Customer cust1 = new Customer("Ivan","+7(927)197-27-27");
        Console.WriteLine("\n" + cust1.Name + " " + cust1.Number);

        foreach (var book in lib.FindBooksByAuthor("author"))
        {
            lib.GiveOutBook(book, cust1);
        }

        PrintBooks(cust1.GetAllBooks());

        lib.ReturnBookToLibrary(book2);
        PrintBooks(cust1.GetAllBooks());

        // User 2
        Customer cust2 = new Customer("Vasya", "8(800)555-35-35");
        Console.WriteLine("\n" + cust2.Name + " " + cust2.Number);

        foreach (var book in lib.GetFreeBooks())
        {
            lib.GiveOutBook(book, cust2);
        }
        PrintBooks(cust2.GetAllBooks());
        PrintBooks(lib.GetTakenBooks());
        PrintBooks(lib.GetFreeBooks());

        // Other
        foreach (var book in lib.GetTakenBooks())
        {
            lib.ReturnBookToLibrary(book);
        }

        Console.WriteLine(book1.DateOfIssue);

        PrintBooks(lib.GetFreeBooks());
        PrintBooks(cust1.GetAllBooks());
        PrintBooks(cust2.GetAllBooks());

        Console.WriteLine("\nBooks with 'Kolobok' title:");
        foreach(var book in lib.FindBooksByTitle("Kolobok"))
        {
            Console.WriteLine(" " + book.Author + ", " + book.Title + ", rarity_" + book.IsRarity);
        }

        Console.ReadKey(true);
	}

    public static void PrintBooks(IReadOnlyCollection<Book> books)
    {
        Console.WriteLine(books.Count +  " books:");

        foreach(var book in books)
        {
            if (book.Customer != null)
            {
                Console.Write(book.DateOfIssue + "     ");
            }
            Console.WriteLine(book.Author + ", " + book.Title + ", rarity_" + book.IsRarity);
        }
    }
}
