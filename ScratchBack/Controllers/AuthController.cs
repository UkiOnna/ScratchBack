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
        public async Task<ActionResult<User>> SignIn(User user)
        {
            Guid g = Guid.NewGuid();
            string token = Convert.ToBase64String(g.ToByteArray());
            token = token.Replace("=", "");
            token = token.Replace("+", "");
            if (_context.User.All(u => u.Password == user.Password && u.Username == user.Username))
            {
                User _user = _context.User.FirstOrDefault(us => us.Username == user.Username);
                _user.Token = token;
                await _context.SaveChangesAsync();
                return Ok(token);
            }
            return Ok(null);
        }

        [HttpGet("token/{id:guid}")]
        public async Task<ActionResult<UserDto>> GetUserByToken(string token)
        {
            if (_context.User.All(u => u.Token == token))
            {
                User user = _context.User.FirstOrDefault(us => us.Token == token);
                UserDto _user = new UserDto() { Id = user.Id, Token = user.Token, DepartmentId = user.DepartmentId, FirstName = user.FirstName, LastName = user.LastName, MiddleName = user.MiddleName, Password = user.Password, RoleId = user.RoleId, Username = user.Username };
                return Ok(user);
            }
            return Ok(null);
        }
    }
}
