using PracticeApp.Frontend1;
using PracticeApp.Services;
using PracticeApp.Services.Models;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PracticeApp.Frontend1.ViewModels
{
    public class RegisterPageViewModel:INotifyPropertyChanged 
    {
        private readonly UserService _userService=new UserService();
        public string username { get; set; }
        public string Username
        {
            get => username;
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }
        public string password { get; set; }
        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public string confirmPassword { get; set; }
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
                    OnPropertyChanged(nameof(ConfirmPassword));
                }
            }
        }
        public ICommand RegisterCommand { get; set; }
        public RegisterPageViewModel()
        {
            RegisterCommand = new Command(async () => await RegisterUser());
        }

        public async Task RegisterUser()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Username and Password cannot be empty", "OK");
                return;
            }
            var user = new User
            {
                Username = Username,
                Password = Password
            };
            if (Password != ConfirmPassword)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Passwords do not match", "OK");
                return;
            }

            try
            {
                await _userService.AddUser(user);
                await App.Current.MainPage.DisplayAlert("Success", "User registered successfully", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
