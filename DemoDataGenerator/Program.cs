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
                Console.WriteLine($"{user.UserName};{user.Betrag.ToString("C2")};{user.IsDeveloper}");
            }

            Console.ReadKey();
        }

        private static void MenuPoint2()
        {
            Console.Clear();

            DataTable users = BuildDemoData<UserDemoDaten>.CreateForDataTable<UserDemoDaten>(ConfigObject, 100);
            foreach (DataRow user in users.Rows)
            {
                string betrag = string.Format(new CultureInfo("de-de", false), "{0:c2}", user["Betrag"]);
                Console.WriteLine($"{user["UserName"]};{betrag};{user["IsDeveloper"]}");
            }

            Console.ReadKey();
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
