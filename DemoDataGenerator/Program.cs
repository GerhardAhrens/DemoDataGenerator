//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Lifeprojects.de">
//     Class: Program
//     Copyright © Lifeprojects.de 2025
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>04.01.2025 18:31:16</date>
//
// <summary>
// Konsolen Applikation mit Menü
// </summary>
//-----------------------------------------------------------------------

using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;

using DemoDataGeneratorLib.Base;

namespace DemoDataGenerator
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            do
            {
                Console.Clear();
                Console.WriteLine("1. Demodaten als List<T>");
                Console.WriteLine("2. Demodaten als DataTable");
                Console.WriteLine("3. Demodaten als Dictionary<Guid,string>");
                Console.WriteLine("4. Demodaten als Dictionary<int,string>");
                Console.WriteLine("X. Beenden");

                Console.WriteLine("Wählen Sie einen Menüpunkt oder 'x' für beenden");
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.X)
                {
                    Environment.Exit(0);
                }
                else
                {
                    if (key == ConsoleKey.D1)
                    {
                        MenuPoint1();
                    }
                    else if (key == ConsoleKey.D2)
                    {
                        MenuPoint2();
                    }
                    else if (key == ConsoleKey.D3)
                    {
                        MenuPoint3();
                    }
                    else if (key == ConsoleKey.D4)
                    {
                        MenuPoint4();
                    }
                }
            }
            while (true);
        }

        private static void MenuPoint1()
        {
            Console.Clear();

            IEnumerable<UserDemoDaten> users = BuildDemoData<UserDemoDaten>.CreateForList<UserDemoDaten>(ConfigObject, 100);
            foreach (UserDemoDaten user in users)
            {
                Console.WriteLine($"{user.UserName};{user.Betrag.ToString("C2")};{user.IsDeveloper};{user.City}");
            }

            Console.WriteLine();
            Console.WriteLine("** EinTaste für zurück!");
            Console.ReadKey();
        }

        private static void MenuPoint2()
        {
            Console.Clear();

            DataTable users = BuildDemoData<UserDemoDaten>.CreateForDataTable<UserDemoDaten>(ConfigObject, 100);
            foreach (DataRow user in users.Rows)
            {
                string betrag = string.Format(new CultureInfo("de-de", false), "{0:c2}", user["Betrag"]);
                Console.WriteLine($"{user["UserName"]};{betrag};{user["IsDeveloper"]};{user["City"]}");
            }

            Console.WriteLine();
            Console.WriteLine("** EinTaste für zurück!");
            Console.ReadKey();
        }

        private static void MenuPoint3()
        {
            Console.Clear();

            Dictionary<object, object> users = BuildDemoData<KeyValuePair<Guid, string>>.CreateForDictionary<KeyValuePair<Guid, string>>(ConfigObjectString, 100);
            foreach (KeyValuePair<object,object> user in users)
            {
                Console.WriteLine($"{user.Key};{user.Value}");
            }

            Console.WriteLine();
            Console.WriteLine("** EinTaste für zurück!");
            Console.ReadKey();
        }

        private static void MenuPoint4()
        {
            Console.Clear();

            Dictionary<object, object> users = BuildDemoData<KeyValuePair<int, string>>.CreateForDictionary<KeyValuePair<int, string>>(ConfigObjectInt, 100);
            foreach (KeyValuePair<object, object> user in users)
            {
                Console.WriteLine($"{user.Key};{user.Value}");
            }

            Console.ReadKey(); Console.WriteLine();
            Console.WriteLine("** EinTaste für zurück!");

        }

        private static KeyValuePair<int, string> ConfigObjectInt(KeyValuePair<int, string> keyValue, object counter)
        {
            keyValue = new KeyValuePair<int, string>(Convert.ToInt32(counter), BuildDemoData.Username());
            return keyValue;
        }

        private static KeyValuePair<Guid, string> ConfigObjectString(KeyValuePair<Guid, string> keyValue, object counter)
        {
            keyValue = new KeyValuePair<Guid, string>(Guid.NewGuid(), BuildDemoData.Username());
            return keyValue;
        }

        private static UserDemoDaten ConfigObject(UserDemoDaten demoDaten)
        {
            var timeStamp = BuildDemoData.SetTimeStamp();
            demoDaten.UserName = BuildDemoData.Username();
            demoDaten.Betrag = BuildDemoData.Currency(1_000, 10_000);
            demoDaten.IsDeveloper = BuildDemoData.Boolean();
            demoDaten.City = BuildDemoData.City();
            demoDaten.CreateOn = timeStamp.CreateOn;
            demoDaten.CreateBy = timeStamp.CreateBy;
            demoDaten.ModifiedOn = timeStamp.ModifiedOn;
            demoDaten.ModifiedBy = timeStamp.ModifiedBy;

            return demoDaten;
        }
    }

    [DebuggerDisplay("UserName={this.UserName}")]
    public class UserDemoDaten
    {
        public string UserName { get; set; }
        public bool IsDeveloper { get; set; }
        public decimal Betrag { get; set; }
        public string City { get; set; }
        public DateTime CreateOn { get; set; }
        public string CreateBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
