using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouseLIghtning.Models
{
    public class Room
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string NumberOfPeople { get; set; }
    }
}
