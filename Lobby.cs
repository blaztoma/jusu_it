using System;
using System.Collections.Generic;
using System.Text;

namespace Jūsų_IT
{
    public class Lobby
    {
        public int LobbyId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public Lobby()
        {

        }

        public Lobby(int id, string name, string location)
        {
            LobbyId = id;
            Name = name;
            Location = location;
        }
    }
}
