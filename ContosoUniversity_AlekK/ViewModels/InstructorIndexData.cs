using ContosoUniversity_AlekK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity_AlekK.ViewModels
{
    public class InstructorIndexData
    {
        public  IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}