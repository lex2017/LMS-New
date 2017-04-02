using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime TimeStamp { get; set; }

        public string ApplicationUserId { get; set; }
        public int? ModuleId { get; set; }
        public int? CourseId { get; set; }
        public int? ActivityId { get; set; }
        public virtual Module Module { get; set; }
        public virtual Course Course { get; set; }
        public virtual Activity Activity { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}