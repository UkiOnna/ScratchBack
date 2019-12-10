using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DepartmentId { get; set; }
    }
}
