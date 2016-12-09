using ECourse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECourse.ViewModels
{
    public class AddToStudents
    {
        public int GroupId { get; set; }
        public List<User> Users { get; set; }
    }
}