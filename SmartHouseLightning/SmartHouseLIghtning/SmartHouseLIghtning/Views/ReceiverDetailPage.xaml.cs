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
    public partial class ReceiverDetailPage : ContentPage
    {
        ConnectionDetailViewModel viewModel;

        IEnumerable<Room> rooms = new List<Room>();

        public ReceiverDetailPage(ConnectionDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

            FillPicker();
        }

        async void FillPicker()
        {
            rooms = await viewModel.DataStore.GetRoomsAsync(true);

            RoomPicker1.ItemsSource = rooms.Select(s => s.Name).ToList();

            RoomPicker1.SelectedItem = rooms.FirstOrDefault(s => s.ID == viewModel.Connection.Room1ID)?.Name;


            RoomPicker2.ItemsSource = rooms.Select(s => s.Name).ToList();

            RoomPicker2.SelectedItem = rooms.FirstOrDefault(s => s.ID == viewModel.Connection.Room2ID)?.Name;
        }


        private async void Locate_Clicked(object sender, EventArgs e)
        {
            if (App.AutoMode == true)
            {
                await DisplayAlert("Alert", "Auto mode is on! Turn it off before locating a receiver.", "OK");
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

        private void Swap_Clicked(object sender, EventArgs e)
        {
            var x = RoomPicker1.SelectedItem;

            RoomPicker1.SelectedItem = RoomPicker2.SelectedItem;

            RoomPicker2.SelectedItem = x;

            FillInfoLabel();
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (App.AutoMode == true)
            {
                await DisplayAlert("Alert", "Auto mode is on! Turn it off before saving.", "OK");
                return;
            }

            var room1 = rooms.FirstOrDefault(s => s.Name == RoomPicker1.SelectedItem.ToString());
            var room2 = rooms.FirstOrDefault(s => s.Name == RoomPicker2.SelectedItem.ToString());


            viewModel.Connection.Room1ID = room1.ID;
            viewModel.Connection.Room1Name = room1.Name;

            viewModel.Connection.Room2ID = room2.ID;
            viewModel.Connection.Room2Name = room2.Name;

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

        private void RoomPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(RoomPicker1.SelectedItem != null && RoomPicker2.SelectedItem != null)
            {
                FillInfoLabel();
                buttonsStackLayout.IsVisible = true;
            }
        }

        private void FillInfoLabel()
        {
            infoLabel.Text = $"The receiver is closer to the \"{RoomPicker1.SelectedItem}\" than the \"{RoomPicker2.SelectedItem}\". Is that correct?";
        }
    }
}