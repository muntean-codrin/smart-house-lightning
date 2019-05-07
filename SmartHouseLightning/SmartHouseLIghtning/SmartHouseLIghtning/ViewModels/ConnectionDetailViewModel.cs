using SmartHouseLIghtning.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartHouseLIghtning.ViewModels
{
    public class ConnectionDetailViewModel : BaseViewModel
    {
        public Connection Connection { get; set; }

        public Command _reloadList;

        public ConnectionDetailViewModel(Connection item = null, Command reloadList = null)
        {
            Title = item?.Name;
            Connection = item;

            _reloadList = reloadList;


        }
    }
}
