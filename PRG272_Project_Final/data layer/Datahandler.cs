using PRG272_Project.business_logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG272_Project.data_layer
{
    internal class Datahandler
    {
        private readonly string TextFileName = @"Students.txt";
        private List<Student> StudentsList = new List<Student>();

        public Datahandler()
        {
            EnsureFileExists();
        }

        // Method to ensure the file exists
        private void EnsureFileExists()
        {
            if (!File.Exists(TextFileName))
            {
                File.Create(TextFileName).Dispose();
            }
        }

        // Method to add a new student
        public void AddNewStudent(Student student)
        {
            using (StreamWriter sw = new StreamWriter(TextFileName, true))
            {
                sw.WriteLine($"{student.StudentID},{student.Name},{student.Age},{student.Course}");
            }
        }

        // Method to retrieve all students from file
        public List<Student> GetData()
        {
            StudentsList.Clear();
            EnsureFileExists();

            using (StreamReader sr = new StreamReader(TextFileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');
                    if (fields.Length == 4)
                    {
                        StudentsList.Add(new Student(fields[0], fields[1], int.Parse(fields[2]), fields[3]));
                    }
                }
            }

            return StudentsList;
        }

        // Method to update a student’s data in the file
        public void UpdateStudent(Student updatedStudent)
        {
            StudentsList = GetData();
            var student = StudentsList.FirstOrDefault(s => s.StudentID == updatedStudent.StudentID);

            if (student != null)
            {
                student.Name = updatedStudent.Name;
                student.Age = updatedStudent.Age;
                student.Course = updatedStudent.Course;
                WriteToFile();
            }
        }

        // Method to delete a student by ID
        public void DeleteStudent(string studentId)
        {
            StudentsList = GetData();
            StudentsList = StudentsList.Where(s => s.StudentID != studentId).ToList();
            WriteToFile();
        }

        // Private method to write the list of students to file
        private void WriteToFile()
        {
            EnsureFileExists();

            using (StreamWriter sw = new StreamWriter(TextFileName, false))
            {
                foreach (var student in StudentsList)
                {
                    sw.WriteLine($"{student.StudentID},{student.Name},{student.Age},{student.Course}");
                }
            }
        }

        public void SaveSummaryToFile(int totalStudents, double averageAge)
        {
            // Define the file path for summary.txt
            string summaryFilePath = "summary.txt";

            // Prepare the content to be written to the file
            string summaryContent = $"Summary Report\n\nTotal Students: {totalStudents}\nAverage Age: {averageAge:F2}";

            // Write the summary content to the file
            File.WriteAllText(summaryFilePath, summaryContent);
        }
    }
}
