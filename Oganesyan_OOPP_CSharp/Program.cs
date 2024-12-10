// See https://aka.ms/new-console-template for more information
using Oganesyan_OOPP_CSharp;
using System.Xml.Serialization;

namespace Oganesyan_OOPP_Sharp
{
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