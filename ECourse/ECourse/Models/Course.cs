using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECourse.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "el campo {0} es obligatorio.")]
        [Range(1, double.MaxValue, ErrorMessage = "debes seleccionar un departamento")]
        public int DepartmentId { get; set; }

        [Display(Name = "Curso")]
        [Required(ErrorMessage = "el campo {0} es obligatorio.")]
        [StringLength(50)]
        public string Title { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "el campo {0} es obligatorio.")]
        [StringLength(50)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Modalidad")]
        [Required(ErrorMessage = "el campo {0} es obligatorio.")]
        public Modalities Modality { get; set; }

        [Display(Name = "Cant Horas")]
        [Required(ErrorMessage = "el campo {0} es obligatorio.")]
        [Range(150, 450, ErrorMessage = "el campo {0} debe contener entre {1} y {2} horas")]
        public int Hours { get; set; }

        [Display(Name = "Fecha de Creacion")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Activo")]
        public bool IsActive { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<TeacherToCourse> TeacherToCourses { get; set; }
    }

    public enum Modalities
    {
        Habilitación, Complementación
    }
}