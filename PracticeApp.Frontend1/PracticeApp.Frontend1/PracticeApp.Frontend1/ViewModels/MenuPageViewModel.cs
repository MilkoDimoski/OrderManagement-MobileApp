using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PracticeApp.Frontend1.ViewModels
{
    public class MenuPageViewModel
    {
        public ICommand ViewOrderInfo { get; set; }
        public ICommand EditOrderDetails { get; set; }   

        public MenuPageViewModel()
        {
            ViewOrderInfo = new Command(async () => await OnViewOrderInfo());
            EditOrderDetails = new Command(async () => await OnEditOrderDetails());
        }

        private async Task OnViewOrderInfo()
        {

            await Shell.Current.GoToAsync("//OrderInfoPage");
        }
        private async Task OnEditOrderDetails()
        {
            await Shell.Current.GoToAsync("//EditOrderDetailsPage");
        }
    }
}
