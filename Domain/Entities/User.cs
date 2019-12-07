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
        public Role Role { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
