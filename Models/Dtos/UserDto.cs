using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public int? DepartmentId { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
