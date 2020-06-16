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
    /// Logika interakcji dla klasy AddDriverWindow.xaml
    /// </summary>
    public partial class AddDriverWindow : Window
    {
        public AddDriverWindow()
        {
            InitializeComponent();
            foreach (Control ctl in Container.Children)
            {
                if (ctl.GetType() == typeof(Label))
                    ((Label)ctl).Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new CepikDB())
            {
                var drivers = new Drivers();
                var RegexID = new Regex(@"^[0-9]{1,6}$");
                var RegexName = new Regex(@"^[A-Z][a-z]*$");
                var RegexAddressCity = new Regex(@"^([A-Z][a-z]*(-[A-Z][a-z]*)?)$");
                var RegexAddress = new Regex(@"^([A-Z][a-z]*(\s[0-9]{1,5})?)$");
                var RegexLastName = new Regex(@"^([A-Z][a-z]*(-[A-Z][a-z]*)?)$");
                var RegexPESEL = new Regex(@"^[0-9]{11}$");
                var RegexDate = new Regex(@"^[0-3][0-9]\/[0-1][0-9]\/[1-2][0-9][0-9][0-9]$");
                    bool DriverIdCondition = false;
                    bool AddressCondition = false;
                    bool PeselCondition = false;
                    bool NameCondition = false;
                    bool LastNameCondition = false;
                    bool RegionCondition = false;
                    bool CityCondition = false;
                    bool DateCondition = true;

         

                //driver id
                int number;
                bool success = Int32.TryParse(driverIdTextBox.Text, out number);
                if (string.IsNullOrEmpty(driverIdTextBox.Text))
                {
                    driverIdNotEmpty.Visibility = Visibility.Visible;
                }
                else if (!success || !RegexID.IsMatch(driverIdTextBox.Text))
                {
                    driverIdIncorrect.Visibility = Visibility.Visible;
                }
                else
                {
                    drivers.DriverId = number;
                    DriverIdCondition = true;
                };

                //name 
                if (string.IsNullOrEmpty(nameTextBox.Text))
                {
                    nameNotEmpty.Visibility = Visibility.Visible;
                }
                else if (!RegexName.IsMatch(nameTextBox.Text))
                {
                    nameIncorrect.Visibility = Visibility.Visible;
                }
                else
                {
                    drivers.FirstName = nameTextBox.Text;
                    NameCondition = true;
                };

                //last name
                if (!RegexName.IsMatch(lastNameTextBox.Text))
                {
                    lastNameIncorrect.Visibility = Visibility.Visible;
                }
                else if (string.IsNullOrEmpty(lastNameTextBox.Text))
                {
                    lastNameNotEmpty.Visibility = Visibility.Visible;
                }
                else
                {
                    drivers.LastName = lastNameTextBox.Text;
                    LastNameCondition = true;
                };

                //address
                if (!RegexAddress.IsMatch(addressTextBox.Text))
                {
                    addressIncorrect.Visibility = Visibility.Visible;
                }
                else if (string.IsNullOrEmpty(addressTextBox.Text))
                {
                    addressNotEmpty.Visibility = Visibility.Visible;
                }
                else
                {
                    drivers.Address = addressTextBox.Text;
                    AddressCondition = true;
                }

                //region 
                if (string.IsNullOrEmpty(regionTextBox.Text))
                {
                    regionNotEmpty.Visibility = Visibility.Visible;
                }
                else
                {
                    drivers.Region = regionTextBox.Text;
                    RegionCondition = true;
                }

                //city
                if (!RegexAddressCity.IsMatch(cityTextBox.Text))
                {
                    cityIncorrect.Visibility = Visibility.Visible;
                }
                else if (string.IsNullOrEmpty(cityTextBox.Text))
                {
                    cityNotEmpty.Visibility = Visibility.Visible;
                }
                else
                {
                    drivers.City = cityTextBox.Text;
                    CityCondition = true;
                }
                
                //pesel

                if (!RegexPESEL.IsMatch(peselTextBox.Text))
                {
                    peselIncorrect.Visibility = Visibility.Visible;
                }
                else if (string.IsNullOrEmpty(peselTextBox.Text))
                {
                    peselNotEmpty.Visibility = Visibility.Visible;
                }
                else
                {
                    drivers.PESEL = peselTextBox.Text;
                    PeselCondition = true;
                }

                //date of birth 
                if (!RegexDate.IsMatch(dateOfBirthTextBox.Text))
                {
                    dateOfBirthIncorrect.Visibility = Visibility.Visible;
                }
                else if (string.IsNullOrEmpty(dateOfBirthTextBox.Text))
                {
                    dateOfBirthNotEmpty.Visibility = Visibility.Visible;
                }
                else
                {
                    drivers.BirthDate = DateTime.Parse(dateOfBirthTextBox.Text);
                    DateCondition = true;
                }

                if (DriverIdCondition && NameCondition && LastNameCondition && AddressCondition && PeselCondition && CityCondition && RegionCondition && DateCondition)
                {
                    backButton.IsEnabled = false;
                    saveButton.IsEnabled = false;
                    dataSaved.Content = "Trwa zapisywanie do bazy...";
                    dataSaved.Visibility = Visibility.Visible;
                    context.Kierowcy.Add(drivers);
                    context.SaveChanges();
                    dataSaved.Content = "Dane zostały zapisane do bazy!";
                    backButton.IsEnabled = true;
                    saveButton.IsEnabled = true;
                    foreach (Control ctl in Container.Children)
                    {
                        if (ctl.GetType() == typeof(Label))
                            ((Label)ctl).Visibility = Visibility.Hidden;
                        if (ctl.GetType() == typeof(TextBox))
                            ((TextBox)ctl).Text = String.Empty;
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                    dataSaved.Visibility = Visibility.Hidden;
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow aw = new AddWindow();
            aw.Owner = this;
            this.Hide();
            aw.ShowDialog();
        }
    }
}
