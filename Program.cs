using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Устанавливаем поддержку русского языка в консоли
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            GF number_1 = new GF("100100100100111101011010101010000101010101001");
            GF number_2 = new GF("111011010101000000111101010100011111101010101");
            GF number_3 = new GF("111010100101111011110110101101011111101010101");


            Console.WriteLine("\nСумма элементов: a + b = c");
            Console.WriteLine("{0} + {1} = {2}", number_1.Element, number_2.Element, (number_1 + number_2).Element);
            Console.WriteLine("{0} + {1} = {2}", number_2.Element, number_3.Element, (number_2 + number_3).Element);
            Console.WriteLine("{0} + {1} = {2}", number_3.Element, number_1.Element, (number_3 + number_1).Element);

            Console.WriteLine("\nПроизведение элементов: a * b = c");
            Console.WriteLine("{0} * {1} = {2}", number_1.Element, number_2.Element, (number_1 * number_2).Element);
            Console.WriteLine("{0} * {1} = {2}", number_2.Element, number_3.Element, (number_2 * number_3).Element);
            Console.WriteLine("{0} * {1} = {2}", number_3.Element, number_1.Element, (number_3 * number_1).Element);

            Console.WriteLine("\nСтепень: a в степени b равно c");
            Console.WriteLine("{0} ** {1} = {2}", number_1.Element, number_2.Element, number_1.Pow(number_2));

            Console.WriteLine("\nОбратный к элементу: a^-1 = b");
            Console.WriteLine("{0}^-1 = {1}", number_1.Element, number_1.Y());
            Console.WriteLine("{0}^-1 = {1}", number_2.Element, number_2.Y());
            Console.WriteLine("{0}^-1 = {1}", number_3.Element, number_3.Y());

            Console.WriteLine("\nСлед элемента: Tr(a) = b;");
            number_1._Trace();
            number_2._Trace();

            Console.WriteLine("\n\nПРОВЕРКА: ");

            Console.WriteLine("\nСимметричность сложения: a + b = b + a");
            Console.WriteLine("{0} + {1} = {2} == {3} = {1} + {0}", number_1.Element, number_2.Element, (number_1 + number_2).Element, (number_2 + number_1).Element);

            Console.WriteLine("\nСимметричность умножения: a * b = b * a");
            Console.WriteLine("{0} * {1} = {2} == {3} = {1} * {0}", number_1.Element, number_2.Element, (number_1 * number_2).Element, (number_2 * number_1).Element);

            Console.WriteLine("\nДистрибутивность: (a + b) * c = (a * c) + (b * c)");
            Console.WriteLine("({0} + {1}) * {2} = {3} == {4} = ({0} * {2}) + ({1} * {2})", number_1.Element, number_2.Element, number_3.Element, ((number_1 + number_2) * number_3).Element, ((number_1 * number_3) + (number_2 * number_3)).Element);

            Console.WriteLine("\nПроизведение обратного на сам элемент: a^-1 * a = 1");
            Console.WriteLine("{0}^-1 * {0} = {1}", number_1.Element, (new GF(number_1.Y()) * number_1).Element);
            Console.WriteLine("{0}^-1 * {0} = {1}", number_2.Element, (new GF(number_2.Y()) * number_2).Element);
            Console.WriteLine("{0}^-1 * {0} = {1}", number_3.Element, (new GF(number_3.Y()) * number_3).Element);

        }
    }
}
