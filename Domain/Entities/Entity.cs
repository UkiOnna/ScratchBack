using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
