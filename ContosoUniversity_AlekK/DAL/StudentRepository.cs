using ContosoUniversity_AlekK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ContosoUniversity_AlekK.DAL
{
    public class StudentRepository : IStudentRepository, IDisposable
    {
        private bool disposed = false;
        private SchoolContext context;
        public StudentRepository(SchoolContext context)
        {
            this.context = context;
        }

        public void DeleteStudent(int studentId)
        {
            Student student = context.Students.Find(studentId);
            context.Students.Remove(student);
        }

        public Student GetStudentByID(int studentId)
        {
            return context.Students.Find(studentId);
        }

        public IEnumerable<Student> GetStudents()
        {
            return context.Students.ToList();
        }

        public void InsertStudent(Student student)
        {
            context.Students.Add(student);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateStudent(Student student)
        {
            context.Entry(student).State = EntityState.Modified;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposed = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~StudentRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}