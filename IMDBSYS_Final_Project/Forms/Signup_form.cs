using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace IMDBSYS_Final_Project.Forms
{
    public partial class Signup_form : Form
    {
        public string username = String.Empty;

        public Signup_form()
        {
            InitializeComponent();
        }

        private void lbl_login_Click_1(object sender, EventArgs e)
        {
            Login_form form = new Login_form();
            form.ShowDialog();
        }

        private void cb_Showpass_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_Showpass.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtUsername.Text))
            {
                errorProvider1.SetError(txtUsername, "Empty field");
                return;
            }
            if (String.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtPassword, "Empty field");
                return;
            }
           
        }
    }
}
