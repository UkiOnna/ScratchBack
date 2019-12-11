using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dtos
{
    public class IntervalDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TaskId { get; set; }
        public string Comment { get; set; }
    }
}
