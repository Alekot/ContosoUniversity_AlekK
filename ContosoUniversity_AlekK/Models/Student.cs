using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity_AlekK.Models
{
    public class Student : Person
    {
        //public int StudentID { get; set; }
        //[Required]
        //[StringLength(50), MinLength(2)]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Type a Last Name that starts with a Capital letter!")]
        //[Display(Name ="Last Name")]
        //public string LastName { get; set; }
        //[Required]
        //[StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.", MinimumLength = 2)]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        //[Column("FirstName")]
        //[Display(Name = "First Name")]
        //public string FirstMidName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)] /*"{0:yyyy-MM-dd}"*/
        [Display(Name ="Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }
        //[Display(Name = "Full Name")]
        //public string FullName
        //{
        //    get
        //    {
        //        return LastName + ", " + FirstMidName;
        //    }
        //}

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}