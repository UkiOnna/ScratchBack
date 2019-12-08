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
    public class AuthController : ControllerBase
    {
        private readonly ScratchContext _context;

        public AuthController(ScratchContext context)
        {
            _context = context;
        }

        // GET: api/Auth
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUser()
        {
            List<UserDto> users = new List<UserDto>();
            foreach (var user in _context.User)
            {
                users.Add(new UserDto() { Id = user.Id, Token = "", DepartmentId = user.DepartmentId, FirstName = user.FirstName, LastName = user.LastName, MiddleName = user.MiddleName, Password = user.Password, RoleId = user.RoleId, Username = user.Username });
            }
            return users.ToArray();
        }

        // GET: api/Auth/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            UserDto _user = new UserDto() { Id = user.Id, Token = "", DepartmentId = user.DepartmentId, FirstName = user.FirstName, LastName = user.LastName, MiddleName = user.MiddleName, Password = user.Password, RoleId = user.RoleId, Username = user.Username };
            if (user == null)
            {
                return NotFound();
            }

            return _user;
        }

        // PUT: api/Auth/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Auth
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDto user)
        {
            User _user = new User() { DepartmentId = user.DepartmentId, FirstName = user.FirstName, LastName = user.LastName, MiddleName = user.MiddleName, Password = user.Password, RoleId = user.RoleId, Username = user.Username };
            _context.User.Add(_user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = _user.Id }, _user);
        }

        // DELETE: api/Auth/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        [HttpPost("sign-in")]
        public IActionResult SignIn(UserLoginDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest();
            }

            var user = new User { Username = userDto.Username, Password = userDto.Password };

            Guid g = Guid.NewGuid();
            string token = g.ToString();
            //string token = Convert.ToBase64String(g.ToByteArray());
            //token = token.Replace("=", "");
            //token = token.Replace("+", "");

            var resultUser = _context.User.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (resultUser != null)
            {
                resultUser.Token = token;
                _context.SaveChanges();

                return Ok(token);
            }

            return BadRequest();
        }

        [HttpGet("token/{id:guid}")]
        public IActionResult GetUserByToken(string token)
        {
            var user = _context.User.FirstOrDefault(u => u.Token == token);
            if (user != null)
            {
                UserDto userDto = new UserDto() { Id = user.Id, Token = user.Token, DepartmentId = user.DepartmentId, FirstName = user.FirstName, LastName = user.LastName, MiddleName = user.MiddleName, Password = user.Password, RoleId = user.RoleId, Username = user.Username };
                return Ok(userDto);
            }
            return BadRequest();
        }

        [HttpGet("fill")]
        public IActionResult FillDb()
        {
            if (!_context.Role.Any())
            {
                _context.Role.Add(new Role { Name = "Worker" });
                _context.Role.Add(new Role { Name = "Department" });
                _context.Role.Add(new Role { Name = "Subdivision" });
                _context.Role.Add(new Role { Name = "Admin" });
            }

            if (!_context.Subdivision.Any())
            {
                _context.Subdivision.Add(new Subdivision { Name = "FAFA" });
            }

            if (!_context.Department.Any())
            {
                _context.Department.Add(new Department { Name = "Департамент #1", SubdivisionId = 1 });
                _context.Department.Add(new Department { Name = "Департамент #2", SubdivisionId = 1 });
            }

            if (!_context.Project.Any())
            {
                _context.Project.Add(new Project { DepartamentId = 1, Title = "Важный проект" });
            }

            if (!_context.User.Any())
            {
                _context.User.Add(new User { FirstName = "root", MiddleName = "root", LastName = "root", DepartmentId = 1, RoleId = 4, Username = "root", Password = "root" });
                _context.User.Add(new User { FirstName = "Игорь", MiddleName = "Иванович", LastName = "Тактаков", DepartmentId = 1, RoleId = 1, Username = "tak", Password = "tak" });
                _context.User.Add(new User { FirstName = "Монет", MiddleName = "Монетович", LastName = "Монетов", DepartmentId = 2, RoleId = 2, Username = "monet", Password = "monet" });
            }

            if (!_context.Task.Any())
            {
                _context.Task.Add(new Domain.Entities.Task { CreatorId = 1, ProjectId = 1, DeadLine = DateTime.UtcNow, Decription = "Выполнить как можно скорее", Title = "Важная", ExecutorId = 2 });
            }

            _context.SaveChanges();

            return Ok("Все ок сэр");
        }
    }
}
