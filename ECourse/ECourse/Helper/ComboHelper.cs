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

        public void Dispose()
        {
            db.Dispose();
        }
    }
}