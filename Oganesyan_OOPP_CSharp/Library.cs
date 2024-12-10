using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Oganesyan_OOPP_CSharp
{
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
}
