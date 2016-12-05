using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECourse.Models
{
    public class Group
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Codigo")]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "el campo {0} es obligatorio.")]
        [Range(1, double.MaxValue, ErrorMessage = "debes seleccionar un Curso para el Grupo")]
        public int CourseID { get; set; }

        [Required(ErrorMessage = "el campo {0} es obligatorio.")]
        [Range(1, double.MaxValue, ErrorMessage = "debes seleccionar un administrador para el Grupo")]
        public int UserId { get; set; }

        [Display(Name = "Capacidad")]
        public int Capacity { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Imagen")]
        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }

        [Display(Name = "Activo")]
        public bool IsActive { get; set; }

        [NotMapped]
        public HttpPostedFileBase FilePhoto { get; set; }
        public virtual Course Courses { get; set; }
        public virtual User Instructor { get; set; }
    }
}