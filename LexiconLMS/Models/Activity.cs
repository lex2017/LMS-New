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
        [LessThanOrEqualTo("EndDate", ErrorMessage = "Startdatum kan inte vara efter sludatum")]
        [Display(Name = "Startdatum")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [GreaterThanOrEqualTo("StartDate", ErrorMessage = "Slutdatum kan inte vara före startdatum.")]
        [Display(Name = "Slutdatum")]
        public DateTime EndDate { get; set; }

        public int? ModuleId { get; set; }
        public virtual Module Module { get; set; }


        public int? ActivityTypeID { get; set; }
        public virtual ActivityType ActivityType { get; set; }


    }
}