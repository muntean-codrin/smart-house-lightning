using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouseLIghtning.Models
{
    public class Connection
    {
        public string Name { get; set; }

        public string Guid { get; set; }

        public EspType Type { get; set; }

        public string Detail
        {
            get
            {
                if(Type ==EspType.ESP_RECEIVER)
                {
                    if(string.IsNullOrEmpty(Room1Name) || string.IsNullOrEmpty(Room2Name))
                        return "Not configured";
                    return $"{Room1Name} -> {Room2Name}";
                }
                else
                {
                    if (string.IsNullOrEmpty(Room1Name))
                        return "Not configured";
                    return Room1Name;
                }

            }
        }

        public string Room1Name { get; set; }

        public int Room1ID { get; set; }

        public string Room2Name { get; set; }

        public int Room2ID { get; set; }

        public string Image => Type == EspType.ESP_CONTROLLER ? "light.png" : "sensor.png";

    }
}
