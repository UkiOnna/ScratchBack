using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public int DepartmentId { get; set; }
        public string Login { get; set; }
    }
}
