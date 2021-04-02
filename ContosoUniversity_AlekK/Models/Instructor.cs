﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity_AlekK.Models
{
    public class Instructor : Person
    {
        //public int ID { get; set; }
        //[Display(Name = "Last Name"), StringLength(50, MinimumLength = 1)]
        //public string LastName { get; set; }
        //[Column("FirstName"), Display(Name = "First Name"), StringLength(50, MinimumLength = 1)]
        //public string FirstMidName { get; set; }
        [DataType(DataType.Date),Display(Name ="Hire Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }
        //[Display(Name = "Full Name")]
        //public string FullName
        //{
        //    get { return LastName + ", " + FirstMidName; }
        //}
        public virtual ICollection<Course> Courses { get; set; }
        public virtual OfficeAssignment OfficeAssignment { get; set; }
    }
}