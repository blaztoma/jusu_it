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

            //PopulateStuff();
        }

        public void PopulateStuff(string name, string model, double cost, bool IsRented, string Owner)
        {
            stuff.Clear();
            stuff.Add(new Stuff(name, model, cost, IsRented, Owner));
            stuff.Add(new Stuff("Name", "Model", 15.2, false, ""));
            stuff.Add(new Stuff("Name", "Model", 15.2, false, ""));
            
        }
    }
}
