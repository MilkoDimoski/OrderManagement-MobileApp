using PracticeApp.Services;
using PracticeApp.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PracticeApp.Frontend1.ViewModels
{
    public class LoginPageViewModel: INotifyPropertyChanged
    {
        private readonly UserService _userService = new UserService();
        public static int LoggedInUserId { get; private set; }
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
        public ICommand LoginCommand { get; set; }
        public LoginPageViewModel()
        {
            LoginCommand = new Command(async () => await LoginUser());
        }
        public async Task LoginUser()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter username and password", "OK");
                return;
            }
            var user = new User
            {
                Username = Username,
                Password = Password
            };

            try
            {
                var loggedInUser = await _userService.LoginUser(user);
                if (loggedInUser != null)
                {
                    LoggedInUserId = loggedInUser.UserId;
                    await Application.Current.MainPage.DisplayAlert("Success", "Login successful", "OK");
                    await Shell.Current.GoToAsync("//MenuPage");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Invalid credentials", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Login failed: {ex.Message}", "OK");
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
