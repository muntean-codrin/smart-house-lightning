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
    public partial class RoomDetailPage : ContentPage
    {
        RoomDetailViewModel viewModel;
        public RoomDetailPage(RoomDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public RoomDetailPage()
        {
            InitializeComponent();

            var item = new Room();

            viewModel = new RoomDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}