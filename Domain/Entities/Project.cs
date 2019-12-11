using Models.Dtos;
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

        public Project()
        {

        }

        public Project(ProjectDto project)
        {
            Id = project.Id;
            Title = project.Title;
            DepartamentId = project.DepartamentId;
        }
    }
}
