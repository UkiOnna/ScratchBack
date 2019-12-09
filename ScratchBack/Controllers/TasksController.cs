﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Data;
using Domain.Entities;
using Models.Dtos;

namespace ScratchBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ScratchContext _context;

        public TasksController(ScratchContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.Entities.Task>>> GetTask()
        {
            return await _context.Task.ToListAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Domain.Entities.Task>> GetTask(int id)
        {
            var task = await _context.Task.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, Domain.Entities.Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tasks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Domain.Entities.Task>> PostTask(Domain.Entities.Task task)
        {
            _context.Task.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { id = task.Id }, task);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Domain.Entities.Task>> DeleteTask(int id)
        {
            var task = await _context.Task.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Task.Remove(task);
            await _context.SaveChangesAsync();

            return task;
        }

        private bool TaskExists(int id)
        {
            return _context.Task.Any(e => e.Id == id);
        }

        [HttpGet("{int:id}/{startDate}/{endDate}")]
        public async Task<ActionResult<IEnumerable<TaskPersonStatisticDto>>> GetPersonStatisticDtos(
            int userId, DateTime startDate, DateTime endDate)
        {
            List<TaskPersonStatisticDto> taskDtos = new List<TaskPersonStatisticDto>();
            var tasks = _context.Task.
                Where(t => t.CreatedDate >= startDate
               && t.CreatedDate <= endDate);
            foreach(var task in tasks) {
                if(task.ExecutorId == userId) {
                    Interval interval = await _context.Interval.FirstOrDefaultAsync(i => i.TaskId == task.Id).ConfigureAwait(true);
                    int work_times = 0;
                    var all_work_times = (interval.EndDate - interval.StartDate).Hours;
                    if (interval.StartDate <= DateTime.Now &&
                        interval.EndDate >= DateTime.Now) {
                        if(all_work_times >= 24) {
                            work_times = 24;
                        }
                        else {
                            work_times = all_work_times;
                        }
                    }
                    
                        taskDtos.Add(
                        new TaskPersonStatisticDto()
                        {
                            Id = task.Id,
                            TaskName = task.Title,
                            TaskStatus = "None",
                            ProjectName = task.Project.Title,
                            TodayWorkTime = work_times,
                            AllWorkTime = all_work_times
                        }
                        );
                }
            }
            return taskDtos.ToArray();
        }
    }
}
