using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECourse.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "el campo {0} es obligatorio.")]
        [Range(1, double.MaxValue, ErrorMessage = "debes seleccionar un administrador para el departamento")]
        public int UserId { get; set; }

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "el campo {0} es obligatorio.")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Fecha de Creacion")]
        public DateTime CreateDate { get; set; }

        public virtual User Administrator { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}