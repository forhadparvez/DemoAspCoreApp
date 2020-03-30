using DemoApp.Data;
using DemoApp.Models;
using DemoApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoApp.Controllers
{
    public class SemestersController : Controller
    {
        private readonly SemesterRepository _semesterRepository;
        private readonly DepartmentRepository _departmentRepository;

        public SemestersController(ApplicationDbContext context)
        {
            _semesterRepository = new SemesterRepository(context);
            _departmentRepository = new DepartmentRepository(context);

        }


        // GET: Semesters
        public ActionResult Index()
        {
            var entities = _semesterRepository.GetAllSemester();

            return View(entities);
        }

        // GET: Semesters/Details/5
        public ActionResult Details(int id)
        {
            var entity = _semesterRepository.FindById(id);
            return View(entity);
        }

        // GET: Semesters/Create
        public ActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Semesters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Semester entity)
        {
            try
            {
                ViewData["DepartmentId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                    return View(entity);

                _semesterRepository.Save(entity);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Semesters/Edit/5
        public ActionResult Edit(int id)
        {
            ViewData["DepartmentId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            var entity = _semesterRepository.FindById(id);
            if (entity != null)
                return View(entity);
            return View();
        }

        // POST: Semesters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Semester entity)
        {
            try
            {
                ViewData["DepartmentId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
                // TODO: Add update logic here
                _semesterRepository.Update(entity);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Semesters/Delete/5
        public ActionResult Delete(int id)
        {
            var entity = _semesterRepository.FindById(id);
            return View(entity);
        }

        // POST: Semesters/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                _semesterRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}