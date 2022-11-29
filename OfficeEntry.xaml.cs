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

namespace Jūsų_IT
{
    /// <summary>
    /// Interaction logic for OfficeEntry.xaml
    /// </summary>
    public partial class OfficeEntry : Window
    {
        public int officeId = 0;

        public OfficeEntry()
        {
            InitializeComponent();
        }

        public OfficeEntry(int id, string title, string location)
        {
            InitializeComponent();
            OfficeTitle.Text = title;
            OfficeLocation.Text = location;
            officeId = id;
        }

        private void AddOffice_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).DialogResult = true;
            Window.GetWindow(this).Close();
        }

        private void CancelOffice_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).DialogResult = false;
            Window.GetWindow(this).Close();
        }
    }
}
