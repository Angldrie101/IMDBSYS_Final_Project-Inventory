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
    public partial class Employee_form : Form
    {
        private INVENTORYEntities4 db;
        UserRepository userRepo;
        public string productname = String.Empty;
        public Employee_form()
        {
            InitializeComponent();
            db = new INVENTORYEntities4();
            
        }

        private void Employee_form_Load(object sender, EventArgs e)
        {
            userRepo = new UserRepository();
            loadUser();
            UpdateTotalPrice();
        }
        private void loadUser()
        {
            dgvw_DisplayProduct.DataSource = db.PURCHASE.ToList();
            dgv_Products.DataSource = db.PRODUCTINFO.ToList();
        }

        private void dgv_Products_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dgv_Products.Rows[e.RowIndex];

                txtProductCode.Text = selectedRow.Cells["PRODUCTCODE"].Value.ToString();
                txtProductName.Text = selectedRow.Cells["PRODUCTNAME"].Value.ToString();
                txtProductQnty.Text = selectedRow.Cells["PRODUCTQNTY"].Value.ToString();
                txtPPrice.Text = selectedRow.Cells["PRODUCTPRICE"].Value.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception : {ex.Message}");
            }
        }
        private void dgvw_DisplayProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dgvw_DisplayProduct.Rows[e.RowIndex];

                txtProductCode.Text = selectedRow.Cells["PRODUCTCODE"].Value.ToString();
                txtProductName.Text = selectedRow.Cells["PRODUCTNAME"].Value.ToString();
                txtProductQnty.Text = selectedRow.Cells["PRODUCTQNTY"].Value.ToString();
                txtPPrice.Text = selectedRow.Cells["PRODUCTPRICE"].Value.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception : {ex.Message}");
            }
        }

        private List<(string, string, string, int)> purchasedProducts = new List<(string, string, string, int)>();

        private void btnEnter_Click(object sender, EventArgs e)
        {
            String pcode = Convert.ToString(txtProductCode.Text);
            String pname = txtProductName.Text;
            String pqnty = txtProductQnty.Text;
            String pprice = txtPPrice.Text;

            String strOutputMsg = "";

            ErrorCode retValue = userRepo.CreateUserUsingStoredProf(txtProductCode.Text, txtProductName.Text, txtProductQnty.Text, txtPPrice.Text, ref strOutputMsg);
            if (String.IsNullOrEmpty(pcode))
            {
                errorProvider.SetError(txtProductCode, "Empty Field!");
                return;
            }

            int productCode = Convert.ToInt32(pcode);
            PRODUCTINFO productInfo = GetProductInfo(productCode);

            if (productInfo == null)
            {
                MessageBox.Show("Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtProductName.Text = productInfo.PRODUCTNAME;
            txtPPrice.Text = productInfo.PRODUCTPRICE.ToString();

            if (String.IsNullOrEmpty(pqnty))
            {
                errorProvider.SetError(txtProductQnty, "Empty Field!");
                return;
            }
            if (String.IsNullOrEmpty(pprice))
            {
                errorProvider.SetError(txtPPrice, "Empty Field!");
                return;
            }

            int currentQuantity = productInfo.PRODUCTQNTY;
            int purchasedQuantity = Convert.ToInt32(pqnty);

            if (currentQuantity >= purchasedQuantity)
            {
                int newQuantity = currentQuantity - purchasedQuantity;

                dgv_Products.DataSource = (pcode, pname, pqnty, newQuantity);

                UpdateProductQuantity(productCode, newQuantity);

                MessageBox.Show($"Product purchased successfully! New quantity: {newQuantity}", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PURCHASE purchase = new PURCHASE();
                purchase.PRODUCTNAME = txtProductName.Text;
                int qnty = Convert.ToInt32(txtProductQnty.Text);
                purchase.PRODUCTQNTY = qnty;
                purchase.PRODUCTPRICE = txtPPrice.Text;

                productname = txtProductName.Text;

                loadUser();
                db.PURCHASE.Add(purchase);
                db.SaveChanges();

                List<PRODUCTINFO> dataSource = (List<PRODUCTINFO>)dgv_Products.DataSource;

                // Find the product in the list
                PRODUCTINFO productToUpdate = dataSource.FirstOrDefault(p => p.PRODUCTCODE == productCode);

                if (productToUpdate != null)
                {
                    // Update the quantity in the List
                    productToUpdate.PRODUCTQNTY = newQuantity;

                    // Refresh the DataGridView
                    dgv_Products.Refresh();
                }

                txtProductCode.Clear();
                txtProductName.Clear();
                txtProductQnty.Clear();
                txtPPrice.Clear();
            }
            else
            {
                MessageBox.Show(strOutputMsg, "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
        private void btnEdit_Click(object sender, EventArgs e)
        {
            String pcode = txtProductCode.Text;
            String pname = txtProductName.Text;
            String pqnty = txtProductQnty.Text;
            String pprice = txtPPrice.Text;

            String strOutputMsg = "";
            // Validation not allow empty or null value
            if (String.IsNullOrEmpty(pname))
            {
                errorProvider.SetError(txtProductName, "Empty Field!");
                return;
            }
            else
            // Validation not allow empty or null value
            if (String.IsNullOrEmpty(pqnty))
            {
                errorProvider.SetError(txtProductQnty, "Empty Field!");
                return;
            }
            // Validation not allow empty or null value
            if (String.IsNullOrEmpty(pprice))
            {
                errorProvider.SetError(txtPPrice, "Empty Field!");
                return;
            }
            int code = Convert.ToInt32(txtProductCode.Text);
            var productinfo = userRepo.GetProductInfo(pcode);

            ErrorCode retValue = userRepo.UpdateUser(code, pname, pqnty, pprice, productinfo, ref strOutputMsg);
            if (retValue == ErrorCode.Success)
            {
                //Clear the errors
                errorProvider.Clear();
                MessageBox.Show(strOutputMsg, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadUser();
                //reset the selected id
                pcode = null;

                txtProductCode.Clear();
                txtProductName.Clear();
                txtProductQnty.Clear();
                txtPPrice.Clear();
            }
            else
            {
                // error 
                MessageBox.Show(strOutputMsg, "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            String pname = txtProductName.Text;

            String strOutputMsg = "";
            // Check if the product name is empty or null
            if (string.IsNullOrEmpty(txtProductName.Text))
            {
                MessageBox.Show("No Product Selected", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int code = Convert.ToInt32(txtProductCode.Text);

            // Assuming your RemoveUser method needs some parameters, pass the necessary values
            ErrorCode retValue = userRepo.RemoveUser(code, pname, txtProductQnty.Text, txtPPrice.Text, ref strOutputMsg);

            if (retValue == ErrorCode.Success)
            {
                // Clear the errors
                errorProvider.Clear();

                MessageBox.Show(strOutputMsg, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Increment the product code (assuming it's an integer)
                int newCode = code + 1;

                // Update the UI with the new product code
                txtProductCode.Text = newCode.ToString();

                loadUser();

                txtProductName.Clear();
                txtProductQnty.Clear();
                txtPPrice.Clear();
            }
            else
            {
                // Handle the error
                MessageBox.Show(strOutputMsg, "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtProductQnty_TextChanged(object sender, EventArgs e)
        {
            UpdateTotalPrice();
            UpdateTotalAmount();
        }

        private void txtPPrice_TextChanged(object sender, EventArgs e)
        {
            UpdateTotalPrice();
        }

        private void UpdateTotalAmount()
        {
            if (int.TryParse(txtProductQnty.Text, out int quantity) && int.TryParse(txtProductCode.Text, out int productCode))
            {
                // Fetch product details from the database based on the entered product code
                PRODUCTINFO productInfo = GetProductInfo(productCode);

                if (productInfo != null && double.TryParse(productInfo.PRODUCTPRICE, out double price))
                {
                    double totalAmount = quantity * price;
                    txtPPrice.Text = totalAmount.ToString("0.00");

                    dgvw_DisplayProduct.DataSource = (productCode, productInfo.PRODUCTNAME, quantity, totalAmount);
                }

                else
                {
                    txtPPrice.Text = "0.00";
                }
            }
            else
            {
                txtPPrice.Text = "0.00";
            }
        }

        private void UpdateTotalPrice()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvw_DisplayProduct.Rows)
            {
                if (row.Cells["PRODUCTQNTY"].Value != null &&
                    row.Cells["PRODUCTPRICE"].Value != null)
                {
                    if (int.TryParse(row.Cells["PRODUCTQNTY"].Value.ToString(), out int quantity) &&
                        decimal.TryParse(row.Cells["PRODUCTPRICE"].Value.ToString(), out decimal price))
                    {
                        total += quantity * price;
                    }
                    else
                    {
                        // Handle parsing errors if necessary
                        Console.WriteLine("Invalid quantity or price format");
                    }
                }
            }

            lblTotalPrice.Text = $"{total:C}";
        }

        private void txtProductCode_TextChanged(object sender, EventArgs e)
        {
            string pcode = txtProductCode.Text;
            string pqnty = txtProductQnty.Text;

            if (string.IsNullOrEmpty(pcode))
            {
                errorProvider.SetError(txtProductCode, "Empty Field!");
                return;
            }

            int productCode = Convert.ToInt32(pcode);
            PRODUCTINFO productInfo = GetProductInfo(productCode);

            if (productInfo == null)
            {
                MessageBox.Show("Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Populate textboxes with product details
            txtProductName.Text = productInfo.PRODUCTNAME;
            txtPPrice.Text = productInfo.PRODUCTPRICE.ToString();
        }

    }
}
