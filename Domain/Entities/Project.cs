using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Project : Entity
    {
        public string Title { get; set; }
        public int DepartamentId { get; set; }
        public Department Departament { get; set; }
    }
}
