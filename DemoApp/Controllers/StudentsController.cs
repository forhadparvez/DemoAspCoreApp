using DemoApp.Data;
using DemoApp.Models;
using DemoApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;

namespace DemoApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentRepository _repository;
        private readonly DepartmentRepository _departmentRepository;
        private readonly SemesterRepository _semesterRepository;

        public StudentsController(ApplicationDbContext context)
        {
            _repository = new StudentRepository(context);
            _departmentRepository = new DepartmentRepository(context);
            _semesterRepository = new SemesterRepository(context);
        }

        [Authorize(Roles = UserRoles.Admin + ", " + UserRoles.Viewer)]
        public IActionResult Index()
        {
            var entity = _repository.GetAll();
            return View(entity);
        }


        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Create(int? id)
        {
            ViewData["DepartmentId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            ViewData["SemesterId"] = new SelectList(_semesterRepository.GetAllSemester(), "Id", "Name");
            ViewBag.Image = "";

            if (id != null)
            {
                var dbId = Convert.ToInt32(id);
                var entity = _repository.GetById(dbId);
                if (entity != null)
                {
                    try
                    {
                        var base64 = ByteArrayToImage(entity.Image);
                        ViewBag.Image = "data:image/gif;base64," + base64;
                    }
                    catch (Exception)
                    {
                        //iggnor
                    }

                    return View(entity);

                }
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Create(Student entity)
        {
            ViewData["DepartmentId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            ViewData["SemesterId"] = new SelectList(_semesterRepository.GetAllSemester(), "Id", "Name");

            // image save option
            entity.Image = entity.ProfileImage.FileName != "" ? FileToByteArray(entity.ProfileImage) : new byte[0];

            // TODO: Add insert logic here
            ModelState.Remove("Id");
            if (!ModelState.IsValid)
                return View(entity);

            if (entity.Id > 0)
            {
                // edit
                var result = _repository.Update(entity);
                ViewBag.Message = result > 0 ? "Save Success" : "Save Fail";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // save
                var result = _repository.Save(entity);
                ViewBag.Message = result > 0 ? "Save Success" : "Save Fail";
                ModelState.Clear();
                return View();

            }
        }


        public byte[] FileToByteArray(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                // act on the Base64 data
                return fileBytes;
            }
        }

        public string ByteArrayToImage(byte[] fileBytes)
        {
            var s = Convert.ToBase64String(fileBytes);
            return s;
        }
    }
}