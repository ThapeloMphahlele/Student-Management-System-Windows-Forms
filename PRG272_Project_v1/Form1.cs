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
using System.Data.SqlClient;
using PRG272_Project.DataLayer;

namespace PRG272_Project
{
    public partial class Form1 : Form
    {
        DataHandler dh = new DataHandler();
        public Form1()
        {
            InitializeComponent();
            
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and password cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (dh.SignUp(username, password))
            {
                MessageBox.Show("Succesfully signed up.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtUsername.Clear();
                txtPassword.Clear();
            }

        }


        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and password cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (dh.Validate(username, password))
            {
                MessageBox.Show("Logged in successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                HomeForm homeForm = new HomeForm();
                homeForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
