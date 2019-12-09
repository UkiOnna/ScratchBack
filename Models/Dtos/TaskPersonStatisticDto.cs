using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dtos
{
    public class TaskPersonStatisticDto
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string TaskName { get; set; }
        public int TodayWorkTime { get; set; }
        public int AllWorkTime { get; set; }
        public string TaskStatus { get; set; }
    }
}
