using System;
using System.Collections.Generic;
using System.Text;

namespace Jūsų_IT
{
    public class Lobby
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Stuff> Stuff { get; set; }

        public Lobby()
        {

        }

        public Lobby(string name, string location)
        {
            Name = name;
            Location = location;
            Stuff = new List<Stuff>();

            PopulateStuff();
        }

        public void PopulateStuff()
        {
            Stuff.Clear();
            Stuff.Add(new Stuff("Name", "Model", 15.2, false, ""));
            Stuff.Add(new Stuff("Name", "Model", 15.2, false, ""));
            Stuff.Add(new Stuff("Name", "Model", 15.2, false, ""));
        }
    }
}
