using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasysteem.Classes
{
    class RekeningController
    {
        private DatabaseDataContext db;

        //Constructor
        public RekeningController(DatabaseDataContext database)
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
        public List<Account> get_rekeningen()
        {
            return db.Accounts.ToList(); 
        }

        //Function to get object 
        public List<Type> get_typen()
        {
            return db.Types.ToList();
        }

        //Function to get object 
        public List<Customer> get_klanten()
        {
            return db.Customers.ToList();
        }
    
        //Function to create object 
        public bool add_KlantRekening(int sCustomer, decimal sBestedingslimiet, decimal sSaldo, string sStartdatum, int sTypeId)
        {
                //Creating object
                Account a = new Account();
                
                //Give object values
                a.bestedingslimiet = sBestedingslimiet;
                a.startdatum = sStartdatum;
                a.saldo = sSaldo;
                a.TypeId = sTypeId;

                //Insert object in queue
                db.Accounts.InsertOnSubmit(a);

                this.submitchanges();
               
                //Creating object
                CustomerAccount ca = new CustomerAccount();

                //Give object values
                ca.AccountId = a.AccountId;
                ca.CustomerId = sCustomer;

                //Insert object in queue
                db.CustomerAccounts.InsertOnSubmit(ca);

                return this.submitchanges();
        }


        //function to update object
        public bool update_KlantRekening(int aId , int sCustomer, decimal sBestedingslimiet, decimal sSaldo, string sStartdatum, int sTypeId) {
            //Get object
            Account a = (from b in db.Accounts
                          where b.AccountId == aId
                          select b).Single();

            //Give object new values
            a.bestedingslimiet = sBestedingslimiet;
            a.startdatum = sStartdatum;
            a.saldo = sSaldo;
            a.TypeId = sTypeId;

            //Get object
            CustomerAccount c = (from d in db.CustomerAccounts
                                  where d.AccountId == aId
                                  select d).Single();
            //Give object new values
            c.CustomerId = sCustomer;
            
            return this.submitchanges();

        }
        
        //Function to delete object
        public bool deleteRekening(Account KR)
        {
            //delete selected object
            db.Accounts.DeleteOnSubmit(KR);
            return this.submitchanges();
        }


        //Funtion to get ID from Customer
        public int get_customerId( int aId) {
            return Convert.ToInt32((from b in this.db.CustomerAccounts where b.AccountId == aId select b.CustomerId).Single());
        }

        //Funtion to get ID from Customer
        public string get_cbKlant(int id)
        {
            return (from b in this.db.Customers where b.CustomerId == id select b.voornaam).Single();
        }

        //Funtion to get ID from Customer
        public string get_cbSoort(int id)
        {
            return (from b in this.db.Types where b.TypeId == id select b.naam).Single();
        }
        
    }
}
