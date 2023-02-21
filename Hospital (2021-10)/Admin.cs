using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using Hospital__2021_10_;

namespace admin
{
    class Admin
    {
        public string name, sur, pes, pwz_, pwz, spec, type, type_, login, pass;
        int[] date = new int[10];
        public bool ns = false;

        public User user = new User();
        public List<User> usesr = new List<User>();


        public void AddPracownik() // создаем работника
        {
            Console.Write("Imie: "); name = Console.ReadLine();
            Console.Write("Surname: "); sur = Console.ReadLine();
            Console.Write("Pesel: "); pes = Console.ReadLine();
            Console.WriteLine("1.Lekarz \t 2.Pielegniarka?:");
            while (true)
            {
                spec = Console.ReadLine();
                if (spec == "1")
                {
                    Console.Write("PWZ: "); pwz = Console.ReadLine();
                    Console.WriteLine("Specjalność 1.Laryngolo, 2.Kardiolog, 3.Neurolog, 4.Urolog");
                    while (true)
                    {
                        type = Console.ReadLine();
                        switch (type)
                        {
                            case "1": type = "laryngolog"; break;
                            case "2": type = "kardiolog"; break;
                            case "3": type = "neurolog"; break;
                            case "4": type = "urolog"; break;
                            default: Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Wybierz 1 z 4"); Console.ResetColor(); continue;
                        }
                        break;
                    }
                    spec = "lekarz";
                    break;
                }


                else if (spec == "2") { spec = "pielegniarka"; break; }
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Wybierz 1 z 2"); Console.ResetColor();
            }
            Console.Write("Username: "); login = Console.ReadLine();
            Console.Write("Haslo: "); pass = Console.ReadLine();

            if (spec == "pielegniarka") { dateNurse(); usesr.Add(new User(name, sur, pes, spec, login, date, pass)); user.nurse.Add(new Nurse(name, sur, pes, spec, login, pass, date)); }
            else { dateDoc(type, pes); usesr.Add(new User(name, sur, pes, spec, login, date, pass, type, pwz)); user.doctors.Add(new Doctor(name, sur, pes, spec, login, pass, date, type, pwz)); }
            Console.ResetColor();
            Console.Clear();
            Console.Write("Użytkownik: "); Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("Administrator\n"); Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine(" \tIf you have problems you need to restart the console \n"); Console.ResetColor();

        }

        public void dateNurse()// даты для медсестр
        {
            Console.WriteLine("Wpisuj dni dyzurst (min - max 10) zeby skonczyć wpisz");
            for (int i = 0; i < date.Length; i++)
            {
                Console.Write(date.Length - i + ": ");
                try
                {
                    date[i] = int.Parse(Console.ReadLine());
                    for (int u = 0; u < i; u++)
                        while (date[i] == date[u] || date[i] == date[i - 1] + 1 || date[i] + 1 == date[i - 1] || date[i] > 31 || date[i] < 1)
                        {
                            Console.Write("Wpisz inny dzien: ");
                            date[i] = int.Parse(Console.ReadLine());
                        }
                }
                catch
                {
                    Console.WriteLine("Wpisz liczbę"); i = i - 1; continue;
                }
            }
        }


