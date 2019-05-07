using SmartHouseLIghtning.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartHouseLIghtning.ViewModels
{
    class ConnectionsViewModel : BaseViewModel
    {
        public ObservableCollection<Connection> Items { get; set; }

        public Command LoadItemsCommand { get; set; }

        public ConnectionsViewModel()
        {
            Title = "Connections";
            Items = new ObservableCollection<Connection>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetConnectionsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
