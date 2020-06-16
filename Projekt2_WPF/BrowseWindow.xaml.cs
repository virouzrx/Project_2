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

namespace Projekt2_WPF
{
    /// <summary>
    /// Logika interakcji dla klasy BrowseWindow.xaml
    /// </summary>
    public partial class BrowseWindow : Window
    {
        public BrowseWindow()
        {
            InitializeComponent();
        }


          

        private void DriversList_Checked(object sender, RoutedEventArgs e)
        {
            using (var ctx = new CepikDB())
            {
                var driverList = ctx.Kierowcy
                    .SqlQuery("Select * From Drivers")
                    .ToList<Drivers>();
                MyList1.ItemsSource = driverList;
                //firstColumn.DisplayMemberBinding = "{Binding Path=FirstName}"; <- to nie dziala 
            }
            MyList1.Visibility = Visibility.Visible;
        }

        private void CarsList_Checked(object sender, RoutedEventArgs e)
        {
            using (var ctx = new CepikDB())
            {
                var carList = ctx.Pojazdy
                    .SqlQuery("SELECT *" +
                    "FROM Cars" +
                    "")
                    .ToList<Cars>();
                MyList2.ItemsSource = carList;
            }
            MyList2.Visibility = Visibility.Visible;
        }

        private void IpList_Checked(object sender, RoutedEventArgs e)
        {
            using (var ctx = new CepikDB())
            {
                var IpList = ctx.Polisa
                    .SqlQuery("Select * From InsurancePolicies")
                    .ToList<InsurancePolicy>();
                MyList3.ItemsSource = IpList;
            }
            MyList3.Visibility = Visibility.Visible;
        }

        private void TechReviewList_Checked(object sender, RoutedEventArgs e)
        {
            using (var ctx = new CepikDB())
            {
                var techReviewList = ctx.BadanieTechniczne
                    .SqlQuery("Select * From BadanieTechniczne")
                    .ToList<TechnicalReview>();
                MyList4.ItemsSource = techReviewList;
            }
            MyList4.Visibility = Visibility.Visible;
        }

        private void ExpiredTechReviewList_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void DriversWithNoCarsList_Checked(object sender, RoutedEventArgs e)
        {
            using (var ctx = new CepikDB())
            {
                var driverNoCarList = ctx.Kierowcy
                    .SqlQuery("Select * From Pojazdy p INNER JOIN Kierowcy k ON p.DriverId = k.DriverId WHERE CarId IS NULL")
                    .ToList<Drivers>();
                MyList5.ItemsSource = driverNoCarList;
            }
            MyList5.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.Owner = this;
            this.Hide();
            mm.ShowDialog();
        }
    }
}
