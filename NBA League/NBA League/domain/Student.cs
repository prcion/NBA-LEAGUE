using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_League.domain
{
    class Student:IEntity<int>
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string school { get; set; }

        public Student(int ID, string name, string school)
        {
            this.ID = ID;
            this.name = name;
            this.school = school;
        }

        override public bool Equals(Object obj)
        {
            if (!(obj is Student))
                return false;
            Student objCopy = obj as Student;
            if (objCopy.ID == this.ID)
                return true;
            return false;
        }

        override public string ToString()
        {
            return ID + " " + name + " " + school;
        }

        override public int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public static bool operator ==(Student teamOne, Student teamTwo)
        {
            return teamOne.Equals(teamTwo);
        }

        public static bool operator !=(Student teamOne, Student teamTwo)
        {
            return !teamOne.Equals(teamTwo);
        }
    }
}
