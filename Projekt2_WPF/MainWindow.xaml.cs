using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
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
    /// Logika interakcji dla klasy SecondMainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        BackgroundWorker worker = new BackgroundWorker();
        public MainWindow()
        {
            InitializeComponent();
            CreateDbButton.IsEnabled = false;
            using (var database = new CepikDB()) //nie dziala, nie rozumiem czemu
            {
                if (database.Database.Exists())
                {
                    CreateDbButton.IsEnabled = false;

                }
                else
                {
                    CreateDbButton.IsEnabled = true;
                }
            }

        }

        private void CreateDB(object sender, RoutedEventArgs e)
        {
            CreateDbButton.IsEnabled = false;
            DeleteDbButton.IsEnabled = false;
            GoToDbButton.IsEnabled = false;
            ProgressBar.Visibility = Visibility.Visible;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var database = new CepikDB())
            {
                if (!database.Database.Exists())
                {
                    Database.SetInitializer(new CreateDatabaseIfNotExists<CepikDB>());
                    var context = new CepikDB();
                    context.Database.Create();
                }
                var admin = new Users();
                admin.Login = "admin";
                admin.Password = "password";
                admin.Role = 1;
                database.Uzytkownicy.Add(admin);
                database.SaveChanges();
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressInfoLabel.Visibility = Visibility.Visible;
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressInfoLabel.Content = "Baza została utworzona!";
            CreateDbButton.IsEnabled = false;
        }


        private void DeleteDB(object sender, RoutedEventArgs e)
        {
            WarningWindow mm = new WarningWindow();
            mm.Owner = this;
            mm.ShowDialog();
        }

        private void GoToDB(object sender, RoutedEventArgs e)
        {
            SecondMainWindow mm = new SecondMainWindow();
            mm.Owner = this;
            this.Hide();
            mm.ShowDialog();
        }
    }
}

