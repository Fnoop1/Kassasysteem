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
    /// Interaction logic for rekeningoverzicht.xaml
    /// </summary>
    public partial class rekeningoverzicht : Window
    {
        private RekeningoverzichtController r;
        private DatabaseDataContext db;

        public rekeningoverzicht()
        {
            InitializeComponent();
            db = new DatabaseDataContext();
            r = new RekeningoverzichtController(db);
            dgRekeningoverzicht.ItemsSource = r.get_rekeningen_klanten();
        
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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainWindow myWindow = new MainWindow();
            myWindow.Show();
            this.Close();
        }

        private void dgRekeningoverzicht_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
