using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckRepetitionSetConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Проверка введённых чисел с коллекцией HashSet\n");
            string strInput = "";
            HashSet<int> mySet = new HashSet<int>();
            while(true)
            {
                Console.WriteLine("Для добавления числа в базу введите < 1 >");
                Console.WriteLine("Для просмотра всех чисел введите < 2 >");
                Console.WriteLine("Для проверки числа введите < 3 >");
                Console.WriteLine("Для добавления 10 случайных чисел в базу (от 1 до 100) введите < 4 >");
                Console.WriteLine("Для завершения работы введите < 0 >");
                strInput = Console.ReadLine();
                switch(strInput)
                {
                    case "1":
                        AddNumber(mySet);
                        break;
                    case "2":
                        PrintSet(mySet);
                        break;
                    case "3":
                        TestNumber(mySet);
                        break;
                    case "4":
                        Generate10Numbers(mySet);
                        break;
                    default:
                        break;
                }
                if (strInput == "0") break;
            }
        }

        /// <summary>
        /// Генерация и добавление в множество 10 разных случайных чисел
        /// </summary>
        /// <param name="mySet">множество чисел</param>
        private static void Generate10Numbers(HashSet<int> mySet)
        {
            int i = 0, zn;
            Random rnd = new Random();
            while (i < 10) 
            {
                zn = rnd.Next(1, 101);
                if (mySet.Contains(zn)) continue;
                else
                {
                    mySet.Add(zn);
                    i++;
                }
            }
        }

        /// <summary>
        /// Добавление числа в множество, введённого пользователем
        /// </summary>
        /// <param name="mySet">множество чисел</param>
        private static void AddNumber(HashSet<int> mySet)
        {
            string strInput;
            while(true)
            {
                Console.WriteLine("Введите число : ");
                strInput = Console.ReadLine();
                if (int.TryParse(strInput, out int zn))
                {
                    mySet.Add(zn);
                    break;
                }
                else
                {
                    Console.WriteLine($"Ошибка ввода числа : < {strInput} >");
                }
            }
        }

        /// <summary>
        /// Вывод на консоль всех чисел множества
        /// </summary>
        /// <param name="mySet">множество чисел</param>
        private static void PrintSet(HashSet<int> mySet)
        {
            int i = 0;
            foreach (int zn in mySet)
            {
                if ((i + 1) % 10 == 0)
                {
                    Console.Write($"{zn, 4}\n");
                }
                else
                {
                    Console.Write($"{zn, 4}");
                }
                i++;
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Проверка наличия в множестве номера, введённого пользователем
        /// </summary>
        /// <param name="mySet">множество чисел</param>
        private static void TestNumber(HashSet<int> mySet)
        {
            string strInput;
            while (true)
            {
                Console.WriteLine("Введите число  ( для выхода - ENTER ) : ");
                strInput = Console.ReadLine();
                if (strInput == "") break;
                if (int.TryParse(strInput, out int zn))
                {
                    if (mySet.Contains(zn))
                    {
                        Console.WriteLine($"Число {zn} есть в базе");
                    }
                    else
                    {
                        Console.WriteLine($"Числа {zn} нет в базе");
                    }
                }
                else
                {
                    Console.WriteLine($"Ошибка ввода числа : < {strInput} >");
                }
            }
        }
    }
}
