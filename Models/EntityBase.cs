using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models
{
    public class Nots
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; } = "Default Description";
        public string CreatedAt { get; set; } = DateTime.Now.ToString();
    }
}
