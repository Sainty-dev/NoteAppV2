using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteAppV2.Model
{
   public class NoteItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Titles { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; } 
    }
}
