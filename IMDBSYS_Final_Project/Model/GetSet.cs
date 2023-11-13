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
        public AppData.DB_LOGIN DB_LOGIN { get; set; }


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
