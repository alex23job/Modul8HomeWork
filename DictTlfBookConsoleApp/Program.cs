using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DictTlfBookConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Телефонная книга с коллекцией Dict\n");
            string strInput = "";
            Dictionary<string, string> myDict = new Dictionary<string, string>();
            if (File.Exists("TlfBook.json"))
            {
                string json = File.ReadAllText("TlfBook.json");
                myDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }

            while (true)
            {
                Console.WriteLine("Для добавления номера в книгу введите < 1 >");
                Console.WriteLine("Для просмотра книги введите < 2 >");
                Console.WriteLine("Для проверки номера введите < 3 >");
                Console.WriteLine("Для завершения работы введите < 0 >");
                strInput = Console.ReadLine();
                switch (strInput)
                {
                    case "1":
                        AddNumber(myDict);
                        break;
                    case "2":
                        PrintDict(myDict);
                        break;
                    case "3":
                        CheckNumber(myDict);
                        break;
                    case "0":
                        SaveBook(myDict);
                        break;
                    default:
                        Console.WriteLine("Введённая команда < " + strInput + " > не поддерживается");
                        break;
                }
                if (strInput == "0") break;
            }
        }

        /// <summary>
        /// Сохранение всех записей словаря в файл в формате Json
        /// </summary>
        /// <param name="dict">словарь записей</param>
        private static void SaveBook(Dictionary<string, string> dict)
        {
            string json = JsonConvert.SerializeObject(dict);
            File.WriteAllText("TlfBook.json", json);
        }

        /// <summary>
        /// Проверка наличия номера в словаре с выводом данных при их наличии
        /// </summary>
        /// <param name="dict">словарь записей</param>
        private static void CheckNumber(Dictionary<string, string> dict)
        {
            string strInput = "", val;
            while (true)
            {
                Console.WriteLine("Введите номер телефона для проверки (для выхода - ENTER)");
                strInput = Console.ReadLine();
                if (strInput == "") break;
                if (dict.TryGetValue(strInput, out val))
                {
                    Console.WriteLine($"Владелец номера {strInput} : {val}");
                }
                else
                {
                    Console.WriteLine($"Номера {strInput} нет в книге ...");
                }
            }
        }

        /// <summary>
        /// Вывод всех записей словаря на экран
        /// </summary>
        /// <param name="dict">словарь записей</param>
        private static void PrintDict(Dictionary<string, string> dict)
        {
            string s1 = "Телефон", s2 = "Владелец";
            Console.WriteLine($"{s1,-12} : {s2,-20}");
            foreach (KeyValuePair<string, string> e in dict)
            {
                Console.WriteLine($"{e.Key, -12} : {e.Value, -20}");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Ввод номера и данных владельца с последующим добавлением записи в словарь
        /// </summary>
        /// <param name="dict">словарь записей</param>
        private static void AddNumber(Dictionary<string, string> dict)
        {
            string s_num, s_fio, val;
            Console.WriteLine("Введите номер телефона : ");
            s_num = Console.ReadLine();
            Console.WriteLine("Введите Ф.И.О. владельца : ");
            s_fio = Console.ReadLine();
            if (dict.TryGetValue(s_num, out val))
            {
                while (true)
                {
                    Console.WriteLine($"В списке уже есть номер {s_num} для владельца {s_fio}");
                    Console.WriteLine("Для замены данных владельца введите < 1 >");
                    Console.WriteLine("Для объединения данных о владельцах введите < 2 >");
                    Console.WriteLine("Для отмены изменений введите < 0 >");
                    string strInput = Console.ReadLine();
                    switch(strInput)
                    {
                        case "1":
                            dict[s_num] = s_fio;
                            break;
                        case "2":
                            dict[s_num] = val + ", " + s_fio;
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Неверная команда : " + strInput);
                            break;
                    }
                }
            }
            else
            {
                dict.Add(s_num, s_fio);
            }
        }

    }
}
