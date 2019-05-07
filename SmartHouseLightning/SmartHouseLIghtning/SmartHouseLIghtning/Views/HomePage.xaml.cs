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
    public partial class HomePage : ContentPage
    {
        HomeViewModel viewModel;

        public HomePage()
        {
            InitializeComponent();

            BindingContext = viewModel = new HomeViewModel();

            SetToggleSwitch();

            viewModel.LoadItemsCommand.Execute(null);
        }

        private async void SetToggleSwitch()
        {
            switchAutoMode.IsEnabled = false;

            //get value for App.AutoMode
            string automode = await viewModel.DataStore.GetAutoModeAsync();

            switchAutoMode.IsEnabled = true;


            if (automode == "on")
            {
                App.AutoMode = true;
            }
            else if(automode == "off")
            {
                App.AutoMode = false;
            }
            else
            {
                switchAutoMode.IsEnabled = false;
                DisplayAlert("Alert", "Server error!", "Ok");

            }

            switchAutoMode.IsToggled = App.AutoMode;


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetToggleSwitch();

            viewModel.LoadItemsCommand.Execute(null);
        }

        private async void SwitchAutoMode_Toggled(object sender, ToggledEventArgs e)
        {
            LightsListView.IsEnabled = false;

            string autoModeValue = switchAutoMode.IsToggled ? "on" : "off";

            App.AutoMode = switchAutoMode.IsToggled;


            bool success = await viewModel.DataStore.UpdateAutoModeAsync(autoModeValue);

            LightsListView.IsEnabled = true;

            if (!success)
            {
                switchAutoMode.IsToggled = !switchAutoMode.IsToggled;

                await DisplayAlert("Alert", "Server error!", "Ok");
            }
            else
            {
                LightsListView.IsVisible = !App.AutoMode;
            }
        }

        private async void LightsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            LightsListView.IsEnabled = false;

            var item = e.SelectedItem as Light;

            if(item != null)
            {
                bool success = await viewModel.DataStore.UpdateControllerStateAsync(item.Guid, !item.isEnabled);

                if(success)
                {
                    item.isEnabled = !item.isEnabled;

                    LightsListView.ItemsSource = null;
                    LightsListView.ItemsSource = viewModel.Items;
                }
                else
                {
                    await DisplayAlert("Alert", "Server error!", "OK");
                }
            }

            LightsListView.IsEnabled = true;

            LightsListView.SelectedItem = null;
        }
    }
}