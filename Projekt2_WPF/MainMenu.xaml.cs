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
    /// Logika interakcji dla klasy MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
            DeleteStuff.IsEnabled = false;
        }

       
        private void MainMenuBackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Owner = this;
            this.Hide();
            mw.ShowDialog();
        }

        private void DeleteStuff_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddStuff_Click(object sender, RoutedEventArgs e)
        {
            AddWindow aw = new AddWindow();
            aw.Owner = this;
            this.Hide();
            aw.ShowDialog();
        }

        private void BrowseStuff_Click(object sender, RoutedEventArgs e)
        {
            BrowseWindow bw = new BrowseWindow();
            bw.Owner = this;
            this.Hide();
            bw.ShowDialog();
        }
    }
}
