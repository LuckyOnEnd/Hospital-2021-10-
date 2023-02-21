using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace admin
{
    public class Nurse : User
    {

        public Nurse() { }

        public Nurse(string name_, string surname_, string pesel_, string spec_, string login_, string pass_, int[] date_) : base(name_, surname_, pesel_, spec_, login_, date_, pass_)
        {

        }
    }
}
