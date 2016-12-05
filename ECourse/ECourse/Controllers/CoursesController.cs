using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECourse.Models;
using ECourse.Helper;

namespace ECourse.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CoursesController : Controller
    {
        private ECourseContext db = new ECourseContext();

        public ActionResult Index()
        {
            var courses = db.Courses.Where(c => c.IsActive)
                .OrderBy(c => c.Title)
                .Include(c => c.Department)
                .ToList();
            return View(courses);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(ComboHelper.GetDepartment(), "DepartmentId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    course.CreationDate = DateTime.Now;
                    db.Courses.Add(course);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Se produjo un Error a la Hora de agregar este curso");
                }
            }

            ViewBag.DepartmentId = new SelectList(ComboHelper.GetDepartment(), "DepartmentId", "Name", course.DepartmentId);
            return View(course);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(ComboHelper.GetDepartment(), "DepartmentId", "Name", course.DepartmentId);
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(course).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Se produjo un Error a la Hora de editar este curso");
                }
            }
            ViewBag.DepartmentId = new SelectList(ComboHelper.GetDepartment(), "DepartmentId", "Name", course.DepartmentId);
            return View(course);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var course = db.Courses.Find(id);
            try
            {
                db.Courses.Remove(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty,
                    "Se produjo un Error a la Hora de eliminar este curso");
            }

            return View(course);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
