using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class ActivityType
    {
        public int ActivityTypeID { get; set; }
        public string Name { get; set; }
        public int? ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
    }
}