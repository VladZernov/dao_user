using System;
using System.Collections.Generic;

namespace DAOUserProject.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Delivery { get; set; }
        public string EMail { get; set; }
        public string FirstName { get; set; }
        public string Icq { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
