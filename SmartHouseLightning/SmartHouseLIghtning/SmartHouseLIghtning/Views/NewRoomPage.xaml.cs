using SmartHouseLIghtning.Models;
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
    public partial class NewRoomPage : ContentPage
    {
        public Room Room { get; set; }
        public NewRoomPage()
        {
            InitializeComponent();

            Room = new Room();

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddRoom", Room);
            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}