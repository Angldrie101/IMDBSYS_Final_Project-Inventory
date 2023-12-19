using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMDBSYS_Final_Project.AppData;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace IMDBSYS_Final_Project
{   
    public class UserRepository
    {
        private INVENTORYEntities4 db;

        public UserRepository()
        {
            db = new INVENTORYEntities4();
        }

        public ErrorCode NewUser(PURCHASE aproductinfo, ref String outMessage)
        {
            ErrorCode retValue = ErrorCode.Error;
            try
            {
                db.PURCHASE.Add(aproductinfo);
                db.SaveChanges();

                outMessage = "Inserted";
                retValue = ErrorCode.Success;
            }
            catch (Exception ex)
            {
                outMessage = ex.Message;
                MessageBox.Show(ex.Message);
            }
            return retValue;
        }

        public ErrorCode UpdateUser(int? productcode, String productname, String productqnty, String productprice, PURCHASE ductinfo, ref String outMessage)
        {
            ErrorCode retValue = ErrorCode.Error;
            try
            {
                // Find the user with id
                PURCHASE product = db.PURCHASE.Where(m => m.PRODUCTCODE == productcode).FirstOrDefault();
                // Update the value of the retrieved user
                product.PRODUCTCODE = ductinfo.PRODUCTCODE;
                product.PRODUCTNAME = ductinfo.PRODUCTNAME;
                product.PRODUCTQNTY = ductinfo.PRODUCTQNTY;
                product.PRODUCTPRICE = ductinfo.PRODUCTPRICE;

                db.SaveChanges();       // Execute the update

                outMessage = "Updated";
                retValue = ErrorCode.Success;
            }
            catch (Exception ex)
            {
                outMessage = ex.Message;
                retValue = ErrorCode.Success;
                MessageBox.Show(ex.Message);
            }
            return retValue;

        }

        public ErrorCode RemoveUser(int productcode, String productname, String productqnty, String productprice, ref String outMessage)
        {
            ErrorCode retValue = ErrorCode.Error;
            try
            {
                PURCHASE product = db.PURCHASE.Where(m => m.PRODUCTCODE == productcode).FirstOrDefault();
                // Remove the user
                db.PURCHASE.Remove(product);
                db.SaveChanges();       // Execute the update

                outMessage = "Deleted";
                retValue = ErrorCode.Success;
            }
            catch (Exception ex)
            {
                outMessage = ex.Message;
                retValue = ErrorCode.Error;
                MessageBox.Show(ex.Message);
            }
            return retValue;
        }

        public ErrorCode CreateUserUsingStoredProf(String productcode, String productname, String productqnty, String productprice, ref String szResponse)
        {
            try
            {
                using (db = new INVENTORYEntities4())
                {
                    // Call the create stored procedure
                    //
                    db.sp_ProductInfo();
                    szResponse = "Insufficient stock for the selected product.";
                    return ErrorCode.Success;
                }
            }
            catch (Exception ex)
            {
                szResponse = ex.Message;
                return ErrorCode.Error;
            }
        }

        public USERACCOUNT GetUserByUsername(String username)
        {
            // re-initialize db object because sometimes data in the list not updated
            using (db = new INVENTORYEntities4())
            {
                // SELECT TOP 1 * FROM USERACCOUNT WHERE userName == username
                return db.USERACCOUNT.Where(s => s.USERNAME == username).FirstOrDefault();
            }
        }

        public PURCHASE GetProductInfo(String pname)
        {
            using (db = new INVENTORYEntities4())
            {
                // SELECT TOP 1 * FROM USERACCOUNT WHERE userName == username
                return db.PURCHASE.Where(s => s.PRODUCTNAME == pname).FirstOrDefault();
            }
        }

        public List<PRODUCTINFO> ProductInfo()
        {
            using (db = new INVENTORYEntities4())
            {
                return db.PRODUCTINFO.ToList();
            }
        }

        public List<vw_all_product_info> AllProductInfo()
        {
            using (db = new INVENTORYEntities4())
            {
                return db.vw_all_product_info.ToList();
            }
        }
    }
}
