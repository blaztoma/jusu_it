using System;
using System.Collections.Generic;
using System.Text;

namespace Jūsų_IT
{
    public class Stuff
    {
        public string Names { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public bool Taken { get; set; }
        public string TakenBy { get; set; }

        public Stuff()
        {

        }

        public Stuff(string name, string model, double price, bool? taken, string takenby)
        {
            Names = name;
            Model = model;
            Price = price;
            Taken = (bool)taken;
            TakenBy = takenby;
        }
    }
}
