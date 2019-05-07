using SmartHouseLIghtning.Models;
using SmartHouseLIghtning.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartHouseLIghtning.ViewModels
{
    class RoomsViewModel : BaseViewModel
    {
        public ObservableCollection<Room> Items { get; set; }

        public Command LoadItemsCommand { get; set; }

        public RoomsViewModel()
        {
            Title = "Rooms";

            Items = new ObservableCollection<Room>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewRoomPage, Room>(this, "AddRoom", async (obj, room) =>
            {
                var newRoom = room as Room;
                Items.Add(newRoom);
                await DataStore.AddRoomAsync(newRoom);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetRoomsAsync(true);
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
