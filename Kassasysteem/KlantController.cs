using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasysteem.Classes
{
    class KlantController
    {
        private DatabaseDataContext db;

        //Constructor
        public KlantController(DatabaseDataContext database)
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
        public List<Customer> geefAlleKlanten()
        {
            return db.Customers.ToList();
        }

        //Function to create object 
        public bool opslaanCustomer(string sVoornaam, string sAchternaam, string sLeeftijd, string sWoonplaats, string sAdres, string sBsn,
            string dGeboortedatum, string sEmail, string sTelefoon)
        {
            //Check if request is valid
            if (sVoornaam != "" && sAchternaam != "" && sLeeftijd != "" && sWoonplaats != "" && sAdres != "" && sBsn != ""
            && dGeboortedatum != null && sEmail != "" && sTelefoon != "")
            {
                //Creating object
                Customer deKlant = new Customer();

                //Give object values
                deKlant.voornaam = sVoornaam;
                deKlant.achternaam = sAchternaam;
                deKlant.leeftijd = sLeeftijd;
                deKlant.woonplaats = sWoonplaats;
                deKlant.adres = sAdres;
                deKlant.bsn = sBsn;
                deKlant.geboortedatum = dGeboortedatum;
                deKlant.email = sEmail;
                deKlant.telefoon = sTelefoon;

                //Insert object in queue
                db.Customers.InsertOnSubmit(deKlant);

                return this.submitchanges();
            }
        
            return false;
        }
        //function to update object
        public bool update_KlantRekeningstring(int kid, string sVoornaam, string sAchternaam, string sLeeftijd, string sWoonplaats, string sAdres, string sBsn,
            string dGeboortedatum, string sEmail, string sTelefoon)
        {
            //Check if request is valid
            if (sVoornaam != "" && sAchternaam != "" && sLeeftijd != "" && sWoonplaats != "" && sAdres != "" && sBsn != ""
            && dGeboortedatum != null && sEmail != "" && sTelefoon != "")
            {
                //Give object 
                Customer deKlant = (from b in db.Customers
                                    where b.CustomerId == kid
                                    select b).Single();
                //Give object values
                deKlant.voornaam = sVoornaam;
                deKlant.achternaam = sAchternaam;
                deKlant.leeftijd = sLeeftijd;
                deKlant.woonplaats = sWoonplaats;
                deKlant.adres = sAdres;
                deKlant.bsn = sBsn;
                deKlant.geboortedatum = dGeboortedatum;
                deKlant.email = sEmail;
                deKlant.telefoon = sTelefoon;
            }

            return this.submitchanges();
        }

        //Function to delete object
        public bool deleteKlant(Customer deKlant)
        {
            //delete selected object
            db.Customers.DeleteOnSubmit(deKlant);
            return this.submitchanges();
        }
    }
}