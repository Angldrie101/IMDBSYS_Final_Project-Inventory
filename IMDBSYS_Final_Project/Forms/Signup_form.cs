using IMDBSYS_Final_Project.AppData;
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
        INVENTORYEntities4 db;

        public Signup_form()
        {
            InitializeComponent();
            db = new INVENTORYEntities4();
        }

        private void lbl_login_Click_1(object sender, EventArgs e)
        {
            Login_form form = new Login_form();
            form.ShowDialog();
            this.Close();
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            //String cbResultSelected = cbBoxRole.SelectedValue.ToString();

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
            if (String.IsNullOrEmpty(txtConfirmPass.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtPassword, "Empty field");
                return;
            }

            if (!txtPassword.Text.Equals(txtConfirmPass.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtConfirmPass, "Password not match");
                return;
            }
            // int code = 123;
            // send email verificode (code)
            // send sms otp (code)

            // find the user id
            // code input equal db. useraccoutn code
            USERACCOUNT nUserAccount = new USERACCOUNT();
            nUserAccount.USERNAME = txtUsername.Text;
            nUserAccount.USERPASSWORD = txtPassword.Text;
            nUserAccount.ROLEID = (Int32)cbRole.SelectedValue;

            username = txtUsername.Text;

            db.USERACCOUNT.Add(nUserAccount);
            db.SaveChanges();

            txtPassword.Clear();
            txtConfirmPass.Clear();
            txtUsername.Clear();
            MessageBox.Show("Registered!");
            Login_form login = new Login_form();
            login.ShowDialog();
        }

        private void Signup_form_Load(object sender, EventArgs e)
        {
            loadboxRole();
        }

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void loadboxRole()
        {
            // SELECT * FROM ROLE
            var roles = db.Role.ToList();

            cbRole.ValueMember = "roleId";
            cbRole.DisplayMember = "roleName";
            cbRole.DataSource = roles;
        }
    }
}
