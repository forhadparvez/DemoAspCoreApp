using DemoApp.Data;
using DemoApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace DemoApp.Repositories
{
    public class DepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }
    }
}