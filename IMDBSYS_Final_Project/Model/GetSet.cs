using IMDBSYS_Final_Project.AppData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBSYS_Final_Project.Model
{
    internal class GetSet
    {
        private static GetSet _instance;


        // *********** Member 
        public USERACCOUNT db_USERACCOUNT { get; set; }

       // public PRODUCTINFO db_PRODUCTINFO { get; set; }

        //************
        private GetSet()
        {

        }

        public static GetSet GetInstance()
        {
            if (_instance == null)
                _instance = new GetSet();
            return _instance;
        }
    }
}
