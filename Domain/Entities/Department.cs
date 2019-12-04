using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Department : Entity
    {
        public string Name { get; set; }
        public int SubdivisionId { get; set; }
        public Subdivision Subdivision { get; set; }
    }
}
