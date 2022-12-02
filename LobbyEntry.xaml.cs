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
    /// Interaction logic for LobbyEntry.xaml
    /// </summary>
    public partial class LobbyEntry : Window
    {
        public LobbyEntry()
        {
            InitializeComponent();
        }

        public LobbyEntry(string title, string number)
        {
            InitializeComponent();
            LobbyTitle.Text = title;
            LobbyNumber.Text =  number;
        }

        private void AddLobby_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).DialogResult = true;
            Window.GetWindow(this).Close();
        }

        private void CancelLobby_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).DialogResult = false;
            Window.GetWindow(this).Close();
        }
    }
}
