using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouseLIghtning.Models
{
    public class Esp
    {
        public string ID { get; set; }

        public string Ip { get; set; }

        public EspType Type { get; set; }

        public DateTime? LastTriggered { get; set; }

        public string ToDo { get; set; }


    }

    public enum EspType
    {
        ESP_RECEIVER,
        ESP_CONTROLLER
    }
}
