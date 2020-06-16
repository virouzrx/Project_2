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
    /// Logika interakcji dla klasy AddCarDocsAndInfo.xaml
    /// </summary>
    public partial class AddCarDocsAndInfo : Window
    {
        public AddCarDocsAndInfo()
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
                var carInfo = new CarDocsAndInfo();
                var RegexVIN = new Regex(@"^[A-Z0-9]{17}$");
                var RegexDate = new Regex(@"^[0-3][0-9]\/[0-1][0-9]\/[0-2][0-9]{3}$");
                var RegexCarId = new Regex(@"^[0-9]$");
                bool VINcondition = false;
                bool DateCondition = false;
                bool IdCondition = false;
                bool carCardCondition = false;

                //czy mniej kodu jest tu, czy jak zrobie to dynamicznie?
             

                //vin number
                if (string.IsNullOrEmpty(vinTextBox.Text))
                {
                    vinNotEmpty.Visibility = Visibility.Visible;
                }
                else if (!RegexDate.IsMatch(vinTextBox.Text))
                {
                    vinIncorrect.Visibility = Visibility.Hidden;
                }
                else
                {
                    carInfo.VinNumber = vinTextBox.Text;
                    VINcondition = true;
                };

                //car card nr 
                if (string.IsNullOrEmpty(carCardTextBox.Text))
                {
                    carCardNotEmpty.Visibility = Visibility.Visible;
                }
                else
                {
                    carInfo.CarCardNumber = carCardTextBox.Text;
                    carCardCondition = true;
                };
                
                //date
                if (string.IsNullOrEmpty(firstRegistryTextBox.Text))
                {
                    DateNotEmpty.Visibility = Visibility.Visible;
                }
                else if (!RegexDate.IsMatch(firstRegistryTextBox.Text))
                {
                    firstRegistryIncorrect.Visibility = Visibility.Visible;
                }
                else
                {
                    carInfo.FirstRegistrationDate = DateTime.Parse(firstRegistryTextBox.Text);
                    DateCondition = true;
                }
                
                //car id
                int number;
                bool success = Int32.TryParse(carIdTextBox.Text, out number);
                if (!success || !RegexCarId.IsMatch(carIdTextBox.Text))
                {
                    carIdIncorrect.Visibility = Visibility.Visible;
                }
                else if (string.IsNullOrEmpty(carIdTextBox.Text))
                {
                    IdNotEmpty.Visibility = Visibility.Visible;
                }
                else
                {
                    carInfo.CarId = number;
                    IdCondition = true;
                }

                
                if (IdCondition && DateCondition && VINcondition && carCardCondition)
                {
                    try
                    {
                        backButton.IsEnabled = false;  
                        saveButton.IsEnabled = false;
                        saveData.Content = "Trwa zapisywanie danych do bazy";
                        saveData.Visibility = Visibility.Visible;
                        context.Informacje.Add(carInfo);
                        context.SaveChanges();
                        saveData.Content = "Dane zostały zapisane";
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
                        saveData.Visibility = Visibility.Hidden;
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        ErrorWindow ew = new ErrorWindow();
                        ew.Owner = this;
                        ew.ShowDialog();
                    }
                }

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddWindow aw = new AddWindow();
            aw.Owner = this;
            this.Hide();
            aw.ShowDialog();
        }

        private void vinTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
