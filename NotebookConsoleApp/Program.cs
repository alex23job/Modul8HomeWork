using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NotebookConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Записная книжка и хранение данных в формате XML\n");
            string strInput = "";
            string path = "person.xml";
            List<Person> myList = new List<Person>();
            if (File.Exists(path))
            {
                myList = DeserializePersonList(path);
                Console.WriteLine($"Из файла '{path}' загружено записей : {myList.Count} шт.\n");
            }

            while (true)
            {
                Console.WriteLine("Для заполнения персональных данных и добавления новой записи введите < 1 >");
                Console.WriteLine("Для просмотра всех записей введите < 2 >");
                Console.WriteLine("Для сохранения в XML файл записи по ID введите < 3 >");
                Console.WriteLine("Для сохранения всех записей в XML файл введите < 4 >");
                Console.WriteLine("Для завершения работы введите < 0 >");
                strInput = Console.ReadLine();
                switch (strInput)
                {
                    case "1":
                        AddNote(myList);
                        break;
                    case "2":
                        PrintList(myList);
                        break;
                    case "3":
                        SaveNoteByID(myList);
                        break;
                    case "4":
                        SaveAllNotes(myList);
                        break;
                    default:
                        break;
                }
                if (strInput == "0")
                {
                    SerializePersonList(myList, path);
                    break;
                }
            }
        }

        /// <summary>
        /// Добавление новой записи на основе вводимых данных
        /// </summary>
        /// <param name="myList">список записей</param>
        private static void AddNote(List<Person> myList)
        {
            Person ps = new Person();
            ps.ID = (myList.Count + 1).ToString();
            Console.WriteLine("Введите Ф.И.О. (пример - Иванов Иван Иванович) : ");
            ps.FIO = Console.ReadLine();
            Console.WriteLine("Введите наименование улицы (пример - Красная) : ");
            ps.Street = Console.ReadLine();
            Console.WriteLine("Введите номер дома (пример - 10) : ");
            ps.HouseNumber = Console.ReadLine();
            Console.WriteLine("Введите номер квартиры (пример - 7) : ");
            ps.ApartmentNumber = Console.ReadLine();
            Console.WriteLine("Введите номер мобильного телефона : ");
            ps.MobilePhone = Console.ReadLine();
            Console.WriteLine("Введите номер домашнего телефона : ");
            ps.FlatPhone = Console.ReadLine();
            Console.WriteLine($"\nДобавлена запись :\n{ps.ToString()}\n");
            myList.Add(ps);
        }

        /// <summary>
        /// Вывод списка записей на экран (консоль)
        /// </summary>
        /// <param name="myList">список записей</param>
        private static void PrintList(List<Person> myList)
        {
            if (myList.Count == 0)
            {
                Console.WriteLine("\nЗаписей нет.\n");
            }
            else
            {
                string pattern = "| {0, -2} | {1, -25} | {2, -15} | {3, -4} | {4, -4} | {5, 12} | {6, 12} |";
                Console.WriteLine(pattern, "ID", "Ф.И.О.", "улица", "дом", "кв.", "моб. тлф", "дом. тлф");
                for (int i = 0; i < myList.Count; i++)
                {
                    Console.WriteLine(myList[i].OutFormatString(pattern));
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Сохранение одной записи по её ID в отдельный файл
        /// </summary>
        /// <param name="myList">список записей</param>
        private static void SaveNoteByID(List<Person> myList)
        {
            string strInput = "";
            Console.WriteLine("Введите идентификатор записи : ");
            strInput = Console.ReadLine();
            Person ps = null;
            for (int i = 0; i < myList.Count; i++)
            {
                if (myList[i].ID == strInput)
                {
                    ps = myList[i];
                    break;
                }
            }
            if (ps == null)
            {
                Console.WriteLine($"Записи с дентификатором < {strInput} > нет в книге");
            }
            else
            {
                Console.WriteLine("Введите имя файла для сохранения или ENTER (имя файла сгенерим автоматически)");
                strInput = Console.ReadLine();
                ps.SaveToXml(strInput);
            }
        }

        /// <summary>
        /// Сохранение всех записей
        /// </summary>
        /// <param name="myList">список записей</param>
        private static void SaveAllNotes(List<Person> myList)
        {
            string strInput = "";
            Console.WriteLine("Введите имя файла для сохранения или ENTER (имя файла сгенерим автоматически)");
            strInput = Console.ReadLine();
            if (strInput == "") strInput = "person.xml";
            Console.WriteLine($"Все записи будут сохранены в файл \"{strInput}\"\n");
            SerializePersonList(myList, strInput);
        }

        /// <summary>
        /// Метод сериализации List<Person>
        /// </summary>
        /// <param name="myList">Коллекция для сериализации</param>
        /// <param name="Path">Путь к файлу</param>
        private static void SerializePersonList(List<Person> myList, string Path)
        {
            // Создаем сериализатор на основе указанного типа 
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Person>));

            // Создаем поток для сохранения данных
            Stream fStream = new FileStream(Path, FileMode.Create, FileAccess.Write);

            // Запускаем процесс сериализации
            xmlSerializer.Serialize(fStream, myList);

            // Закрываем поток
            fStream.Close();
        }

        /// <summary>
        /// Метод десериализации PersonList
        /// </summary>
        /// <param name="Path">Путь к файлу</param>
        private static List<Person> DeserializePersonList(string Path)
        {
            List<Person> tempPersonCol = new List<Person>();
            // Создаем сериализатор на основе указанного типа 
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Person>));

            // Создаем поток для чтения данных
            Stream fStream = new FileStream(Path, FileMode.Open, FileAccess.Read);

            // Запускаем процесс десериализации
            tempPersonCol = xmlSerializer.Deserialize(fStream) as List<Person>;

            // Закрываем поток
            fStream.Close();

            // Возвращаем результат
            return tempPersonCol;
        }
    }
}
