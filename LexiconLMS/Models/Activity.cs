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
        [Display(Name = "Aktivitinamn")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Startdatum")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Slutdatum")]
        public DateTime EndDate { get; set; }

        public int? ModuleId { get; set; }

        public virtual Module Module { get; set; }



    }
}