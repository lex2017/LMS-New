using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Foolproof;

namespace LexiconLMS.Models
{
    public class Course
    {

        public int CourseID { get; set; }

        [Required]
        [Display(Name = "Kurs")]
        public string Name { get; set; }

        [Display(Name = "Beskrivning")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Startdatum")]
        [LessThanOrEqualTo("EndDate", ErrorMessage = "Startdatum kan inte vara efter sludatum")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Slutdatum")]
        [GreaterThanOrEqualTo("StartDate", ErrorMessage = "Slutdatum kan inte vara före startdatum.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDate { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Module> Modules { get; set; }

    }
}