using DemoApp.Data;
using DemoApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DemoApp.Repositories
{
    public class SemesterRepository
    {
        private readonly ApplicationDbContext _context;

        public SemesterRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Semester> GetAllSemester()
        {
            return _context.Semesters.Include(c => c.Department).ToList();
        }

        public Semester FindById(int id)
        {
            return _context.Semesters.Find(id);
        }


        public int Save(Semester entity)
        {
            _context.Semesters.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public int Update(Semester entity)
        {
            _context.Semesters.Update(entity);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var entity = _context.Semesters.Find(id);
            if (entity != null)
                _context.Semesters.Remove(entity);

            return _context.SaveChanges();
        }
    }
}