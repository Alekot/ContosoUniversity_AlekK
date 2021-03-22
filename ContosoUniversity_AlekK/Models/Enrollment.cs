using ContosoUniversity_AlekK.Models.Enums;
using System;
using System.Collections.Generic;

namespace ContosoUniversity_AlekK.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}