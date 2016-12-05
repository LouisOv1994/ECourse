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
    [Authorize(Roles = "Admin")]
    public class DepartmentsController : Controller
    {
        private ECourseContext db = new ECourseContext();

        public ActionResult Index()
        {
            var departments = db.Departments
                .OrderBy(d => d.Name)
                .Include(d => d.Administrator)
                .ToList();
            return View(departments);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(ComboHelper.GetTeacher(), "UserId", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                DuplicateAdministrators(department);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    department.CreateDate = DateTime.Now;
                    db.Departments.Add(department);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Se produjo un Error a la hora de Registrar el Departamento");
                }
            }

            ViewBag.UserId = new SelectList(ComboHelper.GetTeacher(), "UserId", "FullName", department.UserId);
            return View(department);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }

            ViewBag.UserId = new SelectList(ComboHelper.GetTeacher(), "UserId", "FullName", department.UserId);
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                DuplicateAdministrators(department);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(department).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Se produjo un Error a la hora de Editar el departamento");
                }
            }

            ViewBag.UserId = new SelectList(ComboHelper.GetTeacher(), "UserId", "FullName", department.UserId);
            return View(department);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            try
            {
                db.Departments.Remove(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty,
                    "Se produjo un Error a la Hora de Eliminar este departamento");
            }

            return View(department);
        }

        private void DuplicateAdministrators(Department department)
        {
            var duplicatedepartment = db.Departments.Include("Administrator")
                .Where(d => d.UserId == department.UserId).AsNoTracking()
                .FirstOrDefault();

            if (duplicatedepartment != null && duplicatedepartment.DepartmentId != department.DepartmentId)
            {
                string errorMessage = String.Format(
               "El Docente {0} ya es administrador del departamento {1}.",
                duplicatedepartment.Administrator.FullName,
                duplicatedepartment.Name);
                ModelState.AddModelError(string.Empty, errorMessage);
            }
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
