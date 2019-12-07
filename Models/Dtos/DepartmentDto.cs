using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dtos
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubdivisionId { get; set; }
    }
}
