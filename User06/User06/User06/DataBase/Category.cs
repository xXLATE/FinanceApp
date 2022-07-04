using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace User06.DataBase
{
    [Table("Categories")]
    public class Category
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        [Unique]
        public string Name { get; set; }
        public int User_Id { get; set; }
    }
}
