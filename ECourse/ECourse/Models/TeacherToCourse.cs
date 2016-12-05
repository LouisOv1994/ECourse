using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECourse.Models
{
    public class TeacherToCourse
    {
        public int TeacherToCourseId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }

        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
    }
}