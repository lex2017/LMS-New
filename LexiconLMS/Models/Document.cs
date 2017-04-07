using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        [Display(Name = "Filnamn")]
        public string FileName { get; set; }
        public string FilePath { get; set; }
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }
        [Display(Name = "Uppladdat")]
        public DateTime TimeStamp { get; set; }
        [Display(Name = "Inlämnas senast")]
        public DateTime DeadlineDate { get; set; }
        public string UserId { get; set; }


        public int? CourseId { get; set; }
        [Display(Name = "Kurs")]
        public virtual Course Course { get; set; }
        public int? ModuleId { get; set; }
        [Display(Name = "Modul")]
        public virtual Module Module { get; set; }
        public int? ActivityId { get; set; }
        [Display(Name = "Aktivitet")]
        public virtual Activity Activity { get; set; }
        public int FileId { get; set; }

    }
}