using SmartHouseLIghtning.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouseLIghtning.ViewModels
{
    public class RoomDetailViewModel : BaseViewModel
    {
        public Room Room { get; set; }
        public RoomDetailViewModel(Room item = null)
        {
            Title = item?.Name;
            Room = item;
        }
    }
}
