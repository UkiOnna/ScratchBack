using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dtos
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int CreatorId { get; set; }
        public int? ExecutorId { get; set; }
        public int ProjectId { get; set; }
    }
}
