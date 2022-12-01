using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jūsų_IT
{
    public class Office
    {
        public int OfficeId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Lobby> Lobbies { get; set; }

        public Office()
        {

        }

        public Office(int id, string name, string location)
        {
            OfficeId = id;
            Name = name;
            Location = location;
            Lobbies = new List<Lobby>();

            PopulateLobbies();
        }

        public void PopulateLobbies()
        {
            Lobbies.Clear();
            Lobbies.Add(new Lobby("Kaunas office Lobby " + OfficeId.ToString(), "Student str. 50"));
            Lobbies.Add(new Lobby("Klaipeda office Lobby " + OfficeId.ToString(), "Pilies str. 50"));
            Lobbies.Add(new Lobby("Veisiejai Lobby " + OfficeId.ToString(), "Dariaus ir Gireno str. 30"));
        }

    }
}
