using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class ActivityType
    {
        public int ActivityTypeID { get; set; }
        [Display(Name="Aktivitetstyp")]
        public string Name { get; set; }
        public int? ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
    }
}