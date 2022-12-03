using System;
using System.Collections.Generic;
using System.Text;

namespace Jūsų_IT
{
    public class Stuff
    {
        public string Names { get; set; }
        public string Model { get; set; }
        public string Price { get; set; }
        public bool Taken { get; set; }
        public string TakenBy { get; set; }

        public Stuff()
        {

        }

        public Stuff(string name, string model, string price, bool taken)
        {
            Names = name;
            Model = model;
            Price = price;
            Taken = taken;
            //TakenBy = takenby;
        }
    }
}
