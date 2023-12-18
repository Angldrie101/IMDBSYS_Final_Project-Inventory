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

namespace IMDBSYS_Final_Project
{
    public partial class Purchase : UserControl
    {
        private INVENTORYEntities4 db;
        public Purchase()
        {
            InitializeComponent();
            db = new INVENTORYEntities4();
        }

        public void PurchaseInfo()
        {
            dgvw_Purchase.DataSource= db.PURCHASE.ToList();
            UpdateTotalPrice();
        }

        private void Purchase_Load(object sender, EventArgs e)
        {
            UpdateTotalPrice();
        }

        private List<int> hiddenRows = new List<int>();

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();

            // Clear the list of hidden rows
            hiddenRows.Clear();

            // Iterate through the rows in the DataGridView
            for (int i = 0; i < dgvw_Purchase.Rows.Count; i++)
            {
                // Check if any cell in the row contains the search text
                bool rowContainsSearchText = dgvw_Purchase.Rows[i].Cells.Cast<DataGridViewCell>().Any(cell =>
                    cell.Value != null && cell.Value.ToString().ToLower().Contains(searchText));

                // Update the visibility of the row based on whether it contains the search text
                dgvw_Purchase.Rows[i].Visible = rowContainsSearchText;

                // If the row should be hidden, add its index to the list
                if (!rowContainsSearchText)
                {
                    hiddenRows.Add(i);
                }
            }

            // Update the currency manager's position to a valid index
            dgvw_Purchase.CurrentCell = null;

            // Handle the case where the currently selected row is hidden
            if (dgvw_Purchase.CurrentRow != null && !dgvw_Purchase.CurrentRow.Visible)
            {
                dgvw_Purchase.CurrentRow.Selected = false;
            }
        }
        private void UpdateTotalPrice()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvw_Purchase.Rows)
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

        private void lblTotalPrice_Click(object sender, EventArgs e)
        {

        }
    }
}
