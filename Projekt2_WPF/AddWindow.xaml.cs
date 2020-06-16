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
    /// Logika interakcji dla klasy AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddCarWindow acw = new AddCarWindow
            {
                Owner = this
            };
            Hide();
            acw.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddCarDocsAndInfo acdai = new AddCarDocsAndInfo
            {
                Owner = this
            };
            Hide();
            acdai.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddInsurancePolicyWindow  aipw = new AddInsurancePolicyWindow
            {
                Owner = this
            };
            Hide();
            aipw.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AddTechReviewWindow atrw = new AddTechReviewWindow
            {
                Owner = this
            };
            Hide();
            atrw.ShowDialog();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mm = new MainMenu
            {
                Owner = this
            };
            Hide();
            mm.ShowDialog();
        }

        private void AddDriverButton_Click(object sender, RoutedEventArgs e)
        {
            AddDriverWindow adw = new AddDriverWindow
            {
                Owner = this
            };
            Hide();
            adw.ShowDialog();
        }

        private void Button_adddbuser(object sender, RoutedEventArgs e)
        {

        }
    }
}