        public void dateDocEdit(string type_, string pesel) // даты для врачей
        {
            date = new int[10];
            while (true)
            {
                Console.WriteLine("Wpisz dni dyzurst (min - max 10)");
                for (int i = 0; i < date.Length; i++)
                {
                    Console.Write(date.Length - i + ": ");
                    try
                    {
                        date[i] = int.Parse(Console.ReadLine());
                        for (int j = 0; j < user.doctors.Count; j++) if (user.doctors[j].type != type_) { ns = false; } else { ns = true; break; }
                        if (ns == false)
                        {
                            for (int u = 0; u < i; u++) while (date[i] == date[u] || date[i] == date[i - 1] + 1 || date[i] + 1 == date[i - 1] || date[i] > 31 || date[i] < 1)
                                {
                                    Console.Write("Wpisz inny dzien: ");
                                    date[i] = int.Parse(Console.ReadLine());
                                }
                        }
                        else
                            for (int u = 0; u < i; u++)
                            {

                                for (int doc = 0; doc < usesr.Count; doc++)
                                    if (usesr[doc].spec == "lekarz")
                                    {

                                        if (usesr[doc].pes == pesel)
                                        {

                                            if (usesr[doc].type == type_)
                                            {


                                                for (int docl = 0; docl < usesr[doc].date.Length; docl++)
                                                    while (date[i] == date[u] || date[i] == date[i - 1] + 1 || date[i] + 1 == date[i - 1] || date[i] == usesr[doc].date[docl] || date[i] > 31 || date[i] < 1)
                                                    {

                                                        Console.Write("Wpisz inny dzien: ");
                                                        date[i] = int.Parse(Console.ReadLine());
                                                        if (date[i] != date[u] || date[i] != date[i - 1] + 1 || date[i] + 1 != date[i - 1] || date[i] != usesr[doc].date[docl] || date[i] < 31 || date[i] > 1) break;
                                                    }
                                            }
                                        }
                                        else continue;
                                    }
                            }
                    }
                    catch { Console.WriteLine("Wpisz liczbę"); i = i - 1; continue; }
                }
                break;
            }
            Console.Clear();
            Console.Write("Użytkownik: "); Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("Administrator\n"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine(" \tIf you have problems you need to restart the console \n"); Console.ResetColor();

        }

        public void dateDoc(string type_, string pesel) // даты для врачей
        {
            date = new int[10];
            int wolneDni = 31;
            for(int i = 0; i < usesr.Count; i++)
            {
                if (usesr[i].spec == "lekarz")
                {
                    if (usesr[i].type == type_)
                    {
                        for (int u = 0; u < usesr[i].date.Length; u++)
                        {
                            for (int j = 0; j <= 31; j++) if (usesr[i].date[u] == j) wolneDni = wolneDni - 1;
                                else continue;
                        }
                    }
                }
                    
            }
            while (true)
            {
                Console.WriteLine("Wpisz dni dyzurst (min 0 - max 10), Masz {0} dni wolne", wolneDni);
                for (int i = 0; i < date.Length; i++)
                {
                    if (i > wolneDni){ date[i] = 0; continue;}
                    Console.Write(date.Length - i + ": ");
                    try
                    {
                        date[i] = int.Parse(Console.ReadLine());
                        for (int j = 0; j < user.doctors.Count; j++) if (user.doctors[j].type != type_) { ns = false; } else { ns = true; break; }
                        if (ns == false)
                        {
                            for (int u = 0; u < i; u++) while (date[i] == date[u] || date[i] == date[i - 1] + 1 || date[i] + 1 == date[i - 1] || date[i] > 31 || date[i] < 1)
                                {
                                    Console.Write("Wpisz inny dzien: ");
                                    date[i] = int.Parse(Console.ReadLine());
                                }
                        }
                        else
                            for (int u = 0; u < i; u++)
                            {

                                for (int doc = 0; doc < usesr.Count; doc++)
                                    if (usesr[doc].spec == "lekarz")
                                    {
                                            if (usesr[doc].type == type_)
                                            {
                                                for (int docl = 0; docl < usesr[doc].date.Length; docl++)
                                                    while (date[i] == date[u] || date[i] == date[i - 1] + 1 || date[i] + 1 == date[i - 1] || date[i] == usesr[doc].date[docl] || date[i] > 31 || date[i] < 1)
                                                    {

                                                        Console.Write("Wpisz inny dzien: ");
                                                        date[i] = int.Parse(Console.ReadLine());
                                                        if (date[i] != date[u] || date[i] != date[i - 1] + 1 || date[i] + 1 != date[i - 1] || date[i] != usesr[doc].date[docl] || date[i] < 31 || date[i] > 1) break;
                                                    }
                                            }
                                        
                                        else continue;
                                    }
                            }
                    }
                    catch { Console.WriteLine("Wpisz liczbę"); i = i - 1; continue; }
                }
                break;
            }
            Console.Clear();
            Console.Write("Użytkownik: "); Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("Administrator\n"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine(" \tIf you have problems you need to restart the console \n"); Console.ResetColor();

        }
        public void EditPracownik(string pesel, string choice) // меняем инфу работника
        {
            bool znajd = false;
            for (int i = 0; i < usesr.Count; i++)
            {

                if (usesr[i].pes == pesel && usesr[i].spec == "lekarz")
                {
                    Console.WriteLine("Pesel: " + usesr[i].pes);
                    znajd = true;
                    Console.WriteLine("Specjalizacjia: " + usesr[i].spec + " Typ: " + usesr[i].type);
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Wpisz nowe imie: "); usesr[i].name = Console.ReadLine();
                            break;
                        case "2":
                            Console.Write("Wpisz nowe nazwisko: "); usesr[i].sur = Console.ReadLine();
                            break;
                        case "3":
                            Console.Write("Wpisz nowy pesel: "); usesr[i].pes = Console.ReadLine();
                            break;
                        case "4":
                            Console.Write("Wpisz nowy PWZ: "); usesr[i].pwz = Console.ReadLine();
                            break;
                        case "5":
                            Console.Write("Wpisz nowy login: "); usesr[i].log = Console.ReadLine();
                            break;
                        case "6":
                            Console.Write("Wpisz nowe haslo: "); usesr[i].pass = Console.ReadLine();
                            break;
                        case "7":
                            Console.WriteLine("Zmien daty dyzurow: "); dateDocEdit(usesr[i].type, usesr[i].pes);
                            break;
                    }

                }
                else if (usesr[i].pes == pesel && usesr[i].spec == "pielegniarka")
                {
                    znajd = true;
                    Console.WriteLine("Specjalizacjia: " + user.nurse[i].spec);
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Wpisz nowe imie: "); name = Console.ReadLine();
                            break;
                        case "2":
                            Console.Write("Wpisz nowe nazwisko: "); sur = Console.ReadLine();
                            break;
                        case "3":
                            Console.Write("Wpisz nowy pesel: "); pes = Console.ReadLine();
                            break;
                        case "4":
                            Console.Write("Wpisz nowy login: "); login = Console.ReadLine();
                            break;
                        case "5":
                            Console.Write("Wpisz nowe haslo: "); pass = Console.ReadLine();
                            break;
                        case "6":
                            Console.WriteLine("Zmien daty dyzurow: "); dateNurse();
                            break;
                    }
                }
            }
            Console.Clear();
            Console.Write("Użytkownik: "); Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("Administrator\n"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine(" \tIf you have problems you need to restart the console \n"); Console.ResetColor();

            if (znajd == false) { Console.Clear(); Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Nie istnieje pracownika z peselem ({0})", pesel); Console.ResetColor(); }

        }

        public void allEmp()
        {
            if (user.doctors.Count != 0)
            {
                Console.WriteLine("Lekarzy");
                for (int i = 0; i < user.doctors.Count; i++) { Console.WriteLine("Imie: " + user.doctors[i].name + " Surname: " + user.doctors[i].sur + " Specjalizacja: " + user.doctors[i].type); }
            }
            if (user.nurse.Count != 0)
            {
                Console.WriteLine("Pielegniarka");
                for (int i = 0; i < user.nurse.Count; i++) Console.WriteLine("Imie: " + user.nurse[i].name + " Surname: " + user.nurse[i].sur);
            }
        }
    }
}
