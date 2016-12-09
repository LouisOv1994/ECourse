using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECourse.Models
{
    public class GroupDetail
    {
        [Key]
        public int GroupDetailId { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }

        [Display(Name ="Fecha de Inscripcion")]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Activo")]
        public bool IsActive { get; set; }

        public User User { get; set; }
        public Group Group { get; set; }
    }
}