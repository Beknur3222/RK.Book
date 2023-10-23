using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RK.Book
{
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public Book(string title, string author)
        {
            Title = title;
            Author = author;
        }

        public override string ToString()
        {
            return $"Название: {Title}, Автор: {Author}";
        }
    }

    class Program
    {
        static List<Book> library = new List<Book>();
        static string fileName = "library.txt";

        static void Main(string[] args)
        {
            LoadLibrary();

            while (true)
            {
                Console.WriteLine("Библиотека:");
                ShowLibrary();

                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Добавить книгу");
                Console.WriteLine("2. Удалить книгу");
                Console.WriteLine("3. Сохранить библиотеку");
                Console.WriteLine("4. Сохранить и выйти");

                string n = Console.ReadLine();

                switch (n)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        RemoveBook();
                        break;
                    case "3":
                        SaveLibrary();
                        break;
                    case "4":
                        SaveLibrary();
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                        break;
                }
            }
        }

        static void AddBook()
        {
            Console.WriteLine("Введите название книги:");
            string title = Console.ReadLine();

            Console.WriteLine("Введите автора книги:");
            string author = Console.ReadLine();

            Book book = new Book(title, author);
            library.Add(book);
        }

        static void RemoveBook()
        {
            Console.WriteLine("Введите номер книги для удаления:");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= library.Count)
            {
                library.RemoveAt(index - 1);
            }
            else
            {
                Console.WriteLine("Неверный номер книги.");
            }
        }

        static void ShowLibrary()
        {
            for (int i = 0; i < library.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {library[i]}");
            }
        }

        static void SaveLibrary()
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var book in library)
                {
                    writer.WriteLine($"{book.Title},{book.Author}");
                }
            }
            Console.WriteLine("Библиотека сохранена.");
        }

        static void LoadLibrary()
        {
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        string title = parts[0];
                        string author = parts[1];
                        Book book = new Book(title, author);
                        library.Add(book);
                    }
                }
            }
        }
    }
}
