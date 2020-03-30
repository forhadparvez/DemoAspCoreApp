using DemoApp.Data;
using DemoApp.Models;
using DemoApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DemoApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentRepository _studentRepository;

        public StudentsController(ApplicationDbContext context)
        {
            _studentRepository = new StudentRepository(context);
        }

        // GET: api/Students
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return _studentRepository.GetAll();
        }

        // GET: api/Students/5
        [HttpGet("{id}", Name = "Get")]
        public Student Get(int id)
        {
            return _studentRepository.GetById(id);
        }

        // POST: api/Students
        [HttpPost]
        public IActionResult Post([FromBody]Student entity)
        {
            return Ok(_studentRepository.Save(entity));
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Student entity)
        {
            return Ok(_studentRepository.Update(entity));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_studentRepository.Delete(id));
        }
    }
}
