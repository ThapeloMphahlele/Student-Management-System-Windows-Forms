using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG272_Project.business_logic
{
    internal class ValidateDetails
    {
        public bool isEmpty(string studentId, string studentName, string studentAge, string course)
        {
            bool valid = true;

            if (string.IsNullOrWhiteSpace(studentId))
            {
                valid = false;
                MessageBox.Show("Please fill in Student ID");
            }

            if (string.IsNullOrWhiteSpace(studentName))
            {
                valid = false;
                MessageBox.Show("Please fill in Student Name");
            }

            if (!int.TryParse(studentAge, out int age) || age <= 0)
            {
                valid = false;
                MessageBox.Show("Please enter a valid numeric age.");
            }

            if (string.IsNullOrWhiteSpace(course))
            {
                valid = false;
                MessageBox.Show("Please fill in Course");
            }

            return valid;
        }
    }
}
