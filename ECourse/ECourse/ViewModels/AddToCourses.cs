using ECourse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECourse.ViewModels
{
    public class AddToCourses
    {
        public int UserId { get; set; }
        public List<Course> Courses { get; set; }
    }
}