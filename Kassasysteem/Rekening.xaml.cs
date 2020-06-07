using Kassasysteem.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kassasysteem
{
    /// <summary>
    /// Interaction logic for Rekening.xaml
    /// </summary>
    public partial class Rekening : Window
    {
        private RekeningController r;
        private DatabaseDataContext db;

        public Rekening()
        {
            InitializeComponent();
            db = new DatabaseDataContext();
            r = new RekeningController(db);
            cbKlant.ItemsSource = r.get_klanten();
            cbSoort.ItemsSource = r.get_typen();
            dgRekening.ItemsSource = r.get_rekeningen();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow myWindow = new MainWindow();
            myWindow.Show();
            this.Close();
        }

        private void BtnAdd_Copy_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = (Customer)cbKlant.SelectedValue;
            int sCustomer = customer.CustomerId;

            decimal sBestedingslimiet = Convert.ToDecimal(txtBestedingslimiet.Text);
            decimal sSaldo = Convert.ToDecimal(txtSaldo.Text);
            string sStartdatum = dpStartdatum.ToString();

            Type soort = (Type)cbSoort.SelectedValue;
            int sTypeId = soort.TypeId;

            r.add_KlantRekening(customer, sBestedingslimiet, sSaldo, sStartdatum, sTypeId);

            dgRekening.ItemsSource = r.get_rekeningen();
            empty_attributes();
        }

        private void BtnRefresh_Copy_Click(object sender, RoutedEventArgs e)
        {
            CustomerAccount a = (CustomerAccount)dgRekening.SelectedItem;
            Customer sCustomer = null;
            if (!string.IsNullOrEmpty(cbKlant.Text))
            {
                sCustomer = (Customer)cbKlant.SelectedValue;
            }
            else {
                sCustomer = (db.Customers.Where(ca => ca.CustomerId == a.CustomerId)).Single();
            }

            decimal sBestedingslimiet = Convert.ToDecimal(txtBestedingslimiet.Text);
            decimal sSaldo = Convert.ToDecimal(txtSaldo.Text);
            string sStartdatum = dpStartdatum.ToString();
            Type hetType = null;
            if (!string.IsNullOrEmpty(cbSoort.Text))
            {
                hetType = (Type)cbSoort.SelectedValue;
                //sTypeId = soort.TypeId;
            }
            else
            {
                hetType = a.Account.Type;
            }

            r.update_KlantRekening(a.AccountId, sCustomer, sBestedingslimiet, sSaldo, sStartdatum, hetType);

            dgRekening.ItemsSource = r.get_rekeningen();
            empty_attributes();
        }

        private void dgKlant_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgRekening.SelectedItem == null) return;
            Account deKlant = (Account)dgRekening.SelectedItem;
            empty_attributes();
        }

        private void BtnDelete_Copy_Click(object sender, RoutedEventArgs e)
        {
            CustomerAccount a = (CustomerAccount)dgRekening.SelectedItem;
            r.deleteRekening(a);
            dgRekening.ItemsSource = r.get_rekeningen();
            empty_attributes();
        }
        //function to empty all attributes
        private void empty_attributes()
        {
            txtBestedingslimiet.Text = "".ToString();
            txtSaldo.Text = "".ToString();
            dpStartdatum.Text = "".ToString();
        }

        //Function to fill in all attributes
        private void DgRekening_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void dgRekening_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CustomerAccount a = (CustomerAccount)dgRekening.SelectedItem;
            txtBestedingslimiet.Text = a.Account.bestedingslimiet.ToString();
            txtSaldo.Text = a.Account.saldo.ToString();
            dpStartdatum.Text = a.Account.startdatum.ToString();
            int sCustomer = r.get_customerId(a.AccountId);
            cbKlant.Text = r.get_cbKlant(sCustomer);
            cbSoort.Text = r.get_cbSoort(a.Account.Type.TypeId);
        }
    }
}
