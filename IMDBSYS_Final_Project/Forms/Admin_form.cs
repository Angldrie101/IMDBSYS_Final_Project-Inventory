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

namespace IMDBSYS_Final_Project.Forms
{
    public partial class Admin_form : Form
    {
        private INVENTORYEntities4 db;
        public Admin_form()
        {
            InitializeComponent();
            db = new INVENTORYEntities4();
        }

        private void Admin_form_Load(object sender, EventArgs e)
        {

        }

        private void btnAccounts_Click(object sender, EventArgs e)
        {
            loaduser();
        }
        private void loaduser()
        {
            dgvw_Display.DataSource = db.USERACCOUNT.ToList();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            loadproducts();
        }
        private void loadproducts()
        {
            dgvw_Display.DataSource = db.PRODUCTINFO.ToList();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            loadpurchase();
        }

        private void loadpurchase()
        {
            dgvw_Display.DataSource = db.PURCHASE.ToList();
        }

        private void dgvw_DisplayAccounts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
