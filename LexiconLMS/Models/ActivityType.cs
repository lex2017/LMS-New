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
        public string TypeName { get; set; }
        //public int? ActivityId { get; set; }
        public virtual ICollection<Activity> Activity { get; set; }
    }
}