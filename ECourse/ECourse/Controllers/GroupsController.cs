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
    public class GroupsController : Controller
    {
        private ECourseContext db = new ECourseContext();

        public ActionResult Index()
        {
            var groups = db.Groups.Include(g => g.Courses).Include(g => g.Instructor);
            return View(groups.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        public JsonResult GetUsers(int courseid)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var usersTocourse = db.TeacherToCourses.Where(uc => uc.CourseId == courseid)
                .Select(u => u.User);
            return Json(usersTocourse);
        }

        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(ComboHelper.GetCourse(), "CourseId", "Title");
            ViewBag.UserId = new SelectList(ComboHelper.GetTeacher(), "UserId", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Group group)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Groups.Add(group);
                    db.SaveChanges();

                    if (group.FilePhoto != null)
                    {
                        var folder = "~/Content/Photos/Groups";
                        var name = string.Format("{0}.Jpg", group.GroupId);

                        var response = FileHelper.UploadPhoto(group.FilePhoto, folder, name);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, name);
                            group.Photo = pic;


                            db.Entry(group).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Se produjo un Error a la Hora de Registrar este grupo");
                }
            }

            ViewBag.CourseID = new SelectList(ComboHelper.GetCourse(), "CourseId", "Title", group.CourseID);
            ViewBag.UserId = new SelectList(ComboHelper.GetTeacher(), "UserId", "FullName", group.UserId);
            return View(group);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }

            ViewBag.CourseID = new SelectList(ComboHelper.GetCourse(), "CourseId", "Title", group.CourseID);
            ViewBag.UserId = new SelectList(ComboHelper.GetTeacher(), "UserId", "FullName", group.UserId);
            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Group group)
        {
            if (ModelState.IsValid)
            {

                if (group.FilePhoto != null)
                {
                    var folder = "~/Content/Photos/Groups";
                    var name = string.Format("{0}.Jpg", group.GroupId);

                    var response = FileHelper.UploadPhoto(group.FilePhoto, folder, name);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, name);
                        group.Photo = pic;
                    }
                }

                try
                {
                    db.Entry(group).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Se produjo un Error a la Hora de guardar este usuario");
                }

            }
                ViewBag.CourseID = new SelectList(ComboHelper.GetCourse(), "CourseId", "Title", group.CourseID);
                ViewBag.UserId = new SelectList(ComboHelper.GetTeacher(), "UserId", "FullName", group.UserId);
                return View(group);
            }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);

            try
            {
                db.Groups.Remove(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty,
                    "Se produjo un Error a la Hora de eliminar este grupo");
            }
            return View(group);
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
