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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Data.Entity;

namespace Projekt2_WPF
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class SecondMainWindow : Window
    {
        BackgroundWorker worker = new BackgroundWorker();
        public SecondMainWindow()
        {
            InitializeComponent();
          

        }
        private void logInButton_Click(object sender, RoutedEventArgs e)
        {
            using (var ctx = new CepikDB())
            {
                var login = LoginTextBox.Text;
                var password = PasswordTextBox.Text;

                var logginIn = ctx.Uzytkownicy
                                    .Where(s => s.Login == login && s.Password == password)
                                    .FirstOrDefault<Users>();
                if (logginIn == null)
                {
                    Wrong.Content = "Nieprawidłowy login lub hasło.";
                }
                else if (logginIn.Role == 1)
                {
                    MainMenu mm = new MainMenu();
                    mm.Owner = this;
                    this.Hide();
                    mm.ShowDialog();

                    mm.Hello.Content = "Witaj, adminie";
                }
                else if (logginIn.Role == 2)
                {
                    MainMenu mm = new MainMenu();
                    mm.Owner = this;
                    this.Hide();
                    mm.ShowDialog();
                    mm.Hello.Content = "Witaj, diagnosto";
                }
                else if (logginIn.Role == 3)
                {
                    MainMenu mm = new MainMenu();
                    mm.Owner = this;
                    this.Hide();
                    mm.ShowDialog();
                    mm.Hello.Content = "Witaj, użytkowniku";
                }

            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mm = new MainWindow();
            mm.Owner = this;
            this.Hide();
            mm.ShowDialog();
        }
    }
}