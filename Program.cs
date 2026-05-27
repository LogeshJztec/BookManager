using System;
using System.Collections.Generic;
using System.Linq;

namespace BookManager
{
    // Book class
    class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
    }

    class Program
    {
        static List<Book> books = new List<Book>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n===== BOOK MANAGEMENT =====");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. View Books");
                Console.WriteLine("3. Search Book");
                Console.WriteLine("4. Remove Book");
                Console.WriteLine("5. Sort Books");
                Console.WriteLine("6. Exit");

                Console.Write("Choose Option: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddBook();
                        break;

                    case 2:
                        ViewBooks();
                        break;

                    case 3:
                        SearchBook();
                        break;

                    case 4:
                        RemoveBook();
                        break;

                    case 5:
                        SortBooks();
                        break;

                    case 6:
                        return;

                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
            }
        }

        // Add Book
        static void AddBook()
        {
            Book book = new Book();

            Console.Write("Enter Id: ");
            book.Id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Title: ");
            book.Title = Console.ReadLine();

            Console.Write("Enter Author: ");
            book.Author = Console.ReadLine();

            Console.Write("Enter Price: ");
            book.Price = Convert.ToDouble(Console.ReadLine());

            books.Add(book);

            Console.WriteLine("Book Added Successfully!");
        }

        // View Books
        static void ViewBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("No books available.");
                return;
            }

            foreach (var book in books)
            {
                Console.WriteLine(
                    $"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Price: {book.Price}");
            }
        }

        // Search Book using LINQ
        static void SearchBook()
        {
            Console.Write("Enter title to search: ");
            string title = Console.ReadLine();

            var result = books
                .Where(b => b.Title.ToLower().Contains(title.ToLower()))
                .ToList();

            if (result.Count > 0)
            {
                foreach (var book in result)
                {
                    Console.WriteLine(
                        $"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Price: {book.Price}");
                }
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        // Remove Book
        static void RemoveBook()
        {
            Console.Write("Enter Book Id to remove: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var book = books.FirstOrDefault(b => b.Id == id);

            if (book != null)
            {
                books.Remove(book);
                Console.WriteLine("Book Removed Successfully!");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        // Sort Books using LINQ
        static void SortBooks()
        {
            var sortedBooks = books.OrderBy(b => b.Title).ToList();

            Console.WriteLine("\nBooks Sorted By Title:");

            foreach (var book in sortedBooks)
            {
                Console.WriteLine(
                    $"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Price: {book.Price}");
            }
        }
    }
}