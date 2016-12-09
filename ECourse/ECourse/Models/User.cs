
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECourse.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        [Index("UserNameIndex", IsUnique = true)]
        [Required(ErrorMessage = "el campo {0} es obligatorio")]
        [StringLength(100, ErrorMessage = "el campo {0} debe contener entre {2} y {1}", MinimumLength = 7)]
        public string UserName { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "el campo {0} es obligatorio")]
        [StringLength(20, ErrorMessage = "el campo {0} debe contener entre {2} y {1}", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "el campo {0} es obligatorio")]
        [StringLength(30, ErrorMessage = "el campo {0} debe contener entre {2} y {1}", MinimumLength = 3)]
        public string LastName { get; set; }

        [Display(Name = "Usuario")]
        public string FullName { get { return string.Format("{0} {1}", this.FirstName, this.LastName); } }

        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "el campo {0} es obligatorio")]
        [StringLength(10, ErrorMessage = "el campo {0} debe contener entre {2} y {1}", MinimumLength = 10)]
        public string Phone { get; set; }

        [Display(Name = "Direccion")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "el campo {0} es obligatorio")]
        [StringLength(250, ErrorMessage = "el campo {0} debe contener entre {2} y {1}", MinimumLength = 5)]
        public string Address { get; set; }

        [Display(Name = "Imagen")]
        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }

        [Display(Name = "Estudiante")]
        public bool IsStudent { get; set; }

        [Display(Name = "Profesor")]
        public bool IsTeacher { get; set; }

        [Display(Name ="Fecha de Ingreso")]
        public DateTime CreateUser { get; set; }

        [NotMapped]
        public HttpPostedFileBase FilePhoto { get; set; }

        public virtual ICollection<TeacherToCourse> TeacherToCourses { get; set; }
        public virtual ICollection<GroupDetail> GroupDetails { get; set; }
        public virtual ICollection<GroupDetailTmp> GroupDetailsTmp { get; set; }
    }
}