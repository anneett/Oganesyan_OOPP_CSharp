using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Oganesyan_OOPP_CSharp
{
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
}
