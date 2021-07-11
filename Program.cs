using System;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Collections.Generic;
using ConsoleTables;
namespace Projectt1
{
    class Program
    {
        static void Main(string[] args)
        {
           
            int x = 5;
            do
            {
                Console.WriteLine("\n-----Data Project 1: Company Master------");
                Console.WriteLine("Enter 1-For TestCase1 ");
                Console.WriteLine("Enter 2-For TestCase2 ");
                Console.WriteLine("Enter 3-For TestCase3 ");
                Console.WriteLine("Enter 4-For TestCase4 ");
                Console.WriteLine("Enter 5-For Exit ");
                Console.Write("Enter your choice:");
                x = Convert.ToInt32(Console.ReadLine());
                switch (x)
                {
                    case 1:
                        Testcase1();
                        break;
                    case 2:
                        Testcase2();
                        break;
                    case 3:
                        Testcase3();
                        break;
                    case 4:
                        Testcase4();
                        break;
                    case 5:
                        x = 5;
                        Console.WriteLine("Thank You !!");
                        break;
                    default:
                        Console.WriteLine("please enter valid choice");
                        break;
                }


            }
            while (x!=5);

        }


        static void Testcase1()
        {
            var reader = new StreamReader(@"C:\Users\yogen\source\repos\Projectt1\WestBengal.csv");
            var rows = new CsvReader(reader, CultureInfo.InvariantCulture);
            Dictionary<string, int> AuthorizedCap = new Dictionary<string, int>
            {
                ["<=1L"] = 0,
                ["1L to 10L"] = 0,
                ["10L to 1Cr"] = 0,
                ["1Cr to 10Cr"] = 0,
                [">10Cr"] = 0
            };
            foreach (var row in rows.GetRecords<WestBengalProperties>())
            {
                double x = Convert.ToDouble(row.AUTHORIZED_CAP);
                if (x <= 100000)
                    AuthorizedCap["<=1L"]++;

                else if (x > 100000 && x <= 1000000)
                    AuthorizedCap["1L to 10L"]++;

                else if (x > 1000000 && x <= 10000000)
                    AuthorizedCap["10L to 1Cr"]++;

                else if (x > 10000000 && x <= 100000000)
                    AuthorizedCap["1Cr to 10Cr"]++;

                else
                    AuthorizedCap[">10Cr"]++;
            }
            Console.WriteLine("\nTestcase1 output");
            var table = new ConsoleTable("Bin", "Counts");
            table.AddRow("<=1L", AuthorizedCap["<=1L"])
                .AddRow("1L to 10L", AuthorizedCap["1L to 10L"])
                .AddRow("10L to 1Cr", AuthorizedCap["10L to 1Cr"])
                .AddRow("1Cr to 10Cr", AuthorizedCap["1Cr to 10Cr"])
                .AddRow(">10Cr", AuthorizedCap[">10Cr"]);
            table.Write();
        }


        static void Testcase2()
        {
            var reader = new StreamReader(@"C:\Users\yogen\source\repos\Projectt1\WestBengal.csv");
            var rows = new CsvReader(reader, CultureInfo.InvariantCulture);
            Dictionary<string, int> DATEOFREGISTRATION = new Dictionary<string, int>();
            for (int i = 2000; i <= 2019; i++)
            {
                DATEOFREGISTRATION[i.ToString()] = 0;
            }
            //foreach (KeyValuePair<string, int> ele2 in DATEOFREGISTRATION)
            //{ Console.WriteLine("Key={0} value={1}", ele2.Key, ele2.Value); }

            foreach (var row in rows.GetRecords<WestBengalProperties>())
            {
                if (row.DATE_OF_REGISTRATION != "NA")
                {
                    DateTime dt = Convert.ToDateTime(row.DATE_OF_REGISTRATION);
                    int year = dt.Year;  //fetch the year

                    if (year >= 2000 && year <= 2019)
                    { DATEOFREGISTRATION[year.ToString()]++; }
                }
            }


            //foreach(KeyValuePair<string,int> ele2 in DATEOFREGISTRATION)
            //{ Console.WriteLine("{0} and {1}",ele2.Key,ele2.Value); }

            Console.WriteLine("\nTestcase2 output");
            var table = new ConsoleTable("Year", "No Of Registrations");
            for (int i = 2000; i <= 2019; i++)
            {
                table.AddRow(i, DATEOFREGISTRATION[i.ToString()]);
            }
            table.Write();
        }

        static void Testcase3()
        {
            var reader = new StreamReader(@"C:\Users\yogen\source\repos\Projectt1\WestBengal.csv");
            var rows = new CsvReader(reader, CultureInfo.InvariantCulture);

            Dictionary<string, int> PrincipalActivity = new Dictionary<string, int>();
            foreach (var row in rows.GetRecords<WestBengalProperties>())
            {
                if (row.DATE_OF_REGISTRATION != "NA")
                {
                    DateTime dt = Convert.ToDateTime(row.DATE_OF_REGISTRATION);
                    int year = dt.Year;  //fetch the year

                    if (year == 2015)
                    {
                        string x = row.PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN;
                        if (PrincipalActivity.ContainsKey(x))
                            PrincipalActivity[x]++;
                        else
                            PrincipalActivity[x] = 1;
                    }
                }
            }
            Console.WriteLine("\nTestcase3 output");
            var table = new ConsoleTable("PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN", "No Of Registrations");
            foreach (KeyValuePair<string, int> ele2 in PrincipalActivity)
            {
                table.AddRow(ele2.Key, ele2.Value);
            }

            table.Write();
            Console.WriteLine();
        }


        static void Testcase4()
        {
            var reader = new StreamReader(@"C:\Users\yogen\source\repos\Projectt1\WestBengal.csv");
            var rows = new CsvReader(reader, CultureInfo.InvariantCulture);
            Dictionary<string, Dictionary<string, int>> PrincipalActivity = new Dictionary<string, Dictionary<string, int>>();
            foreach (var row in rows.GetRecords<WestBengalProperties>())
            {
                if (row.DATE_OF_REGISTRATION != "NA")
                {
                    DateTime dt = Convert.ToDateTime(row.DATE_OF_REGISTRATION);
                    int year = dt.Year;  //fetch the year
                    if (year >= 2000 && year <= 2019)
                    {
                        if (PrincipalActivity.ContainsKey(year.ToString()) && PrincipalActivity[year.ToString()].ContainsKey(row.PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN))
                        {
                            PrincipalActivity[year.ToString()][row.PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN]++;
                        }
                        else if (PrincipalActivity.ContainsKey(year.ToString()))
                        {
                            PrincipalActivity[year.ToString()].Add(row.PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN, 1);
                        }
                        else
                        {
                            PrincipalActivity.Add(year.ToString(), new Dictionary<string, int>());
                            PrincipalActivity[year.ToString()].Add(row.PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN, 1);
                        }




                    }
                }
            }
            Console.WriteLine("\nTestcase4 output");
            var table = new ConsoleTable("PRINCIPAL_BUSINESS_ACTIVITY", "Counts ");
            foreach (var keyss1 in PrincipalActivity.Keys)
            {
                table.AddRow(keyss1, "");
                foreach (var keyss2 in PrincipalActivity[keyss1].Keys)
                {
                    table.AddRow(keyss2, PrincipalActivity[keyss1][keyss2]);
                }
            }
            table.Write();
        }
    }        
}
          