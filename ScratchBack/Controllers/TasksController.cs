using System;
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

        [HttpGet("user-tasks/{id}")]
        public ActionResult GetUserTasks(int id)
        {

            var tasks = _context.Task.Where(t => t.ExecutorId == id);

            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks);
        }

        [HttpGet("user-tasks/{id}/{date}")]
        public ActionResult GetUserTasksByDate(int id, string date)
        {
            DateTime tempDate = DateTime.Parse(date);
            var tasks = _context.Task.Where(t => t.ExecutorId == id && t.CreatedDate.Date == tempDate);

            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks);
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
        public IActionResult PostTask(TaskDto tempTask)
        {
            Domain.Entities.Task task = new Domain.Entities.Task(tempTask);
            _context.Task.Add(task);
            _context.SaveChanges();

            return Ok();
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

        [HttpGet("personal-statistics/{id}/{start}/{end}")]
        public IActionResult GetPersonStatisticDtos(int id, string start, string end)
        {
            List<TaskPersonStatisticDto> taskDtos = new List<TaskPersonStatisticDto>();
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);
            var tasks = _context.Task.Where(t => t.CreatedDate >= startDate.Date && t.CreatedDate <= endDate.Date);
            foreach (var task in tasks)
            {
                if (task.ExecutorId == id)
                {
                    Interval interval = _context.Interval.FirstOrDefault(i => i.TaskId == task.Id);
                    int work_times = 0;
                    var all_work_times = (interval.EndDate - interval.StartDate).Hours;
                    if (interval.StartDate <= DateTime.Now &&
                        interval.EndDate >= DateTime.Now)
                    {
                        if (all_work_times >= 24)
                        {
                            work_times = 24;
                        }
                        else
                        {
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
            return Ok(taskDtos);
        }
    }
}
