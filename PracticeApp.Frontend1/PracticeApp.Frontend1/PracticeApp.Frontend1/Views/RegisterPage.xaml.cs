using System;
using PracticeApp.Frontend1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticeApp.Frontend1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new RegisterPageViewModel();
        }

        private async void OnLoginTapped(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
    }
}
