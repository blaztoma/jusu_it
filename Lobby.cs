using System;
using System.Collections.Generic;
using System.Text;

namespace Jūsų_IT
{
    public class Lobby
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public List<Stuff> stuff { get; set; }

        public Lobby()
        {

        }

        public Lobby(string name, int number)
        {
            Name = name;
            Number = number;
            stuff = new List<Stuff>();

            PopulateStuff();
        }

        public void PopulateStuff()
        {
            stuff.Clear();
            stuff.Add(new Stuff("Name", "Model", "12", false));
            stuff.Add(new Stuff("Name", "Model", "15.5", false));
            //stuff.Add(new Stuff("Name", "Model", 15.2, false, ""));
            
        }
    }
}
