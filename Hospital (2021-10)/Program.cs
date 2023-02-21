using admin;
using System.Numerics;
using System.Xml.Serialization;

namespace Hospital__2021_10_
{
    public class Program
    {
        /*
         * 
         * 
         * 
         * 
         *                 ADMIN
         * 
         *                          USERNAME:  Admin
         * 
         *                          PASSWORD:  admin12345
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         */
        static void Main(string[] args)
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<User>));
            Admin admin = new Admin();
            List<Doctor> doc = new List<Doctor>();
            List<Nurse> nur = new List<Nurse>();
            bool lp = false;

            try
            {
                using (Stream fxml = new FileStream("xml.xml", FileMode.Open, FileAccess.Read)) //десериализация
                {
                    var prXML = (List<User>)xml.Deserialize(fxml);
                    foreach (User u in prXML)
                    {
                        if (u.spec == "lekarz") { admin.usesr.Add(new User(u.name, u.sur, u.pes, u.spec, u.log, u.date, u.pass, u.type, u.pwz)); admin.user.doctors.Add(new Doctor(u.name, u.sur, u.pes, u.spec, u.log, u.pes, u.date, u.type, u.pwz)); }
                        else if (u.spec == "pielegniarka")
                        {
                            admin.usesr.Add(new User(u.name, u.sur, u.pes, u.spec, u.log, u.date, u.pass)); admin.user.nurse.Add(new Nurse(u.name, u.sur, u.pes, u.spec, u.log, u.pass, u.date));
                        }
                    }
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nie masz pracownikow");
                Console.ResetColor();
            }

            while (true)
            {
                Console.Write("Username: ");
                string login = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                if (login == "Admin" && password == "admin12345")
                {
                    Console.Clear();
                    Console.Write("Użytkownik: "); Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("Administrator\n"); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine(" \tIf you have problems you need to restart the console \n"); Console.ResetColor();

                    if (admin.user.doctors.Count != 0)
                    {
                        Console.WriteLine("Lekarzy");
                        for (int i = 0; i < admin.user.doctors.Count; i++) { Console.WriteLine("Imie: " + admin.user.doctors[i].name + " Surname: " + admin.user.doctors[i].sur + " Specjalizacja: " + admin.user.doctors[i].type); }
                    }
                    if (admin.user.nurse.Count != 0)
                    {
                        Console.WriteLine("Pielegniarka");
                        for (int i = 0; i < admin.user.nurse.Count; i++) Console.WriteLine("Imie: " + admin.user.nurse[i].name + " Surname: " + admin.user.nurse[i].sur);
                    }
                    while (true)
                    {
                        Console.WriteLine("\n  1.Dodac pracownika  2.Edytowac pracownika  3.Wyswetlic wszystkich");
                        string choose = Console.ReadLine();
                        switch (choose)
                        {
                            case "1":
                                Console.Clear();
                                Console.Write("Użytkownik: "); Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("Administrator\n"); Console.ResetColor();
                                admin.AddPracownik(); using (Stream fXml = new FileStream("xml.xml", FileMode.OpenOrCreate, FileAccess.Write)) // добавляем в файл
                                {
                                    xml.Serialize(fXml, admin.usesr);
                                }
                                break;
                            case "2":
                                Console.Clear();
                                Console.Write("Użytkownik: "); Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("Administrator\n"); Console.ResetColor();
                                if (admin.usesr.Count == 0) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Nie masz pracownikow"); Console.ResetColor(); break; }
                                Console.Write("Pesel pracownika: ");
                                string pesel = Console.ReadLine();
                                Console.WriteLine("1.Imie, 2.Nazwisko, 3.Pesel, 4.PWZ, 5.Username,  6.Password, 7.Dni dyzurow");
                                string choice = Console.ReadLine();
                                admin.EditPracownik(pesel, choice);
                                using (Stream fXml = new FileStream("xml.xml", FileMode.OpenOrCreate, FileAccess.Write)) // добавляем в файл
                                {
                                    xml.Serialize(fXml, admin.usesr);
                                }
                                break;
                            case "3":
                                Console.Clear();
                                Console.Write("Użytkownik: "); Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("Administrator\n"); Console.ResetColor();
                                admin.allEmp();
                                break;
                            default: Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Wybierz 1 z 3"); Console.ResetColor(); break;
                        }
                    }
                }
                for (int u = 0; u < admin.usesr.Count; u++)
                {
                    if (admin.usesr[u].log == login && admin.usesr[u].pass == password)
                    {
                        lp = true;
                        if (admin.usesr[u].spec == "lekarz")
                        {
                            Console.Clear();
                            Console.Write("Użytkownik: "); Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Lekarz\n"); Console.ResetColor();
                        }
                        else
                        {
                            Console.Clear();
                            Console.Write("Użytkownik: "); Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine("Pielęgniarka\n"); Console.ResetColor();
                        }
                        Console.WriteLine("\t\t--------Lekarzy--------");
                        for (int i = 0; i < admin.user.doctors.Count; i++) Console.WriteLine("Imie: " + admin.user.doctors[i].name + " Surname: " + admin.user.doctors[i].sur + " Specjalizacja: " + admin.user.doctors[i].type);
                        Console.WriteLine("\t\t--------Pielegniarka--------");
                        for (int i = 0; i < admin.user.nurse.Count; i++) Console.WriteLine("Imie: " + admin.user.nurse[i].name + " Surname: " + admin.user.nurse[i].sur);
                        Console.WriteLine("--------------------------------------");
                        Console.WriteLine("Twoj plan dyzurow");
                        for (int i = 0; i < admin.usesr[u].date.Length; i++) Console.WriteLine(admin.usesr[u].date[i] + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString());
                        Console.ReadKey();
                        break;
                    }
                }
                if (lp == false)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Zły login lub hasło"); Console.ResetColor();
                }
                else Console.Clear();


            }
        }

    }
}