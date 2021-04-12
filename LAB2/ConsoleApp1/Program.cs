using System;
using System.Xml;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            var command = "";
            Console.WriteLine("Выберите режим работы:\n 1. Сортировка входной строки \n 2. Запуск генерации XML файла");
            while (true)
            {
                Console.Write("Режим - ");
                command = Console.ReadLine();
                if (command != "1" && command != "2")
                {
                    Console.WriteLine("***Ошибка ввода*** \nВведите 1 или 2");
                }
                else break;
            }
            Console.Clear();
            if(command == "1")
            {
                Console.WriteLine("Выбран режим сортировки входной строки");
                Console.Write("Введите строку целых чисел, разделяя их пробелом \nСтрока - ");
                var input = new int[0];
                while (true)
                {
                    try
                    {
                        input = Console.ReadLine().Trim().Split(' ').Select(int.Parse).ToArray();
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("***Неверный формат входных данных***");
                        Console.Write("Строка - ");
                    }
                }

                var comparisons = BubbleSort(input);
                Console.Write("Результат сортировки: ");
                foreach (var a in input)
                {
                    Console.Write($"{a} ");
                }
                Console.WriteLine($"\nСравнений: {comparisons}");
                Console.WriteLine("\nРабота программы завершена. Можете закрыть программу");
                Console.ReadKey();
            }
            else if(command == "2")
            {
                Console.WriteLine("Сейчас начнется генерация XML файла, " +
                    "в который будут помещены тесты, \nсодержащие длину " +
                    "строки (от 2 до 1000) и количество операций,\nпотребованных для сортировки этой строки \n");

                var path = @"../../../Data.xml";
                var xml = new XmlTextWriter(path, Encoding.UTF8);
                xml.WriteStartDocument();
                xml.WriteStartElement("experiments");
                xml.WriteEndElement();
                xml.Close();


                var document = new XmlDocument();
                document.Load(path);

                var rnd = new Random();
                Console.WriteLine("Арифмитическая прогрессия");
                Console.WriteLine("   Перемешанный массив");
                for (var i = 2; i <= 1000; i += 5)
                {
                    var input = ArithmeticProgression(rnd.Next(-1000, 1000), rnd.Next(0, 100), i);
                    ArrayShuffle(input);
                    var comparisons = BubbleSort(input);
                    AddToXML(document, i, comparisons);
                }
                Console.WriteLine("   ***Выполнено");
                Console.WriteLine("   Массив по убыванию");
                for (var i = 2; i <= 1000; i += 5)
                {
                    var input = ArithmeticProgression(rnd.Next(-1000, 1000), rnd.Next(-100, 0), i);
                    var comparisons = BubbleSort(input);
                    AddToXML(document, i, comparisons);
                }
                Console.WriteLine("   ***Выполнено");

                Console.WriteLine("Геометрическая прогрессия");
                Console.WriteLine("   Перемешанный массив");
                for (var i = 2; i <= 1000; i += 5)
                {
                    var input = GeometricProgression(rnd.Next(-10, 10), rnd.Next(-10, 10), i);
                    ArrayShuffle(input);
                    var comparisons = BubbleSort(input);
                    AddToXML(document, i, comparisons);
                }
                Console.WriteLine("   ***Выполнено");
                Console.WriteLine("   Массив по убыванию");
                for (var i = 2; i <= 1000; i += 5)
                {
                    var input = GeometricProgression(rnd.Next(0, 10), rnd.Next(0, 10), i);
                    input.Reverse();
                    var comparisons = BubbleSort(input);
                    AddToXML(document, i, comparisons);
                }
                Console.WriteLine("   ***Выполнено");

                Console.WriteLine("\nГенерация XML файла успешно завершена. Можете закрыть программу");
                Console.ReadKey();
                document.Save(path);
            }
        }

        public static int BubbleSort(int[] array)
        {
            var comparisons = 0;
            var isSortComplete = false;
            while (!isSortComplete)
            {
                isSortComplete = true;
                for (var i = 0; i < array.Length - 1; i++)
                {
                    if(array[i] > array[i + 1])
                    {  
                        var a = array[i];
                        var b = array[i + 1];
                        array[i] = b;
                        array[i + 1] = a;
                        isSortComplete = false;
                    }
                    comparisons++;
                }
            }
            return comparisons;
        }

        public static int[] ArithmeticProgression(int start, int step, int length)
        {
            var output = new List<int>() { start };
            for (var i = 0; i < length; i++)
            {
                output.Add(output.Last() + step);
            }
            return output.ToArray();
        }
        public static int[] GeometricProgression(int start, int step, int length)
        {
            var output = new List<int>() { start };
            for (var i = 0; i < length; i++)
            {
                output.Add(output.Last() * step);
            }
            return output.ToArray();
        }
        public static void ArrayShuffle(int[] array)
        {
            var random = new Random();
            for (int i = array.Length - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                // обменять значения data[j] и data[i]
                var temp = array[j];
                array[j] = array[i];
                array[i] = temp;
            }
        }

        public static void AddToXML(XmlDocument document, int length, int operations)
        {
            var element = document.CreateElement("experiment");
            document.DocumentElement.AppendChild(element); // указываем родителя

            var Length = document.CreateAttribute("Length"); // создаём атрибут
            Length.Value = length.ToString(); // устанавливаем значение атрибута
            element.Attributes.Append(Length); // добавляем атрибут

            var count = document.CreateAttribute("Operations"); // создаём атрибут
            count.Value = operations.ToString(); // устанавливаем значение атрибута
            element.Attributes.Append(count); // добавляем атрибут
        }
    }
}
