using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBA_League.domain;
using NBA_League.repository;
using NBA_League.validation;

namespace NBA_League.service
{
    class StudentService
    {
        private StudentFileRepository repo;
        private Student student;

        public StudentService(StudentFileRepository repo)
        {
            this.repo = repo;
            this.student = default(Student);
        }

        public Student SaveStudent(string name, string school)
        {
            student = default(Student);
            try
            {
                student = repo.Save(new Student(getMaxID(), name, school));
            }catch(ValidationException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return student;
        }

        public Student UpdateStudent(int ID, string name, string school)
        {
            student = default(Student);
            try
            {
                student = repo.Update(new Student(ID, name, school));
            }catch(ValidationException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return student;
        }

        public Student DeleteStudent(int ID)
        {
            student = default(Student);
            try
            {
                student = repo.Delete(ID);
            }catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return student;
        }

        public Student FindOne(int ID)
        {
            return repo.FindOne(ID);
        }

        public IEnumerable<Student> GetAll()
        {
            return repo.FindAll();
        }

        private int getMaxID()
        {
            int Max = 0;
            foreach (Student student in repo.FindAll())
            {
                if (student.ID > Max)
                    Max = student.ID;
            }
            return Max + 1;
        }
    }
}
