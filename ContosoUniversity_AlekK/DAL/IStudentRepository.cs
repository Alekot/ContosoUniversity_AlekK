using ContosoUniversity_AlekK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity_AlekK.DAL
{
    public interface IStudentRepository : IDisposable
    {
        IEnumerable<Student> GetStudents();
        Student GetStudentByID(int studentId);
        void InsertStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int studentId);
        void Save();
    }
}