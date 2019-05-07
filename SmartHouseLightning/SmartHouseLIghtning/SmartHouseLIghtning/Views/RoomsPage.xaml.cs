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
    public partial class RoomsPage : ContentPage
    {
        RoomsViewModel viewModel;

        public RoomsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new RoomsViewModel();

            viewModel.LoadItemsCommand.Execute(null);
        }

        private async void AddRoom_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewRoomPage());
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var room = args.SelectedItem as Room;
            if (room == null)
                return;

            await Navigation.PushAsync(new RoomDetailPage(new RoomDetailViewModel(room)));

            // Manually deselect item.
            RoomsListView.SelectedItem = null;
        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}