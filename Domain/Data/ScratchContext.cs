using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data
{
    public class ScratchContext : DbContext
    {
        public ScratchContext(DbContextOptions<ScratchContext> options) : base(options)
        {
        }
        public DbSet<Department> Department { get; set; }
        public DbSet<Interval> Interval { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Subdivision> Subdivision { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<User> User { get; set; }
    }
}
