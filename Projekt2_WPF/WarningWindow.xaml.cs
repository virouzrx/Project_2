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
    /// Logika interakcji dla klasy WarningWindow.xaml
    /// </summary>
    public partial class WarningWindow : Window
    {
        public WarningWindow()
        {
            InitializeComponent();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            using (var ctx = new CepikDB())
            {
                var sql = @"Update [User] SET FirstName = @FirstName WHERE Id = @Id";
                ctx.Database.ExecuteSqlCommand(sql);
            }
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {   
            Hide();
        }
    }
}
