using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Jūsų_IT
{
    public class Office
    {
        public int OfficeId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Lobby> lobbies { get; set; }
        
        public Office()
        {

        }

        public Office(int id, string name, string location)
        {
            OfficeId = id;
            Name = name;
            Location = location;

            lobbies = new List<Lobby>();
            PopulateLobbies();
        }

        public void PopulateLobbies()
        {
            lobbies.Clear();
            lobbies.Add(new Lobby("Klaipeda office Lobby " + OfficeId.ToString(), 1));
        }

    }
}
