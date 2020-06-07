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
    /// Interaction logic for Klant.xaml
    /// </summary>
    public partial class Klant : Window
    {
        private KlantController pc;
        private DatabaseDataContext db;
        public Klant()
        {
            InitializeComponent();
            db = new DatabaseDataContext();
            pc = new KlantController(db);
            dgKlant.ItemsSource = pc.geefAlleKlanten();
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

        private void btnAdd_Copy_Click(object sender, RoutedEventArgs e)
        {
            string sVoornaam = txtVoornaam.Text;
            string sAchternaam = txtAchternaam.Text;
            string sLeeftijd = txtLeeftijd.Text;
            string sWoonplaats = txtWoonplaats.Text;
            string sAdres = txtAdres.Text;
            string iBsn = txtBsn.Text;
            string dGeboortedatum = dpGeboortedatum.SelectedDate.Value.ToString();
            string sEmail = txtEmail.Text;
            string sTelefoon = txtTelefoon.Text;

            pc.opslaanCustomer(sVoornaam, sAchternaam, sLeeftijd, sWoonplaats, sAdres, iBsn, dGeboortedatum, sEmail, sTelefoon);
            dgKlant.ItemsSource = pc.geefAlleKlanten();

            empty_attributes();
        }

        private void btnRefresh_Copy_Click(object sender, RoutedEventArgs e)
        {
            var deKlant = (Customer)dgKlant.SelectedItem;
            pc.deleteKlant(deKlant);

            string sVoornaam = txtVoornaam.Text;
            string sAchternaam = txtAchternaam.Text;
            string sLeeftijd = txtLeeftijd.Text;
            string sWoonplaats = txtWoonplaats.Text;
            string sAdres = txtAdres.Text;
            string iBsn = txtBsn.Text;
            string dGeboortedatum = dpGeboortedatum.SelectedDate.Value.ToString();
            string sEmail = txtEmail.Text;
            string sTelefoon = txtTelefoon.Text;

            pc.opslaanCustomer(sVoornaam, sAchternaam, sLeeftijd, sWoonplaats, sAdres, iBsn, dGeboortedatum, sEmail, sTelefoon);
            dgKlant.ItemsSource = pc.geefAlleKlanten();

            empty_attributes();
        }

        private void dgKlant_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgKlant.SelectedItem == null) return;
            Customer deKlant = (Customer)dgKlant.SelectedItem;
            txtVoornaam.Text = deKlant.voornaam.ToString();
            txtAchternaam.Text = deKlant.achternaam.ToString();
            txtLeeftijd.Text = deKlant.leeftijd.ToString();
            txtWoonplaats.Text = deKlant.woonplaats.ToString();
            txtAdres.Text = deKlant.adres.ToString();
            txtBsn.Text = deKlant.bsn.ToString();
            txtEmail.Text = deKlant.email.ToString();
            txtTelefoon.Text = deKlant.telefoon.ToString();
            dpGeboortedatum.Text = deKlant.geboortedatum.ToString();
        }

        private void btnDelete_Copy_Click(object sender, RoutedEventArgs e)
        {
            var deKlant = (Customer)dgKlant.SelectedItem;
            pc.deleteKlant(deKlant);
            dgKlant.ItemsSource = pc.geefAlleKlanten();
            empty_attributes();
        }
        //function to empty all attributes
        private void empty_attributes() {
            txtVoornaam.Text = "".ToString();
            txtAchternaam.Text = "".ToString();
            txtLeeftijd.Text = "".ToString();
            txtWoonplaats.Text = "".ToString();
            txtAdres.Text = "".ToString();
            txtBsn.Text = "".ToString();
            dpGeboortedatum.Text = "".ToString();
            txtEmail.Text = "".ToString();
            txtTelefoon.Text = "".ToString();
        }
    }
}