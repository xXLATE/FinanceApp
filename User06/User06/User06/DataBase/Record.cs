using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace User06.DataBase
{
    [Table("Records")]
    public class Record
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public int Type_Id { get; set; }
        public int Category_Id { get; set; }
        public string Sum { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int User_Id { get; set; }
    }
}
