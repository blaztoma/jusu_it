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
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

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
            Offices.ItemsSource = offices;
        }

        private void exit_click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_open_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open_file_dialog = new OpenFileDialog();
            open_file_dialog.Filter = "XML Files (*.xml)| *.xml| All Files (*.*)|*.*";

            if (open_file_dialog.ShowDialog() == true)
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
            saveFileDialog.Filter = "XML Files (*.xml)| *.xml| All Files (*.*)|*.*";

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
                try
                {
                    offices.Add(new Office((offices.Max(t => t.OfficeId) + 1), officeEntryWindow.OfficeTitle.Text, officeEntryWindow.OfficeLocation.Text));
                    Offices.Items.Refresh();
                }
                catch (InvalidOperationException)
                {
                    offices.Add(new Office(1, officeEntryWindow.OfficeTitle.Text, officeEntryWindow.OfficeLocation.Text));
                    Offices.Items.Refresh();
                }

            }
        }

        private void RemoveOffice_Click(object sender, RoutedEventArgs e)
        {
            
            Office? selectedOffice = Offices.SelectedItem as Office;
            Lobby? selectedLobby = Lobbies.SelectedItem as Lobby; 
            int index = offices.FindIndex(s => s.OfficeId == selectedOffice.OfficeId);

            if (index != -1) 
            {//------------------------- 108 neleidzia office pasalinti -----------------------
                try
                {
                    selectedLobby.stuff.Clear();
                    selectedOffice.lobbies.Clear();
                    offices.RemoveAt(index);
                }
                catch (NullReferenceException)
                {
                    if(selectedOffice.lobbies != null)
                    {
                        selectedOffice.lobbies.Clear();
                        offices.RemoveAt(index);
                    }
                    else
                    {
                        offices.RemoveAt(index);
                    }
                }

                Offices.Items.Refresh();
                Lobbies.Items.Refresh();
                Stuffs.Items.Refresh();
            }
            
        }

        private void EditOffice_Click(object sender, RoutedEventArgs e)
        {
            Office? selectedOffice = Offices.SelectedItem as Office;
            OfficeEntry officeEntryWindow = new OfficeEntry(selectedOffice.OfficeId, selectedOffice.Name, selectedOffice.Location); 
            officeEntryWindow.OfficeEdit_btn.Content = "Redaguoti darbovietę";

            if (officeEntryWindow.ShowDialog() == true)
            {
                int index = offices.FindIndex(s => s.OfficeId == officeEntryWindow.officeId);
                if (index != -1)
                {
                    offices[index] = new Office(officeEntryWindow.officeId, officeEntryWindow.OfficeTitle.Text, officeEntryWindow.OfficeLocation.Text);
                }
                Offices.Items.Refresh();
            }
            
        }

        private void Offices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Office? selectedOffice = Offices.SelectedItem as Office;
            if (selectedOffice != null)
            {
                Lobbies.ItemsSource = selectedOffice.lobbies;
                OfficeEditButton.IsEnabled = true;
                OfficeRemoveButton.IsEnabled = true;
                LobbyAddButton.IsEnabled = true;


                try
                {
                    Lobbies.Columns[2].Visibility = Visibility.Collapsed;
                    Lobbies.Columns[0].Header = "Pavadinimas";
                    Lobbies.Columns[1].Header = "Numeris";
                }
                catch (Exception) { }

                
            }
            else
            {
                OfficeEditButton.IsEnabled = false;
                OfficeRemoveButton.IsEnabled = false;
                LobbyAddButton.IsEnabled = false;
            }
            Stuffs.Visibility = Visibility.Collapsed;
        }

        private void Lobbies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Lobby? selectedLobby = Lobbies.SelectedItem as Lobby;

            if (selectedLobby != null)
            {
                Stuffs.ItemsSource = selectedLobby.stuff;

                LobbyEditButton.IsEnabled = true;
                LobbyRemoveButton.IsEnabled = true;
                StuffAddButton.IsEnabled = true;

                try
                {
                    Stuffs.Columns[0].Header = "Pavadinimas";
                    Stuffs.Columns[1].Header = "Modelis";
                    Stuffs.Columns[2].Header = "Kaina Eurais (€)";
                    Stuffs.Columns[3].Header = "Paiimta";
                    Stuffs.Columns[4].Header = "Vardas pavardė";
                }
                catch (Exception) { }

                Stuffs.Visibility = Visibility.Visible;
            }
            else
            {
                LobbyEditButton.IsEnabled = false;
                LobbyRemoveButton.IsEnabled = false;
                StuffAddButton.IsEnabled = false;
            }
            try 
            {
                Stuffs.ItemsSource = selectedLobby?.stuff;
                StuffAmount.Content = "Kiekis: " + selectedLobby?.stuff.Count.ToString();
            }
            catch(Exception) { }
            
            
        }

        private void Stuffs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Stuff? selectedStuff = Stuffs.SelectedItem as Stuff;

            if (selectedStuff != null)
            {
                StuffEditButton.IsEnabled = true;
                StuffRemoveButton.IsEnabled = true;
            }
            else
            {
                StuffEditButton.IsEnabled = false;
                StuffRemoveButton.IsEnabled = false;
            }
        }

        private void AddLobby_Click(object sender, RoutedEventArgs e)
        {
            LobbyEntry lobbyEntryWindow = new LobbyEntry();

            try
            {
                if (lobbyEntryWindow.ShowDialog() == true)
                {
                    Office? selectedOffice = Offices.SelectedItem as Office;
                    selectedOffice?.lobbies.Add(new Lobby(lobbyEntryWindow.LobbyTitle.Text, int.Parse(lobbyEntryWindow.LobbyNumber.Text)));
                    Lobbies.Items.Refresh();
                }
            }
            catch (FormatException) { MessageBox.Show("Įvedėte ne skaičių"); }
            catch (NullReferenceException) { MessageBox.Show("Netaisyklingai sukūrėte darbovietę! \n Prašome ją pašalinti"); }
            
        }

        private void EditLobby_Click(object sender, RoutedEventArgs e)
        {
            
            Lobby? selectedLobby = Lobbies.SelectedItem as Lobby;
            LobbyEntry lobbyEntryWindow = new LobbyEntry(selectedLobby.Name, selectedLobby.Number.ToString());
            lobbyEntryWindow.LobbyEdit_btn.Content = "Redaguoti kabinetą";


            if (lobbyEntryWindow.ShowDialog() == true)
            {
                
                Office? selectedOffice = Offices.SelectedItem as Office;
                int index = selectedOffice.lobbies.FindIndex(s => s.Number == selectedLobby.Number);

                try
                {
                    if (index != -1)
                    {
                        selectedOffice.lobbies[index] = new Lobby(lobbyEntryWindow.LobbyTitle.Text, int.Parse(lobbyEntryWindow.LobbyNumber.Text));
                        Lobbies.Items.Refresh();
                    }
                }
                catch (FormatException) { MessageBox.Show("Įvedėte ne skaičių"); }

            }

        }

        private void RemoveLobby_Click(object sender, RoutedEventArgs e)
        {
            Lobby? selectedLobby = Lobbies.SelectedItem as Lobby;
            Office? selectedOffice = Offices.SelectedItem as Office;
            int index = selectedOffice.lobbies.FindIndex(s => s.Number == selectedLobby.Number);

            if (index != -1)
            {
                try
                {
                    selectedLobby?.stuff.Clear();
                    selectedOffice.lobbies.RemoveAt(index);  
                }
                catch(NullReferenceException)
                {
                    selectedOffice.lobbies.RemoveAt(index);
                }
            }

            Lobbies.Items.Refresh();
            Stuffs.Items.Refresh();
        }

        private void AddStuff_Click(object sender, RoutedEventArgs e)
        {
            StuffEntry stuffEntryWindow = new StuffEntry();

            if (stuffEntryWindow.ShowDialog() == true)
            {
                double price;
                double.TryParse(stuffEntryWindow.StuffPrice.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out price);
                Lobby? selectedLobby = Lobbies.SelectedItem as Lobby;

                try
                {
                    selectedLobby.stuff.Add(new Stuff(stuffEntryWindow.StuffTitle.Text, stuffEntryWindow.StuffModel.Text, price, stuffEntryWindow.StuffIsTaken.IsChecked, stuffEntryWindow.StuffOwner.Text));
                    Stuffs.Items.Refresh();
                }
                catch (NullReferenceException) { MessageBox.Show("Negalime sukurti jūsų norimos įrangos be taisyklingai sukurto kabineto!"); }
            }
        }

        private void EditStuff_Click(object sender, RoutedEventArgs e)
        {
            Stuff? selectedStuff = Stuffs.SelectedItem as Stuff;
            StuffEntry stuffEntryWindow = new StuffEntry();
            stuffEntryWindow = new StuffEntry(selectedStuff.Names, selectedStuff.Model, selectedStuff.Price, selectedStuff.Taken, selectedStuff.TakenBy);
            stuffEntryWindow.StuffEdit_btn.Content = "Redaguoti";
            
            if (stuffEntryWindow.ShowDialog() == true)
            {
                double price;
                double.TryParse(stuffEntryWindow.StuffPrice.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out price);
                Lobby? selectedLobby = Lobbies.SelectedItem as Lobby;
                int index = selectedLobby.stuff.FindIndex(s => s.Price == selectedStuff.Price);

                try
                {
                    if(index != 1)
                    {
                        selectedLobby.stuff[index] = new Stuff(stuffEntryWindow.StuffTitle.Text, stuffEntryWindow.StuffModel.Text, price, stuffEntryWindow.StuffIsTaken.IsChecked, stuffEntryWindow.StuffOwner.Text);
                        Stuffs.Items.Refresh();
                    }
                    selectedLobby.stuff[index] = new Stuff(stuffEntryWindow.StuffTitle.Text, stuffEntryWindow.StuffModel.Text, price, stuffEntryWindow.StuffIsTaken.IsChecked, stuffEntryWindow.StuffOwner.Text);
                }
                catch(FormatException) { MessageBox.Show("Įvedėte ne skaičių"); }
                Stuffs.Items.Refresh();
            }
        }

            private void RemoveStuff_Click(object sender, RoutedEventArgs e)
            {
                Lobby? selectedLobby = Lobbies.SelectedItem as Lobby;
                Stuff? selectedStuff = Stuffs.SelectedItem as Stuff;
                int index = selectedLobby.stuff.FindIndex(s => s.Price == selectedStuff.Price);

                if (index != -1)
                {
                    selectedLobby.stuff.RemoveAt(index);
                    Stuffs.Items.Refresh();
                }
            }

        private void OfficeLayoutUpdated(object sender, EventArgs e)
        {
            Offices.Columns[0].Visibility = Visibility.Collapsed;
            Offices.Columns[3].Visibility = Visibility.Collapsed;
            Offices.Columns[1].Header = "Pavadinimas";
            Offices.Columns[2].Header = "Adresas";

        }

        private void LobbyLayoutUpdated(object sender, EventArgs e)
        {
            Office? selectedOffice = Offices.SelectedItem as Office;

            try
            {
                LobbyAmount.Content = "Kiekis: " + selectedOffice.lobbies.Count.ToString();
            }
            catch (NullReferenceException) { }
        }

        private void StuffLayoutUpdated(object sender, EventArgs e)
        {
            Lobby? selectedLobby = Lobbies.SelectedItem as Lobby;

            if (selectedLobby?.stuff != null)   
            { 
                StuffAmount.Content = "Kiekis: " + selectedLobby?.stuff.Count.ToString();
            }
        }

    }
}
