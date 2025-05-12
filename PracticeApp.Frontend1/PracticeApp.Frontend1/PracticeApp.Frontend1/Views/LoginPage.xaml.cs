using PracticeApp.Frontend1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticeApp.Frontend1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginPageViewModel();
        }
    }
}