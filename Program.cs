using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookManager
{
    // Book Class
    class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public double Price { get; set; }
    }

    class Program
    {
        // Book List
        static List<Book> books = new List<Book>();

        // JSON File Path
        static string filePath = "books.json";

        static async Task Main(string[] args)
        {
            // Load books from JSON file
            await LoadBooksAsync();

            while (true)
            {
                Console.WriteLine("\n========= BOOK MANAGEMENT =========");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. View Books");
                Console.WriteLine("3. Search Book");
                Console.WriteLine("4. Remove Book");
                Console.WriteLine("5. Sort Books");
                Console.WriteLine("6. Exit");

                Console.Write("\nChoose Option: ");

                int choice;

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        await AddBook();
                        break;

                    case 2:
                        ViewBooks();
                        break;

                    case 3:
                        SearchBook();
                        break;

                    case 4:
                        await RemoveBook();
                        break;

                    case 5:
                        SortBooks();
                        break;

                    case 6:
                        Console.WriteLine("Exiting Application...");
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        // Add Book
        static async Task AddBook()
        {
            try
            {
                Book book = new Book();

                Console.Write("Enter Book Id: ");
                book.Id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Title: ");
                book.Title = Console.ReadLine();

                Console.Write("Enter Author: ");
                book.Author = Console.ReadLine();

                Console.Write("Enter Price: ");
                book.Price = Convert.ToDouble(Console.ReadLine());

                books.Add(book);

                await SaveBooksAsync();

                Console.WriteLine("Book Added Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // View Books
        static void ViewBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("No books available.");
                return;
            }

            Console.WriteLine("\n========= BOOK LIST =========");

            foreach (var book in books)
            {
                Console.WriteLine(
                    $"ID: {book.Id} | " +
                    $"Title: {book.Title} | " +
                    $"Author: {book.Author} | " +
                    $"Price: {book.Price}");
            }
        }

        // Search Book using LINQ
        static void SearchBook()
        {
            Console.Write("Enter title to search: ");

            string title = Console.ReadLine();

            var result = books
                .Where(b => b.Title != null &&
                            b.Title.ToLower().Contains(title.ToLower()))
                .ToList();

            if (result.Count > 0)
            {
                Console.WriteLine("\n========= SEARCH RESULT =========");

                foreach (var book in result)
                {
                    Console.WriteLine(
                        $"ID: {book.Id} | " +
                        $"Title: {book.Title} | " +
                        $"Author: {book.Author} | " +
                        $"Price: {book.Price}");
                }
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        // Remove Book
        static async Task RemoveBook()
        {
            try
            {
                Console.Write("Enter Book Id to remove: ");

                int id = Convert.ToInt32(Console.ReadLine());

                var book = books.FirstOrDefault(b => b.Id == id);

                if (book != null)
                {
                    books.Remove(book);

                    await SaveBooksAsync();

                    Console.WriteLine("Book Removed Successfully!");
                }
                else
                {
                    Console.WriteLine("Book not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Sort Books using LINQ
        static void SortBooks()
        {
            var sortedBooks = books
                .OrderBy(b => b.Title)
                .ToList();

            if (sortedBooks.Count == 0)
            {
                Console.WriteLine("No books available.");
                return;
            }

            Console.WriteLine("\n========= SORTED BOOKS =========");

            foreach (var book in sortedBooks)
            {
                Console.WriteLine(
                    $"ID: {book.Id} | " +
                    $"Title: {book.Title} | " +
                    $"Author: {book.Author} | " +
                    $"Price: {book.Price}");
            }
        }

        // Load Books from JSON File
        static async Task LoadBooksAsync()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string jsonData =
                        await File.ReadAllTextAsync(filePath);

                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        books = JsonSerializer.Deserialize<List<Book>>(jsonData);

                        if (books == null)
                        {
                            books = new List<Book>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading books: {ex.Message}");
            }
        }

        // Save Books to JSON File
        static async Task SaveBooksAsync()
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(
                    books,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

                await File.WriteAllTextAsync(filePath, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving books: {ex.Message}");
            }
        }
    }
}