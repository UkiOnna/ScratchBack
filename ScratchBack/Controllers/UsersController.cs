using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Repositories;

namespace ScratchBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RepositoryService _repository;

        public UsersController(RepositoryService repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return Ok(_repository.GetRepository<User>().GetAll());
        }
    }
}