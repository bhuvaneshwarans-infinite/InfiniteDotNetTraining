using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MVCAssessmentQuestion2.Models
{
    public class Movie
    {
        //Movie(Mid, Moviename, DateofRelease)
        [Key]
        public int Mid { get; set; }
        public string Moviename { get; set; }

        public DateTime DateofRelease { get; set; }

    }
}