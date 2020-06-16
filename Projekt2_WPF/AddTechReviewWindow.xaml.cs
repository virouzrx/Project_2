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
    /// Logika interakcji dla klasy AddTechReviewWindow.xaml
    /// </summary>
    public partial class AddTechReviewWindow : Window
    {
        public AddTechReviewWindow()
        {
            InitializeComponent();
            foreach (Control ctl in Container.Children)
            {
                if (ctl.GetType() == typeof(Label))
                    ((Label)ctl).Visibility = Visibility.Hidden;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            negativeCheckBox.IsChecked = false;
        }

        private void negativeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            positiveCheckBox.IsChecked = false;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new CepikDB())
            {
                var tr = new TechnicalReview();
                var RegexID = new Regex(@"^[0-9]{1,6}$");
                var RegexDate = new Regex(@"^[0-3][0-9]\/[0-1][0-9]\/[0-2][0-9]{3}$");
                bool TrIdCondition = false;
                bool CarIdCondition = false;
                bool firstDateCondition = false;
                bool secondDateCondition = false;
                bool checkBoxCondition = false;

             

                //IP id
                int number;
                bool success = Int32.TryParse(trIdTextBox.Text, out number);
                if (string.IsNullOrEmpty(trIdTextBox.Text))
                {
                    TrIdNotEmpty.Visibility = Visibility.Visible;
                }
                else if (!success || !RegexID.IsMatch(trIdTextBox.Text))
                {
                    TrIdIncorrect.Visibility = Visibility.Visible;
                }
                else
                {
                    tr.TechnicalReviewID = number;
                    TrIdCondition = true;
                };

                //first date
                if (string.IsNullOrEmpty(firstDateTextBox.Text))
                {
                    TrDateNotEmpty.Visibility = Visibility.Visible;
                }
                else if (!RegexDate.IsMatch(firstDateTextBox.Text))
                {
                    TrDateIncorrect.Visibility = Visibility.Visible;
                }
                else
                {
                    tr.TechnicalReviewDate= DateTime.Parse(firstDateTextBox.Text);
                    firstDateCondition = true;
                };

                //2nd date
                if (string.IsNullOrEmpty(secondDateTextBox.Text))
                {
                    TrExpiryNotEmpty.Visibility = Visibility.Visible;
                }
                else if (!RegexDate.IsMatch(secondDateTextBox.Text))
                {
                    TrExpiryIncorect.Visibility = Visibility.Visible;
                }
                else
                {
                    tr.TechnicalReviewExpiryDate = DateTime.Parse(secondDateTextBox.Text);
                    secondDateCondition = true;
                };

                //car id
                bool success2 = Int32.TryParse(carIdTextBox.Text, out number);
                if (string.IsNullOrEmpty(carIdTextBox.Text))
                {
                    CarIdNotEmpty.Visibility = Visibility.Visible;
                }
                else if (!success || !RegexID.IsMatch(carIdTextBox.Text))
                {
                    CarIdIncorrect.Visibility = Visibility.Visible;
                }
                else
                {
                    tr.CarId = number;
                    CarIdCondition = true;
                };
                if (positiveCheckBox.IsChecked == false && negativeCheckBox.IsChecked == false)
                {
                    noCheck.Visibility = Visibility.Visible;
                }
                else if (positiveCheckBox.IsChecked == true)
                {
                    tr.TechnicalReviewStatus = true;
                    checkBoxCondition = true;
                } 
                else
                {
                    tr.TechnicalReviewStatus = false;
                    checkBoxCondition = true;
                }


                if (TrIdCondition && CarIdCondition && firstDateCondition && secondDateCondition && checkBoxCondition)
                {
                    try
                    {
                        backButton.IsEnabled = false;
                        saveButton.IsEnabled = false;
                        saveData.Content = "Trwa zapisywanie danych do bazy";
                        saveData.Visibility = Visibility.Visible;
                        context.BadanieTechniczne.Add(tr);
                        context.SaveChanges();
                        saveData.Content = "Dane zostały zapisane do bazy";
                        backButton.IsEnabled = true;
                        saveButton.IsEnabled = true;
                        foreach (Control ctl in Container.Children)
                        {
                            if (ctl.GetType() == typeof(Label))
                                ((Label)ctl).Visibility = Visibility.Hidden;
                            if (ctl.GetType() == typeof(TextBox))
                                ((TextBox)ctl).Text = String.Empty;
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
    }
}
