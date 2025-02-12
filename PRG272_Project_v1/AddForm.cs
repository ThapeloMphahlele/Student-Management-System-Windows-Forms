using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PRG272_Project
{
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string ID = txtID.Text;
            string name = txtName.Text;
            string ageText = txtAge.Text;
            string course = txtCourse.Text;

            if (string.IsNullOrEmpty(ID) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(ageText) || string.IsNullOrEmpty(course))
            {
                MessageBox.Show("All fields are required to be filled.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (!int.TryParse(ageText, out int age) || age <= 0)
            {
                MessageBox.Show("Please eneter a valid age.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AddStudent(ID, name, age, course);
        }

        private void AddStudent(string ID, string name, int age, string course)
        {
            if (!File.Exists("students.txt") || new FileInfo("students.txt").Length == 0)
            {
                using (StreamWriter writer = new StreamWriter("students.txt", true))
                {
                    writer.WriteLine($"{"StudentID:",-10} | {"Name:",-20} | {"Age:",-5} | {"Course:",-20}");
                    writer.WriteLine(new string('-', 60));

                }

            }

            try
            {
                using (StreamWriter writer = new StreamWriter("students.txt", true))
                {
                    writer.WriteLine($"{ID,-10} | {name,-20} | {age,-5} | {course,-20}");
                }

                MessageBox.Show($"{name} added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult result = MessageBox.Show("Would you like to add another student?", "Add another?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    txtID.Clear();
                    txtName.Clear();
                    txtAge.Clear();
                    txtCourse.Clear();
                    txtID.Focus();

                }
                else
                {
                    this.Close();
                }

            }
            catch (Exception err)
            {
                MessageBox.Show($"An error occurred: {err.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
