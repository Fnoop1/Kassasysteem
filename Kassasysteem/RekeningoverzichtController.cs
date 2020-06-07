using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasysteem
{
    class RekeningoverzichtController
    {
        private DatabaseDataContext db;

        //Constructor
        public RekeningoverzichtController(DatabaseDataContext database)
        {
            this.db = database;
        }

        //function for submitchanges
        private bool submitchanges()
        {
            try
            {
                //try submitchanges
                this.db.SubmitChanges();

                //gives true(no Exception)
                return true;
            }
            catch (Exception)
            {
                //give false(Exception)
                return false;
            }
        }


        //Function to get object 
        public List<CustomerAccount> get_rekeningen_klanten()
        {
            return db.CustomerAccounts.ToList();
        }
        
    }
}
