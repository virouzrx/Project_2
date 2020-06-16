using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
    /// Logika interakcji dla klasy AddCarWindow.xaml
    /// </summary>
    public partial class AddCarWindow : Window
    {
        public AddCarWindow()
        {
            InitializeComponent();
            //czy mniej kodu jest tu, czy jak zrobie to dynamicznie?
            foreach (Control ctl in Container.Children)
            {
                if (ctl.GetType() == typeof(Label))
                    ((Label)ctl).Visibility = Visibility.Hidden;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddWindow aw = new AddWindow();
            aw.Owner = this;
            this.Hide();
            aw.ShowDialog();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            using (var context = new CepikDB())
            {
                var cars = new Cars();
                var RegexCarID = new Regex(@"^[0-9]{1,6}$");
                var RegexCarName = new Regex(@"^([A-Z][a-z]*( [A-Z][a-z]*)?)$");
                var RegexLicensePlate = new Regex(@"^[A-Z]{2,3}[A-Z0-9]{4,5}$");
                var RegexModelName = new Regex(@"^([A-Z][a-z]*( [A-Z][a-z]*( [A-Za-z0-9]*))?)$");
                bool CarIdCondition = false; ;
                bool DriverIdCondition = false;
                bool CarModelCondition = false;
                bool CarCompanyCondition = false;
                bool LicensePlateCondition = false;



                //carid
                int number;
                bool success = Int32.TryParse(carIdTextBox.Text, out number);
                if (string.IsNullOrEmpty(carIdTextBox.Text))
                {
                    carIdNotEmpty.Visibility = Visibility.Visible;
                }
                else if (!RegexCarID.IsMatch(carIdTextBox.Text) || !success)
                {
                    carIdIncorrect.Visibility = Visibility.Hidden;
                }
                else
                {
                    cars.CarId = number;
                    CarIdCondition = true;
                };

                //license plate
                if (string.IsNullOrEmpty(licensePlateTextBox.Text))
                {
                    licensePlateNotEmpty.Visibility = Visibility.Visible;
                }
                else if (!RegexLicensePlate.IsMatch(licensePlateTextBox.Text))
                {
                    licensePlateIncorrect.Visibility = Visibility.Visible;
                }
                else
                {
                    cars.LicensePlateNumber = licensePlateTextBox.Text;
                    LicensePlateCondition = true;
                }

                //model
                if (string.IsNullOrEmpty(modelTextBox.Text))
                {
                    ModelNotEmpty.Visibility = Visibility.Visible;
                }
                else if (!RegexModelName.IsMatch(modelTextBox.Text))
                {
                    modelIncorrect.Visibility = Visibility.Visible;
                }
                else
                {
                    cars.CarModel = modelTextBox.Text;
                    CarModelCondition = true;
                }

                //company
                if (string.IsNullOrEmpty(companyTextBox.Text))
                {
                    companyEmpty.Visibility = Visibility.Visible;
                }
                else if (!RegexCarName.IsMatch(companyTextBox.Text))
                {
                    companyIncorrect.Visibility = Visibility.Visible;
                }
                else
                {
                    cars.CarCompany = companyTextBox.Text;
                    CarCompanyCondition = true;
                }

                //driver id
                bool success2 = Int32.TryParse(driverIdTextBox.Text, out number);
                if (!success || !RegexCarID.IsMatch(driverIdTextBox.Text))
                {
                    driverIdIncorrect.Visibility = Visibility.Visible;
                }
                else if (string.IsNullOrEmpty(carIdTextBox.Text))
                {
                    driverIdNotEmpty.Visibility = Visibility.Visible;
                }
                else
                {
                    cars.DriverId = number;
                    DriverIdCondition = true;
                }

                if (DriverIdCondition && LicensePlateCondition && CarIdCondition && CarModelCondition && CarCompanyCondition)
                {
                    try
                    {
                        backButton.IsEnabled = false;
                        saveButton.IsEnabled = false;
                        saveData.Content = "Trwa zapisywanie danych do bazy";
                        saveData.Visibility = Visibility.Visible;
                        context.Pojazdy.Add(cars);
                        context.SaveChanges();
                        backButton.IsEnabled = true;
                        saveButton.IsEnabled = true;
                        foreach (Control ctl in Container.Children)
                        {
                            if (ctl.GetType() == typeof(Label))
                                ((Label)ctl).Visibility = Visibility.Hidden;
                            if (ctl.GetType() == typeof(TextBox))
                                ((TextBox)ctl).Text = String.Empty;
                        }
                        saveData.Content = "Dane zostały zapisane";
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        saveData.Visibility = Visibility.Hidden;

                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        ErrorWindow2 ew = new ErrorWindow2();
                        ew.Owner = this;
                        ew.ShowDialog();
                    }
                }


            }
        }
    }
}
