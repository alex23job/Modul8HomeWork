using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Работа с коллекцией List типов int\n");
            string strInput = "";
            List<int> myList = new List<int>();
            while(true)
            {
                Console.WriteLine("Для генерации списка из 100 случайных чисел int введите < 1 >");
                Console.WriteLine("Для просмотра списка введите < 2 >");
                Console.WriteLine("Для чисел int в указанном диапазоне введите < 3 >");
                Console.WriteLine("Для завершения работы введите < 0 >");
                strInput = Console.ReadLine();
                if (strInput == "0") break;
                switch(strInput)
                {
                    case "1":
                        Generic100int(myList);
                        break;
                    case "2":
                        PrintList(myList);
                        break;
                    case "3":
                        RemoveRange(myList);
                        break;
                    default:
                        Console.WriteLine("Введённая команда < " + strInput + " > не поддерживается");
                        break;
                }
            }
        }

        private static void RemoveRange(List<int> list)
        {
            string strInput = "";
            int n_min = 0, n_max = 200, cnt = 0, i;
            while (true)
            {
                Console.WriteLine("Введите нижнюю границу диапазона в интервале ( 1 - 100 )");
                strInput = Console.ReadLine();
                if (int.TryParse(strInput, out n_min))
                {
                    if (n_min >= 0 && n_min <= 100) break;
                    else
                    {
                        Console.WriteLine("Ошибка ввода нижней границы диапазона : " + strInput);
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка ввода : ", strInput);
                }
            }
            while (true)
            {
                Console.WriteLine("Введите вернюю границу диапазона в интервале ( 1 - 100 )");
                strInput = Console.ReadLine();
                if (int.TryParse(strInput, out n_max))
                {
                    if (n_max >= 0 && n_max <= 100 && n_min < n_max) break;
                    else
                    {
                        Console.WriteLine("Ошибка ввода верней границы диапазона : " + strInput);
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка ввода : ", strInput);
                }
            }
            for (i = list.Count; i > 0; i--)
            {
                if (list[i - 1] > n_min && list[i - 1] < n_max)
                {
                    list.RemoveAt(i - 1);
                    cnt++;
                }
            }
            Console.WriteLine($"В диапазоне от {n_min} до {n_max} было удалено чисел : {cnt} шт.\n");
        }

        private static void PrintList(List<int> list)
        {
            int i;
            for (i = 0; i < list.Count; i++)
            {
                if ((i + 1) % 10 == 0)
                {
                    Console.Write($"{list[i], 4}\n");
                }
                else
                {
                    Console.Write($"{list[i], 4}");
                }
            }
            Console.WriteLine();
        }

        private static void Generic100int(List<int> list)
        {
            int i;
            Random rnd = new Random();
            for (i = 0; i < 100; i++)
            {
                list.Add(rnd.Next(1, 101));
            }
        }
    }
}
