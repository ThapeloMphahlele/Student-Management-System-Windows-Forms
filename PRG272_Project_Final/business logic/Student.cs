using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG272_Project.business_logic
{
    internal class Student
    {
        public string StudentID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Course { get; set; }

        public Student()
        { }
        public Student(string studentid, string name, int age, string course)
        {
            this.StudentID = studentid;
            this.Name = name;
            this.Age = age;
            this.Course = course;

        }
        public override string ToString()
        {
            return "StudentID:" + StudentID + "Name: " + Name + "Age" + Age + "Course " + Course;
        }
    }
}
