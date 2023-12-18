using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMDBSYS_Final_Project.Forms;
using IMDBSYS_Final_Project.Model;

namespace IMDBSYS_Final_Project.Forms
{
    public partial class Login_form : Form
    {
        UserRepository userRepo;

        public Login_form()
        {
            InitializeComponent();

            userRepo = new UserRepository();
        }

        private void cb_Showpass_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_Showpass.Checked)
            {
                txtpassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtpassword.UseSystemPasswordChar = true;
            }
        }

        private void lbl_signUp_Click(object sender, EventArgs e)
        {
            Signup_form form = new Signup_form();
            form.ShowDialog();
            this.Close();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtusername.Text))
            {
                ErrorProvider.SetError(txtusername, "Empty Field!");
                return;
            }
            if (String.IsNullOrEmpty(txtpassword.Text))
            {
                ErrorProvider.SetError(txtpassword, "Empty Field!");
                return;
            }

            var userLogged = userRepo.GetUserByUsername(txtusername.Text);

            if (userLogged != null)
            {
                if (userLogged.USERPASSWORD.Equals(txtpassword.Text))
                {
                    // Assigned to a singleton
                    GetSet.GetInstance().db_USERACCOUNT = userLogged;

                    switch ((Role)userLogged.ROLEID)
                    {
                        case Role.EMPLOYEE:
                            // Load student Dashboard
                            new Employee_form().Show();
                            this.Hide();
                            break;
                        case Role.MANAGER:
                            // Load Teacher Dashboard
                            new Manager_form().Show();
                            this.Hide();
                            break;
                        case Role.ADMIN:
                            // Load Admin Dashboard
                            new Admin_form().Show();
                            this.Hide();
                            break;
                        default:
                            MessageBox.Show("Sucessful log in!");
                            this.Hide();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Password");
                }
            }
            else
            {
                MessageBox.Show("Username not found");
            }
        }

        private void Login_form_Load(object sender, EventArgs e)
        {

        }

    }
}
