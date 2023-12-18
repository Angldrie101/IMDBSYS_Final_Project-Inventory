using IMDBSYS_Final_Project.AppData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMDBSYS_Final_Project
{
    public partial class Products : UserControl
    {
        private INVENTORYEntities4 db;
        public string productname = String.Empty;
        public Products()
        {
            InitializeComponent();
            db = new INVENTORYEntities4();
        }

        public void AvailableProd()
        {
            dgvw_AddSupply.DataSource = db.PRODUCTINFO.ToList();
        }

        private void Products_Load(object sender, EventArgs e)
        {

        }

        private void btnAddSupply_Click(object sender, EventArgs e)
        {
            String pcode = txtProductCode.Text;
            String pname = txtProductName.Text;
            String qnty = txtProductQnty.Text;
            String price = txtProductPrice.Text;

            String strOutputMsg = "";
            // Validation not allow empty or null value
            if (String.IsNullOrEmpty(pcode))
            {
                errorProvider.SetError(txtProductCode, "Empty Field!");
                return;
            }
            else
            // Validation not allow empty or null value
            if (String.IsNullOrEmpty(pname))
            {
                errorProvider.SetError(txtProductName, "Empty Field!");
                return;
            }

            int productCode = Convert.ToInt32(pcode);
            PRODUCTINFO productInfo = GetProductInfo(productCode);

            txtProductName.Text = productInfo.PRODUCTNAME;
            txtProductPrice.Text = productInfo.PRODUCTPRICE.ToString();

            // Validation not allow empty or null value
            if (String.IsNullOrEmpty(qnty))
            {
                errorProvider.SetError(txtProductQnty, "Empty Field!");
                return;
            }

            // Validation not allow empty or null value
            if (String.IsNullOrEmpty(price))
            {
                errorProvider.SetError(txtProductPrice, "Empty Field!");
                return;
            }

            // Update the product quantity in the database
            int currentQuantity = productInfo.PRODUCTQNTY;
            int purchasedQuantity = Convert.ToInt32(qnty);

            if (currentQuantity >= purchasedQuantity || currentQuantity == 0)
            {
                int newQuantity = currentQuantity + purchasedQuantity;

                dgvw_AddSupply.DataSource = (pcode, pname, qnty, newQuantity);


                // Update the product quantity in the database
                UpdateProductQuantity(productCode, newQuantity);

                MessageBox.Show($"Product purchased successfully! New quantity: {newQuantity}", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PRODUCTINFO supply = new PRODUCTINFO();
                int code = Convert.ToInt32(txtProductCode.Text);
                supply.PRODUCTCODE = code;
                supply.PRODUCTNAME = txtProductName.Text;
                int pqnty = Convert.ToInt32(txtProductQnty.Text);
                supply.PRODUCTQNTY = pqnty;
                supply.PRODUCTPRICE = txtProductPrice.Text;

                productname = txtProductName.Text;

                AvailableProd();
                db.PRODUCTINFO.Add(supply);
                //db.SaveChanges();
                List<PRODUCTINFO> dataSource = (List<PRODUCTINFO>)dgvw_AddSupply.DataSource;

                // Find the product in the list
                PRODUCTINFO productToUpdate = dataSource.FirstOrDefault(p => p.PRODUCTCODE == productCode);

                if (productToUpdate != null)
                {
                    // Update the quantity in the List
                    productToUpdate.PRODUCTQNTY = newQuantity;
                }

                // Refresh the DataGridView
                dgvw_AddSupply.Refresh();

                txtProductCode.Clear();
                txtProductName.Clear();
                txtProductQnty.Clear();
                txtProductPrice.Clear();

            }
        }

        private PRODUCTINFO GetProductInfo(int productCode)
        {
            using (var db = new INVENTORYEntities4())
            {
                return db.PRODUCTINFO.SingleOrDefault(p => p.PRODUCTCODE == productCode);
            }
        }

        private void UpdateProductQuantity(int productCode, int newQuantity)
        {
            using (var db = new INVENTORYEntities4())
            {
                PRODUCTINFO product = db.PRODUCTINFO.SingleOrDefault(p => p.PRODUCTCODE == productCode);

                if (product != null)
                {
                    product.PRODUCTQNTY = newQuantity;
                    db.SaveChanges();
                }
            }
        }

        private void txtProductCode_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProductCode.Text.Trim()))
            {
                if (int.TryParse(txtProductCode.Text.Trim(), out int enteredProductCode))
                {
                    PRODUCTINFO product = db.PRODUCTINFO.FirstOrDefault(p => p.PRODUCTCODE == enteredProductCode);

                    if (product != null)
                    {
                        txtProductName.Text = product.PRODUCTNAME;
                        txtProductPrice.Text = product.PRODUCTPRICE.ToString();

                        AvailableProd();
                    }
                    else
                    {
                        txtProductName.Text = "";
                        txtProductQnty.Text = "";
                        txtProductPrice.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid product code (numeric).");
                    txtProductName.Text = "";
                    txtProductQnty.Text = "";
                    txtProductPrice.Text = "";
                }
            }
            else
            {
                txtProductName.Text = "";
                txtProductQnty.Text = "";
                txtProductPrice.Text = "";
            }
        }
    }
}
