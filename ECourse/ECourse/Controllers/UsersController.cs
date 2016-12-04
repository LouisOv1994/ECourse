﻿using System;
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
    public class UsersController : Controller
    {
        private ECourseContext db = new ECourseContext();

        public ActionResult Index()
        {
            var users = db.Users
                .OrderBy(u => u.CreateUser)
                .ThenBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .ToList();

            return View(users);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.CreateUser = DateTime.Now;

                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();

                    if (user.IsStudent)
                    {
                        UserHelper.CreateUserASP(user.UserName, "Student");
                    }

                    if (user.IsTeacher)
                    {
                        UserHelper.CreateUserASP(user.UserName, "Teacher");
                    }

                    return RedirectToAction("Index");

                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Se produjo un Error a la Hora de Registrar este usuario");
                }

                if (user.FilePhoto != null)
                {
                    var folder = "~/Content/Photos/Users";
                    var name = string.Format("{0}.Jpg", user.UserId);

                    var response = FileHelper.UploadPhoto(user.FilePhoto, folder, name);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, name);
                        user.Photo = pic;


                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }

            return View(user);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.FilePhoto != null)
                {
                    var folder = "~/Content/Photos/Users";
                    var name = string.Format("{0}.Jpg", user.UserId);

                    var response = FileHelper.UploadPhoto(user.FilePhoto, folder, name);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, name);
                        user.Photo = pic;
                    }
                }

                try
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();

                    var dbtwo = new ECourseContext();

                    var currentUser = dbtwo.Users.Find(user.UserId);

                    if (currentUser.UserName != user.UserName)
                    {
                        UserHelper.UpdateUsers(user.UserName, currentUser.UserName);
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Se produjo un Error a la Hora de Editar este usuario");
                }
            }
            return View(user);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);

            try
            {
                db.Users.Remove(user);
                db.SaveChanges();
                UserHelper.DeleteUsers(user.UserName);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty,
                    "Se produjo un Error a la Hora de Eliminar este usuario");
            }

            return View(user);
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
