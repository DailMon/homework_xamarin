using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Gibdd
{
    public class Profile
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }        
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MidleName { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Region { get; set; }
        public string Subdivision { get; set; }
        public string RegionOfIncedent { get; set; }

        public bool IsCompany { get; set; }
        public string TypeProfile { get; set; } = "Гражданин";

        // from company
        public string NameCompany { get; set; }
        public string AddInfomation { get; set; }
        public string OutgoingNumber { get; set; }
        public DateTime DateRegistration { get; set; } = DateTime.Now;
        public string NumberLetter { get; set; }       

        public override string ToString()
        {
            return Name;
        }
    }
}
