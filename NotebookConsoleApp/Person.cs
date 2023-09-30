using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NotebookConsoleApp
{
    public class Person
    {
        /// <summary>
        /// Идентификатор контакта
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Фамилия Имя Отчество
        /// </summary>
        public string FIO { get; set; }

        /// <summary>
        /// наименование улицы
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// номер дома
        /// </summary>
        public string HouseNumber { get; set; }

        /// <summary>
        /// номер квартиры
        /// </summary>
        public string ApartmentNumber { get; set; }

        /// <summary>
        /// Номер мобильного телефона
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// Номер домашнего телефона
        /// </summary>
        public string FlatPhone { get; set; }

        public Person() { }

        /// <summary>
        /// Вывод полей экземпляра класса в файл в формате XML
        /// </summary>
        /// <param name="path">путь и имя файла</param>
        public void SaveToXml(string path="")
        {
            XElement elPerson = new XElement("Person");
            XAttribute aName = new XAttribute("name", FIO);
            elPerson.Add(aName);
            XElement elAddress = new XElement("Address");
            XElement elStreet = new XElement("Street", Street);
            XElement elHouseNumber = new XElement("HouseNumber", HouseNumber);
            XElement elFlatNumber = new XElement("FlatNumber", ApartmentNumber);
            XElement elPhones = new XElement("Phones");
            XElement elMobPhone = new XElement("MobilePhone", MobilePhone);
            XElement elFlatPhone = new XElement("FlatPhone", FlatPhone);

            elPhones.Add(elMobPhone, elFlatPhone);
            elAddress.Add(elStreet, elHouseNumber, elFlatNumber);
            elPerson.Add(elAddress, elPhones);

            if (path == "")
            {   //  генерация имени файла на основе поля FIO
                string[] fio = FIO.Split(' ');
                StringBuilder sb = new StringBuilder(fio[0]); 
                if (fio.Length > 1)
                {
                    for (int i = 1; i < fio.Length; i++)
                    {
                        sb.Append("_" + fio[i]);
                    }
                }
                string name = sb.ToString() + ".xml";
                elPerson.Save(name);
            }
            else
            {
                elPerson.Save(path);
            }
        }

        /// <summary>
        /// Формирование строки в задаваемом формате
        /// </summary>
        /// <param name="pattern">формат строки</param>
        /// <returns></returns>
        public string OutFormatString(string pattern)
        {
            return string.Format(pattern, ID, FIO, Street, HouseNumber, ApartmentNumber, MobilePhone, FlatPhone);
        }

        /// <summary>
        /// Вывод полей экземпляра класса в строку
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Запись № {0} Ф.И.О.: {1} Адрес: ул. {2} д. {3} кв. {4}  тлф:{5}, {6}", ID, FIO, Street, HouseNumber, ApartmentNumber, MobilePhone, FlatPhone);
        }
    }
}
