using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Logika interakcji dla klasy AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
            foreach (Control ctl in Container.Children)
            {
                if (ctl.GetType() == typeof(Label))
                    ((Label)ctl).Visibility = Visibility.Hidden;
                if (ctl.GetType() == typeof(TextBox))
                    ((TextBox)ctl).Text = String.Empty;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new CepikDB())
            {
                var user = new Users();
                bool passwordcondition = false;
                bool loginCondition = false;
                bool checkboxCondition = false;

                //login
                if (string.IsNullOrEmpty(loginTextBox.Text))
                {
                    LoginNotEmpty.Visibility = Visibility.Visible;
                }
                else
                {
                    user.Login = loginTextBox.Text;
                    loginCondition = true;
                };

                //password
                if (string.IsNullOrEmpty(passwordTextBox.Text))
                {
                    PasswordNotEmpty.Visibility = Visibility.Visible;
                }
                else
                {
                    user.Password = passwordTextBox.Text;
                    passwordcondition = true;
                };

                //role
                if (adminCheckBox.IsChecked == true)
                {
                    diagnostCheckBox.IsChecked = false;
                    userCheckBox.IsChecked = false;
                    user.Role = 1;
                    checkboxCondition = true;
                }
                else if (diagnostCheckBox.IsChecked == true)
                {
                    adminCheckBox.IsChecked = false;
                    userCheckBox.IsChecked = false;
                    user.Role = 2;
                    checkboxCondition = true;
                }
                else if (userCheckBox.IsChecked == true)
                {
                    adminCheckBox.IsChecked = false;
                    diagnostCheckBox.IsChecked = false;
                    user.Role = 3;
                    checkboxCondition = true;
                }
                else
                {
                    checkboxCondition = false;
                }

                if (passwordcondition && loginCondition && checkboxCondition)
                {

                    saveData.Content = "Trwa dodawanie użytkownika do bazy...";
                    saveData.Visibility = Visibility.Visible;
                    saveButton.IsEnabled = false;
                    backButton.IsEnabled = false;
                    context.Uzytkownicy.Add(user);
                    context.SaveChanges();
                    saveData.Content = "Użytkownik został dodant do bazy!";
                    saveButton.IsEnabled = true;
                    saveButton.IsEnabled = true;
                    foreach (Control ctl in Container.Children)
                    {
                        if (ctl.GetType() == typeof(Label))
                            ((Label)ctl).Visibility = Visibility.Hidden;
                        if (ctl.GetType() == typeof(TextBox))
                            ((TextBox)ctl).Text = String.Empty;
                        if (ctl.GetType() == typeof(CheckBox))
                            ((CheckBox)ctl).IsChecked = false;
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                    saveData.Visibility = Visibility.Hidden;
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

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }

}
