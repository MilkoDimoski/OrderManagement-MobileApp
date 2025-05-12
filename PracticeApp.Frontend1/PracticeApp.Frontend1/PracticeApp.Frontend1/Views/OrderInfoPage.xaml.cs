using PracticeApp.Frontend1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticeApp.Frontend1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderInfoPage : ContentPage
    {
        private readonly OrderInfoPageViewModel _viewModel;
        public OrderInfoPage()
        {
            InitializeComponent();
            _viewModel = new OrderInfoPageViewModel();
            BindingContext =_viewModel;
        }
        private async void OnGetOrderDetailsClicked(object sender, EventArgs e)
        {
            if(!int.TryParse(OrderNumberEntry.Text, out int orderNumber))
            {
                await DisplayAlert("Error", "Please enter a valid order number", "OK");
                return;
            }
            _viewModel.OrderNumber = orderNumber;
            _viewModel.GetOrderDetailsCommand.Execute(null);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            OrderNumberEntry.Text = string.Empty;
            _viewModel.Reset();
        }
    }
}