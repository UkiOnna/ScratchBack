using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Interval : Entity
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public string Comment { get; set; }

        public Interval()
        {

        }

        public Interval(IntervalDto interval)
        {
            StartDate = interval.StartDate;
            EndDate = interval.EndDate;
            TaskId = interval.TaskId;
            Comment = interval.Comment;
        }
    }
}
