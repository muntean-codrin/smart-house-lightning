using SmartHouseLIghtning.Models;
using SmartHouseLIghtning.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouseLIghtning.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControllerDetailPage : ContentPage
    {
        ConnectionDetailViewModel viewModel;

        IEnumerable<Room> rooms = new List<Room>();
        public ControllerDetailPage(ConnectionDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

            FillPicker();
        }

        async void FillPicker()
        {
            rooms = await viewModel.DataStore.GetRoomsAsync(true);

            RoomPicker.ItemsSource = rooms.Select(s => s.Name).ToList();

            RoomPicker.SelectedItem = rooms.FirstOrDefault(s => s.ID == viewModel.Connection.Room1ID)?.Name;
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

        }

        private async void Locate_Clicked(object sender, EventArgs e)
        {
            if (App.AutoMode == true)
            {
                await DisplayAlert("Alert", "Auto mode is on! Turn it off before locating a controller.", "OK");
                return;
            }

            bool success = await viewModel.DataStore.LocateEspAsync(viewModel.Connection.Guid);

            if (success)
            {
                locateButton.IsEnabled = false;
                await Task.Delay(5000);
                locateButton.IsEnabled = true;

            }
            else
            {
                await DisplayAlert("Alert", "Unknow error occured.", "Ok");
            }
        }
        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (App.AutoMode == true)
            {
                await DisplayAlert("Alert", "Auto mode is on! Turn it off before saving.", "OK");
                return;
            }

            if (RoomPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Alert", "Select a room from the list before saving.", "Ok");
                return;
            }

            var room = rooms.FirstOrDefault(s => s.Name == RoomPicker.SelectedItem.ToString());

            viewModel.Connection.Room1ID = room.ID;
            viewModel.Connection.Room1Name = room.Name;

            bool success = await viewModel.DataStore.UpdateConnectionAsync(viewModel.Connection);

            if (!success)
            {
                await DisplayAlert("Alert", "Unknow error occured.", "Ok");
            }
            else
            {
                viewModel._reloadList?.Execute(null);
                await Navigation.PopAsync();
            }


        }
    }
}