using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gibdd
{
    public class TextAppeal
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }
        public override string ToString()
        {
            return Text;
        }
    }
    
}
