using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace admin
{
    [XmlInclude(typeof(Doctor)), XmlInclude(typeof(Nurse))]
    [Serializable]
    public class User
    {
        public List<Doctor> doctors { get; set; } = new List<Doctor>();
        public List<Nurse> nurse { get; set; } = new List<Nurse>();
        public string name { get; set; }
        public string sur { get; set; }
        public string pes { get; set; }
        public string spec { get; set; }
        public string log { get; set; }
        public string pass { get; set; }
        public string type { get; set; }
        public string pwz { get; set; }
        public int[] date { get; set; }

        public User() { }


        public User(string name_, string surname_, string pesel_, string spec_, string login_, int[] date_, string pass_, string type_, string pwz_)
        {
            name = name_;
            sur = surname_;
            pes = pesel_;
            spec = spec_;
            log = login_;
            pass = pass_;
            date = date_;
            type = type_;
            pwz = pwz_;

        }

        public User(string name_, string surname_, string pesel_, string spec_, string login_, int[] date_, string pass_)
        {
            name = name_;
            sur = surname_;
            pes = pesel_;
            spec = spec_;
            log = login_;
            pass = pass_;
            date = date_;

        }
    }
}
