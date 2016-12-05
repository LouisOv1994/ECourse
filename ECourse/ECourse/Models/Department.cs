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
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "el campo {0} es obligatorio.")]
        [Range(1, double.MaxValue, ErrorMessage = "debes seleccionar un administrador para el departamento")]
        public int UserId { get; set; }

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "el campo {0} es obligatorio.")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Fecha de Registro")]
        public DateTime CreateDate { get; set; }

        public virtual User Administrator { get; set; }
    }
}