using SmartHouseLIghtning.Models;
using SmartHouseLIghtning.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouseLIghtning.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnectionsPage : ContentPage
    {
        ConnectionsViewModel viewModel;

        public ConnectionsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ConnectionsViewModel();

            viewModel.LoadItemsCommand.Execute(null);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var connection = args.SelectedItem as Connection;
            if (connection == null)
                return;

            if(App.AutoMode == true)
            {
                await DisplayAlert("Alert", "Auto mode is on! Turn it off before configuring connections.", "OK");
                ConnectionsListView.SelectedItem = null;
                return;
            }
            
            if(connection.Type == EspType.ESP_CONTROLLER)
            {
                await Navigation.PushAsync(new ControllerDetailPage(new ConnectionDetailViewModel(connection, viewModel.LoadItemsCommand)));
            }
            else
            {
                await Navigation.PushAsync(new ReceiverDetailPage(new ConnectionDetailViewModel(connection, viewModel.LoadItemsCommand)));
            }
            

            // Manually deselect item.
            ConnectionsListView.SelectedItem = null;
        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}