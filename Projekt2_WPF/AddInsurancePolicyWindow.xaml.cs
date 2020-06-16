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
    /// Logika interakcji dla klasy AddInsurancePolicyWindow.xaml
    /// </summary>
    public partial class AddInsurancePolicyWindow : Window
    {
        public AddInsurancePolicyWindow()
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
                var ip = new InsurancePolicy();
                var RegexID = new Regex(@"^[0-9]{1,6}$");
                var RegexDate = new Regex(@"^[0-3][0-9]\/[0-1][0-9]\/[0-2][0-9]{3}$");
                bool IpIdCondition = false;
                bool CarIdCondition = false;
                bool firstDateCondition = false;
                bool secondDateCondition = false;



                //IP id
                int number;
                bool success = Int32.TryParse(IpIdTextBox.Text, out number);
                if (string.IsNullOrEmpty(IpIdTextBox.Text))
                {
                    IpIdNotEmpty.Visibility = Visibility.Visible;
                }
                else if (!success || !RegexID.IsMatch(IpIdTextBox.Text))
                {
                    IpIdIncorrect.Visibility = Visibility.Visible;
                }
                else
                {
                    ip.InsuranceId = number;
                    IpIdCondition = true;
                };

                //first date
                if (string.IsNullOrEmpty(firstDateTextBox.Text))
                {
                    FirstDateNotEmpty.Visibility = Visibility.Visible;
                }
                else if (!RegexDate.IsMatch(firstDateTextBox.Text))
                {
                    FirstDateIncorrect.Visibility = Visibility.Visible;
                }
                else
                {
                    ip.PolicyStartDay = DateTime.Parse(firstDateTextBox.Text);
                    firstDateCondition = true;
                };

                //last name
                if (string.IsNullOrEmpty(secondDateTextBox.Text))
                {
                    SecondDateNotEmpty.Visibility = Visibility.Visible;
                }
                else if (!RegexDate.IsMatch(secondDateTextBox.Text))
                {
                    SecondDateIncorrect.Visibility = Visibility.Visible;
                }
                else
                {
                    ip.PolicyExpiryDate = DateTime.Parse(secondDateTextBox.Text);
                    secondDateCondition = true;
                };

                //car id
                bool success2 = Int32.TryParse(carIdTextBox.Text, out number);
                if (string.IsNullOrEmpty(IpIdTextBox.Text))
                {
                    CarIdNotEmpty.Visibility = Visibility.Visible;
                }
                else if (!success || !RegexID.IsMatch(carIdTextBox.Text))
                {
                    CarIdIncorrect.Visibility = Visibility.Visible;
                }
                else
                {
                    ip.CarId = number;
                    CarIdCondition = true;
                };


                {
                    if (IpIdCondition && CarIdCondition && firstDateCondition && secondDateCondition)
                    {
                        try
                        {
                            backButton.IsEnabled = false;
                            saveButton.IsEnabled = false;
                            saveData.Content = "Trwa zapisywanie danych do bazy";
                            saveData.Visibility = Visibility.Visible;
                            context.Polisa.Add(ip);
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

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddWindow aw = new AddWindow();
            aw.Owner = this;
            this.Hide();
            aw.ShowDialog();
        }
    }
}
