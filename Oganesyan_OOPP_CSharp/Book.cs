using Oganesyan_OOPP_Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Oganesyan_OOPP_CSharp
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
}
