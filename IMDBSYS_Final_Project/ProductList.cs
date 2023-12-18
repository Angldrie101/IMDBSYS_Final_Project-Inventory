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
    public partial class ProductList : UserControl
    {
        private INVENTORYEntities4 db;
        public ProductList()
        {
            InitializeComponent();
            db = new INVENTORYEntities4();
        }
        public void LoadProductInfo()
        {
            dgvw_Products.DataSource = db.PRODUCTINFO.ToList();
        }
        private void dgvw_Products_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void ProductList_Load(object sender, EventArgs e)
        {
            //productlist();
        }

        private List<int> hiddenRows = new List<int>();

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();

            // Clear the list of hidden rows
            hiddenRows.Clear();

            // Iterate through the rows in the DataGridView
            for (int i = 0; i < dgvw_Products.Rows.Count; i++)
            {
                // Check if any cell in the row contains the search text
                bool rowContainsSearchText = dgvw_Products.Rows[i].Cells.Cast<DataGridViewCell>().Any(cell =>
                    cell.Value != null && cell.Value.ToString().ToLower().Contains(searchText));

                // Update the visibility of the row based on whether it contains the search text
                dgvw_Products.Rows[i].Visible = rowContainsSearchText;

                // If the row should be hidden, add its index to the list
                if (!rowContainsSearchText)
                {
                    hiddenRows.Add(i);
                }
            }

            // Update the currency manager's position to a valid index
            dgvw_Products.CurrentCell = null;

            // Handle the case where the currently selected row is hidden
            if (dgvw_Products.CurrentRow != null && !dgvw_Products.CurrentRow.Visible)
            {
                dgvw_Products.CurrentRow.Selected = false;
            }
        }
    }
}
