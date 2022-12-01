using System;
using System.IO;
using Microsoft.Win32;
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
using System.Xml.Serialization;

namespace Jūsų_IT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Office> offices = new List<Office>();

        public MainWindow()
        {
            InitializeComponent();

            offices.Add(new Office(1, "Kaunas office", "Student str. 50"));
            offices.Add(new Office(2, "Klaipeda office", "Pilies str. 50"));
            offices.Add(new Office(3, "Veisiejai","Dariaus ir Gireno str. 30"));

            Offices.ItemsSource = offices;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void exit_click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        

        private void btn_open_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open_file_dialog = new OpenFileDialog();
            open_file_dialog.Filter = "Text Files (*.txt)|*.txt| All Files (*.*)| *.*";

            if(open_file_dialog.ShowDialog() == true)
            {
                XmlSerializer serializer =  new XmlSerializer(typeof(List<Office>));
                using (Stream reader = new FileStream(open_file_dialog.FileName, FileMode.Open))
                {
                    offices = null;
                    offices = (List<Office>)serializer.Deserialize(reader);
                    Offices.ItemsSource = offices;
                }
            }
        }

        private void btn_save_file_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Text Files (*.txt)| *.txt| All Files (*.*)|*.*";

            if(saveFileDialog.ShowDialog() == true)
            {
                XmlSerializer serialiser = new XmlSerializer(typeof(List<Office>));
                TextWriter filestream = new StreamWriter(@saveFileDialog.FileName);
                serialiser.Serialize(filestream, offices);
                filestream.Close();
            }
        }

        private void AddOffice_Click(object sender, RoutedEventArgs e)
        {
            OfficeEntry officeEntryWindow = new OfficeEntry();
            if (officeEntryWindow.ShowDialog() == true)
            {
                offices.Add(new Office((offices.Max(t => t.OfficeId) + 1), officeEntryWindow.OfficeTitle.Text, officeEntryWindow.OfficeLocation.Text));
                Offices.Items.Refresh();
            }
        }

        private void RemoveOffice_Click(object sender, RoutedEventArgs e)
        {
            Office? selectedOffice = Offices.SelectedItem as Office;
            int index = offices.FindIndex(s => s.OfficeId == selectedOffice.OfficeId);
            if (index != -1) offices.RemoveAt(index);
            Offices.Items.Refresh();
        }

        private void EditOffice_Click(object sender, RoutedEventArgs e)
        {
            Office? selectedOffice = Offices.SelectedItem as Office;
            OfficeEntry officeEntryWindow = new OfficeEntry(selectedOffice.OfficeId, selectedOffice.Name, selectedOffice.Location);
            if (officeEntryWindow.ShowDialog() == true)
            {
                int index = offices.FindIndex(s => s.OfficeId == officeEntryWindow.officeId);

                if (index != -1)
                    offices[index] = new Office(officeEntryWindow.officeId, officeEntryWindow.OfficeTitle.Text, officeEntryWindow.OfficeLocation.Text);
                Offices.Items.Refresh();
            }
        }

        private void Offices_Selected(object sender, RoutedEventArgs e)
        {
        }

        private void Offices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Office? selectedOffice = Offices.SelectedItem as Office;
            Lobbies.ItemsSource = selectedOffice.Lobbies;
        }
    }

    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
