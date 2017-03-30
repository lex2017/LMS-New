using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class ActivityType
    {
        public int ActivityTypeID { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection <Activity> Activities { get; set; }
    }
}