using DemoApp.Data;
using DemoApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DemoApp.Repositories
{
    public class StudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Student GetById(int id)
        {
            return _context.Students.Find(id);
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students
                .Include(c => c.Department)
                .Include(c => c.Semester)
                .ToList();
        }

        public Student GetByRollNo(string roll)
        {
            return _context.Students.SingleOrDefault(c => c.Roll == roll);
        }

        public bool IsRollExist(string roll)
        {
            bool isExist = false;
            var entity = GetByRollNo(roll);
            if (entity != null)
                isExist = true;
            return isExist;
        }

        public int Save(Student entity)
        {
            if (IsRollExist(entity.Roll)) return 0;

            _context.Students.Add(entity);
            _context.SaveChanges();

            return entity.Id;
        }

        public int Update(Student entity)
        {
            _context.Students.Update(entity);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
                _context.Students.Remove(entity);

            return _context.SaveChanges();
        }
    }
}