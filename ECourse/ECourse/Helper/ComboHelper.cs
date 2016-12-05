using ECourse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECourse.Helper
{
    public class ComboHelper : IDisposable
    {
        private static ECourseContext db = new ECourseContext();


        public static List<Course> GetCourse()
        {
            var courses = db.Courses.Where(u => u.IsActive).ToList();
            courses.Add(new Course
            {
                CourseId = 0,
                Title = "[Seleccionar Curso...]"
            });
            return courses.OrderBy(i => i.Title).ThenBy(i => i.CreationDate).ToList();
        }

        public static List<User> GetTeacher()
        {
            var users = db.Users.Where(u => u.IsTeacher).ToList();
            users.Add(new User
            {
                UserId = 0,
                FirstName = "[Seleccionar Administrador...]"
            });
            return users.OrderBy(i => i.FirstName).ThenBy(i => i.LastName).ToList();
        }

        public static List<Department> GetDepartment()
        {
            var departments = db.Departments.ToList();
            departments.Add(new Department
            {
                UserId = 0,
                Name = "[Seleccionar Departamento...]"
            });
            return departments.OrderBy(i => i.Name).ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}