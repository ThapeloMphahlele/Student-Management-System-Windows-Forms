using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PRG272_Project.business_logic;
using PRG272_Project.data_layer;

namespace PRG272_Project
{
    public partial class Form1 : Form
    {
        private Datahandler handler = new Datahandler(); //object to access the Class Datahandler
        BindingSource src; // creating a binding source
        string StudentNum = "";
        
        private ValidateDetails valid = new ValidateDetails(); // object to access the Class ValidateDetails 

        public Form1()
        {
            InitializeComponent();
            LoadData();
            // Adding items to ComboBox
            CmbCourse.Items.Add("DiT");
            CmbCourse.Items.Add("BiT");
            CmbCourse.Items.Add("Bcom");
        }

        private void LoadData()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = handler.GetData();
            
        }


        private void btn_Add_Student_Click(object sender, EventArgs e)
        {

            try
            {
                // Validate age input
                if (!int.TryParse(Age_textBox.Text, out int age))
                {
                    MessageBox.Show("Please enter a valid number for age.");
                    return;
                }

                // Validate course selection
                if (CmbCourse.SelectedItem == null)
                {
                    MessageBox.Show("Please select a course.");
                    return;
                }

                // Create the new student object
                var student = new Student
                {
                    StudentID = StudentID_textBox.Text,
                    Name = Name_textBox.Text,
                    Age = age,
                    Course = CmbCourse.SelectedItem.ToString()
                };


                // Use Datahandler to add the student to the file
                handler.AddNewStudent(student);

                ClearInputs();
                LoadData();

                // Notify success and clear fields
                MessageBox.Show("Student added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding the student: " + ex.Message);
            }
        }

        private void Update_Student_Details_Click(object sender, EventArgs e)
        {
            // Ensure that all input fields are filled
            if (!valid.isEmpty(StudentID_textBox.Text, Name_textBox.Text, Age_textBox.Text, CmbCourse.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            try
            {
                // Parse age with validation
                if (!int.TryParse(Age_textBox.Text, out int age))
                {
                    MessageBox.Show("Please enter a valid age.");
                    return;
                }

                // Ensure a course is selected
                if (CmbCourse.SelectedItem == null)
                {
                    MessageBox.Show("Please select a course.");
                    return;
                }

                // Create updated student object
                var updatedStudent = new Student(StudentID_textBox.Text, Name_textBox.Text, age, CmbCourse.SelectedItem.ToString());

                // Update the student using the handler
                handler.UpdateStudent(updatedStudent);

                // Ensure the file is updated as well
                handler.UpdateStudent(updatedStudent); 

                // Provide feedback, clear input fields, and refresh data display
                ClearInputs();
                LoadData();
                MessageBox.Show("Student details updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void btn_Delete_Student_Click(object sender, EventArgs e)
        {
           
        }

        private void btn_View_All_Students_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                StudentID_textBox.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                Name_textBox.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                Age_textBox.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                CmbCourse.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }


        }

        private void ClearInputs()
        {
            StudentID_textBox.Clear();
            Name_textBox.Clear();
            Age_textBox.Clear();
            CmbCourse.SelectedIndex = -1;
        }

        private void CmbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Delete_student_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                string studentId = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                handler.DeleteStudent(studentId);
                LoadData();
                MessageBox.Show("Student deleted successfully.");
            }
            else
            {
                MessageBox.Show("Please select a student to delete.");
            }


        }

        private void Search_by_studentID_Click(object sender, EventArgs e)
        {
            // Get the StudentID from the TextBox input
            string studentID = Search_TextBox.Text.Trim();

            // Check if the StudentID is not empty
            if (string.IsNullOrEmpty(studentID))
            {
                MessageBox.Show("Please enter a Student ID to search.");
                return;
            }

            // Get the list of all students from the file
            var students = handler.GetData();

            // Search for the student with the given StudentID
            var student = students.FirstOrDefault(s => s.StudentID == studentID);

            // Check if the student is found
            if (student != null)
            {
                // Display the student's details in corresponding fields
                StudentID_textBox.Text = student.StudentID;
                Name_textBox.Text = student.Name;
                Age_textBox.Text = student.Age.ToString();
                CmbCourse.SelectedItem = student.Course;

                // display a message confirming the student was found
                MessageBox.Show("Student found!");
            }
            else
            {
                // If the student was not found, display an error message
                MessageBox.Show("Student with the given ID not found.");
            }
        }

        private void btn_All_Students_Click(object sender, EventArgs e)
        {
            var students = handler.GetData();
            dataGridView1.DataSource = students;
            MessageBox.Show("Displaying All students");
        }

        private void Search_TextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Summary_Click(object sender, EventArgs e)
        {
            // Retrieve all students from the file
            var students = handler.GetData();

            // Calculate total number of students
            int totalStudents = students.Count;

            // Calculate the average age of students (if there are any students)
            double averageAge = totalStudents > 0 ? students.Average(s => s.Age) : 0;

            // Display the total number of students and the average age on the form
            MessageBox.Show( $"Summary Report\n\nTotal Students: {totalStudents}\nAverage Age: {averageAge:F2}");
            

            // Save the summary to summary.txt file
            handler.SaveSummaryToFile(totalStudents, averageAge);

            // show a message indicating the report is generated
            MessageBox.Show("Summary report generated successfully.");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Form Closing.....Goodbye!!");
            Environment.Exit(0);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
