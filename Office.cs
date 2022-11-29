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

        public Office()
        {

        }

        public Office(int id, string name, string location)
        {
            OfficeId = id;
            Name = name;
            Location = location;
        }

    }
}
