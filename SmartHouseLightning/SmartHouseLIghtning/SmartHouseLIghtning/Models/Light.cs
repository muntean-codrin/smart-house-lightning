using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouseLIghtning.Models
{
    public class Light
    {
        public string Name { get; set; }

        public string Room { get; set; }

        public string Guid { get; set; }

        public bool isEnabled { get; set; }

        public string Image => isEnabled ? "light_on.png" : "light.png";
    }
}
