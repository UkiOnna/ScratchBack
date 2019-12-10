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
    public class DepartmentsController : ControllerBase
    {
        private readonly ScratchContext _context;

        public DepartmentsController(ScratchContext context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartment()
        {
            List<DepartmentDto> departmentDtos = new List<DepartmentDto>();
            foreach (var department in _context.Department)
            {
                departmentDtos.Add(new DepartmentDto { Name = department.Name, SubdivisionId = department.SubdivisionId, Id = department.Id });
            }
            return departmentDtos.ToArray();
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartment(int id)
        {
            var department = await _context.Department.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }
            DepartmentDto _department = new DepartmentDto() { Name = department.Name, SubdivisionId = department.SubdivisionId, Id = department.Id };

            return _department;
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

        // POST: api/Departments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult PostDepartment(DepartmentDto department)
        {
            Department departmentDto = new Department() { Name = department.Name, SubdivisionId = department.SubdivisionId };
            _context.Department.Add(departmentDto);
            _context.SaveChanges();

            return Ok();
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {
            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Department.Remove(department);
            await _context.SaveChangesAsync();

            return department;
        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.Id == id);
        }

        [HttpGet("subdivision-departments/{id}")]
        public async Task<ActionResult<Department>> GetSubdivisionDepartments(int id)
        {
            List<DepartmentDto> departmentDtos = new List<DepartmentDto>();
            var _departments = _context.Department.Where(s => s.SubdivisionId == id);
            foreach (var department in _departments)
            {
                departmentDtos.Add(new DepartmentDto { Name = department.Name, SubdivisionId = department.SubdivisionId, Id = department.Id });
            }
            return Ok(departmentDtos.ToArray());
        }
    }
}
