using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }

        [Required]
        [Display(Name = "Aktivitet")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [LessThanOrEqualTo("EndDate", ErrorMessage = "Startdatum kan inte vara efter slutdatum")]
        [Display(Name = "Startdatum")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [GreaterThanOrEqualTo("StartDate", ErrorMessage = "Slutdatum kan inte vara före startdatum.")]
        [Display(Name = "Slutdatum")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Modul")]
        public int ModuleId { get; set; }
        [Display(Name = "Modul")]
        public virtual Module Module { get; set; }

        [Display(Name = "Aktivitetstyp")]
        public int? ActivityTypeID { get; set; }
        [Display(Name = "Aktivitetstyp")]
        public virtual ActivityType ActivityType { get; set; }
        public virtual ICollection<Document> Documents { get; set; }


    }
}