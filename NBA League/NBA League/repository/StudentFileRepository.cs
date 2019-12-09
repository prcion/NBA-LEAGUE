using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBA_League.domain;
using NBA_League.validation;

namespace NBA_League.repository
{
    class StudentFileRepository : AbstractRepository<int, Student>
    {
        string fileName;
        public StudentFileRepository(IValidator<Student> validator, string fileName) : base(validator)
        {
            this.fileName = fileName;
            LoadFromFile();
        }

        private void LoadFromFile()
        {
            using (TextReader read = File.OpenText(fileName))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    string[] values = line.Split(';');
                    if (values.Length == 3)
                    {
                        int id;
                        bool ok = int.TryParse(values[0], out id);
                        if (ok)
                        {
                            Student student = new Student(id, values[1], values[2]);
                            base.Save(student);
                        }
                    }
                }
            }
        }

        private void WriteToFile()
        {
            using (TextWriter writer = File.CreateText(fileName))
            {
                foreach (Student student in FindAll())
                {
                    writer.WriteLine("{0};{1};{2}", student.ID, student.name, student.school);
                }
            }
        }

        public override Student Save(Student e)
        {
            Student team = base.Save(e);
            WriteToFile();
            return team;
        }

        public override Student Update(Student e)
        {
            Student team = base.Update(e);
            WriteToFile();
            return team;
        }

        public override Student Delete(int id)
        {
            Student team = base.Delete(id);
            WriteToFile();
            return team;
        }
    }
}
