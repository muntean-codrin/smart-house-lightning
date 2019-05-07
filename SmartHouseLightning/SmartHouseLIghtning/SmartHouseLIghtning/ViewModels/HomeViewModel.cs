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
    class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<Light> Items { get; set; }

        public Command LoadItemsCommand { get; set; }

        public HomeViewModel()
        {
            Title = "Home";
            Items = new ObservableCollection<Light>();

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
                var items = await DataStore.GetControllersAsync(true);
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
