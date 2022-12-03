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
    /// Interaction logic for StuffEntry.xaml
    /// </summary>
    public partial class StuffEntry : Window
    {
        public StuffEntry()
        {
            InitializeComponent();
        }

        public StuffEntry(string title, string model, double price, bool isTaken, string takenBy)
        {
            InitializeComponent();

            StuffTitle.Text = title;
            StuffModel.Text = model;
            StuffPrice.Text = price.ToString();
            StuffIsTaken.IsChecked = isTaken;
            StuffOwner.Text = takenBy.ToString();

        }

        private void IsTaken_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddStuff_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).DialogResult = true;
            Window.GetWindow(this).Close();
        }

        private void CancelStuff_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).DialogResult = false;
            Window.GetWindow(this).Close();
        }
    }
}
