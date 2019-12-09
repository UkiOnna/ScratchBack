using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Task : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DeadLine { get; set; }
        public int CreatorId { get; set; }
        public User Creator { get; set; }
        public int? ExecutorId { get; set; }
        public User Executor { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public Task(TaskDto task)
        {
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            DeadLine = DateTime.Parse(task.Deadline);
            CreatorId = task.CreatorId;
            ExecutorId = task.ExecutorId;
            ProjectId = task.ProjectId;
        }

        public Task()
        {

        }


    }
}
