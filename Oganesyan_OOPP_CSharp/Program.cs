// See https://aka.ms/new-console-template for more information
using System.Xml.Serialization;

namespace Oganesyan_OOPP_Sharp
{
    [Serializable]
    [XmlType(TypeName = "Book")]
    public class Book
    {
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public int ReleaseYear { get; set; } = 0;
        public string PublishingHouse { get; set; } = "";
        public bool InStock { get; set; } = false;
        public double Rating { get; set; } = 0.0;
        public Book() { }

        public virtual void Input()
        {
            Console.Write("Введите название книги: ");
            Title = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(Title))
            {
                Console.Write("Книга не может быть без названия. Введите название книги: ");
                Title = Console.ReadLine();
            }

            Console.Write("Введите автора книги: ");
            Author = Console.ReadLine();

            Console.Write("Введите год выпуска книги (от 1700 до 2024): ");
            ReleaseYear = Program.GetCorrectData(1700, 2024);

            Console.Write("Введите издательство книги: ");
            PublishingHouse = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(PublishingHouse))
            {
                Console.Write("Издательство не может быть пустым. Введите издательство: ");
                PublishingHouse = Console.ReadLine();
            }

            Console.Write("Есть ли книга в наличии (1 - Да, 0 - Нет): ");
            InStock = Program.GetCorrectData(0, 1) == 1;

            Console.Write("Введите рейтинг книги (от 0.0 до 5.0): ");
            Rating = Program.GetCorrectData(0.0, 5.0);
        }

        public virtual void Output()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                Console.WriteLine("Нет добавленных книг.");
            }
            else
            {
                Console.WriteLine($"Название: {Title}");
                Console.WriteLine($"Автор: {Author}");
                Console.WriteLine($"Год выпуска: {ReleaseYear}");
                Console.WriteLine($"Издательство: {PublishingHouse}");
                Console.WriteLine($"В наличии: {(InStock ? "Да" : "Нет")}");
                Console.WriteLine($"Рейтинг: {Rating:F1}");
            }
        }
    }

    [Serializable]
    [XmlType(TypeName = "EBook")]
    public class EBook : Book
    {
        public string Link { get; set; } = "";
        public EBook() { }

        public override void Input()
        {
            base.Input();
            Console.Write("Введите ссылку на электронную книгу: ");
            Link = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(Link))
            {
                Console.Write("У электронной книги должна быть ссылка. Введите ссылку: ");
                Link = Console.ReadLine();
            }
        }

        public override void Output()
        {
            base.Output();
            Console.WriteLine($"Ссылка: {Link}");
        }
    }
    public class Library
    {
        private List<Book> books = new List<Book>();

        public void AddBook()
        {
            var book = new Book();
            book.Input();
            books.Add(book);
        }

        public void AddEBook()
        {
            var ebook = new EBook();
            ebook.Input();
            books.Add(ebook);
        }

        public void OutputBooks()
        {
            if (!books.Any())
            {
                Console.WriteLine("\nУ вас нет книг для просмотра.");
                return;
            }

            Console.WriteLine("\nКниги: ");
            foreach (var book in books)
            {
                book.Output();
                Console.WriteLine();
            }
        }

        public void SaveBooks()
        {
            Console.Write("\nВведите название файла для сохранения: ");
            var filename = Console.ReadLine();

            var xs = new XmlSerializer(typeof(List<Book>), new[] { typeof(Book), typeof(EBook) });

            using (Stream fs = new FileStream(filename + ".xml", FileMode.OpenOrCreate))
            {
                xs.Serialize(fs, books);
            }

            Console.WriteLine("\nДанные успешно сохранены.");
        }

        public void LoadBooks()
        {
            Console.Write("\nВведите название файла для загрузки: ");
            var filename = Console.ReadLine();

            if (File.Exists(filename + ".xml"))
            {
                try
                {
                    var xs = new XmlSerializer(typeof(List<Book>), new[] { typeof(Book), typeof(EBook) });
                    using (Stream fs = new FileStream(filename + ".xml", FileMode.Open))
                    {
                        var deserializedBooks = xs.Deserialize(fs) as List<Book>;
                        if (deserializedBooks == null)
                        {
                            Console.WriteLine("\nОшибка загрузки данных: файл пуст или поврежден.");
                            return;
                        }
                        books = deserializedBooks;
                        Console.WriteLine("\nДанные успешно загружены.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
        }

        public void Clear()
        {
            books.Clear();
            Console.WriteLine("\nСписок книг очищен.");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var library = new Library();

            while (true)
            {
                Console.WriteLine("\n         МЕНЮ");
                Console.WriteLine("1. Добавить книгу;");
                Console.WriteLine("2. Добавить электронную книгу;");
                Console.WriteLine("3. Просмотреть все книги;");
                Console.WriteLine("4. Записать данные в файл;");
                Console.WriteLine("5. Загрузить данные из файла;");
                Console.WriteLine("6. Очистить список;");
                Console.WriteLine("0. Выход.");
                Console.Write("\nВыберите пункт меню: ");

                int number = GetCorrectData(0, 6);
                switch (number)
                {
                    case 1: library.AddBook(); break;
                    case 2: library.AddEBook(); break;
                    case 3: library.OutputBooks(); break;
                    case 4: library.SaveBooks(); break;
                    case 5: library.LoadBooks(); break;
                    case 6: library.Clear(); break;
                    case 0: return;
                }
            }
        }

        public static T GetCorrectData<T>(T min, T max) where T : IComparable<T>
        {
            T input;
            while (true)
            {
                try
                {
                    input = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));

                    if (input.CompareTo(min) >= 0 && input.CompareTo(max) <= 0)
                    {
                        return input;
                    }
                    else
                    {
                        Console.Write($"\nВведите корректные данные! Введите число от {min} до {max}: ");
                    }
                }
                catch
                {
                    Console.WriteLine("Ошибка ввода! Попробуйте снова.");
                }
            }
        }
    }
}