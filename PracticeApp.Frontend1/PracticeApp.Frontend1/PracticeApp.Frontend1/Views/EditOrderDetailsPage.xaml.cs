using PracticeApp.Frontend1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticeApp.Frontend1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditOrderDetailsPage : ContentPage
    {
        private readonly EditOrderDetailsViewModel _viewModel;
        public EditOrderDetailsPage()
        {
            InitializeComponent();
            _viewModel = new EditOrderDetailsViewModel();
            BindingContext =_viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Reset();
        }
    }
}