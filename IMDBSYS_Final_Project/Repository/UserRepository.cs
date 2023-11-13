using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMDBSYS_Final_Project.AppData;

namespace IMDBSYS_Final_Project
{   
    public class UserRepository
    {
        private FINALPROJECTEntities1 db;

        public UserRepository()
        {
            db = new FINALPROJECTEntities1();
        }

        public ErrorCode NewUser(DB_LOGIN userAccount, ref String outMessage)
        {
            ErrorCode retValue = ErrorCode.Error;
            try
            {
                db.DB_LOGIN.Add(userAccount);
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

        public DB_LOGIN GetUserByUsername(String username)
        {
            // re-initialize db object because sometimes data in the list not updated
            using (db = new FINALPROJECTEntities1())
            {
                // SELECT TOP 1 * FROM USERACCOUNT WHERE userName == username
                return db.DB_LOGIN.Where(s => s.USERNAME == username).FirstOrDefault();
            }
        }
    }
}
