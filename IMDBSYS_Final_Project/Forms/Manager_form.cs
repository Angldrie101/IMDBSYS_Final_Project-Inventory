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
    public partial class Manager_form : Form
    {
        private INVENTORYEntities4 db;
        public Manager_form()
        {
            InitializeComponent();
            db = new INVENTORYEntities4();
        }

        //public event Action<string, string, string, string> NewProductAdded;


        private void dtgv_Products_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Manager_form_Load(object sender, EventArgs e)
        {
            ProductList prod = new ProductList();
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(prod);
            prod.LoadProductInfo();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            ProductList list = new ProductList();
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(list);
            list.LoadProductInfo();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            Purchase prchse = new Purchase();
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(prchse);
            prchse.PurchaseInfo();
        }

        private void btnAddSupply_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            Products product = new Products();
            mainPanel.Controls.Add(product);
            product.AvailableProd();
        }
    }
}
