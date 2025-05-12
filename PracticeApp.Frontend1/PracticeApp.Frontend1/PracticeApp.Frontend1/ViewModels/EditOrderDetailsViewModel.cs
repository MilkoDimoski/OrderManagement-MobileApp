using PracticeApp.Services;
using PracticeApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PracticeApp.Frontend1.ViewModels
{
    public class EditOrderDetailsViewModel: INotifyPropertyChanged
    {
        private readonly OrderService _orderService = new OrderService();
        private int? _orderNumber;
        public int? OrderNumber
        {
            get => _orderNumber;
            set
            {
                if (_orderNumber != value)
                {
                    _orderNumber = value;
                    OnPropertyChanged(nameof(OrderNumber));
                }
            }
        }
        private ObservableCollection<OrderDetail> _allDetails;
        public ObservableCollection<OrderDetail> AllDetails
        {
            get => _allDetails;
            set
            {
                if (_allDetails != value)
                {
                    _allDetails = value;
                    OnPropertyChanged(nameof(AllDetails));
                }
            }
        }

        private int _currentIndex;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                if (_currentIndex != value)
                {
                    _currentIndex = value;
                    OnPropertyChanged(nameof(CurrentIndex));
                    UpdateCurrentDetail();

                    RefreshNavigation();
                }
            }
        }
        private OrderDetail _currentDetail;
        public OrderDetail CurrentDetail
        {
            get => _currentDetail;
            set
            {
                if (_currentDetail != value)
                {
                    _currentDetail = value;
                    OnPropertyChanged(nameof(CurrentDetail));
                }
            }
        }


        public bool HasCurrentDetail => CurrentDetail != null;
        public bool CanPrevious => CurrentIndex > 0;
        public bool CanNext => CurrentIndex < AllDetails.Count - 1;
        public string PositionText => AllDetails == null ? "" : $"{CurrentIndex + 1} of {AllDetails.Count}";


        public ICommand LoadDetailsCommand { get; set; }
        public ICommand PreviousCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand SaveDetailCommand { get; set; }
        public ICommand BackToMenuCommand { get; set; }

        public EditOrderDetailsViewModel()
        {
            AllDetails = new ObservableCollection<OrderDetail>();
            LoadDetailsCommand = new Command(async () => await LoadDetails());
            PreviousCommand= new Command(()=>CurrentIndex--, () => CanPrevious);
            NextCommand = new Command(() => CurrentIndex++, () => CanNext);
            SaveDetailCommand= new Command(async () => await SaveCurrentDetail());
            BackToMenuCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("//MenuPage");
            });
        }

        private async Task LoadDetails()
        {
            if (OrderNumber <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter a valid order number", "OK");
                return;
            }
            try
            {
                var orderDetails = await _orderService.GetOrderDetailsByOrderNumber(OrderNumber.Value);
                var order = await _orderService.GetOrderByOrderNumber(OrderNumber.Value);
                if (order.UserId != LoginPageViewModel.LoggedInUserId)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You do not have permission to edit this order", "OK");
                    return;
                }

                if (orderDetails != null && orderDetails.Count >0)
                {
                    AllDetails.Clear();
                    foreach (var detail in orderDetails)
                    {
                        AllDetails.Add(detail);
                    }
                    CurrentIndex = 0;
                    UpdateCurrentDetail();
                }
                else
                {
                    CurrentDetail = null;
                }
                RefreshNavigation();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
        private void UpdateCurrentDetail()
        {
            if (AllDetails != null && AllDetails.Count > 0)
            {
                CurrentDetail = AllDetails[CurrentIndex];
            }
            else
            {
                CurrentDetail = null;
            }
        }
        private async Task SaveCurrentDetail() 
        {
            await _orderService.UpdateOrderDetail(CurrentDetail);
            await Application.Current.MainPage.DisplayAlert("Success", "Order detail updated successfully", "OK");
        }

        private void RefreshNavigation()
        {
            OnPropertyChanged(nameof(CanPrevious));
            OnPropertyChanged(nameof(CanNext));
            OnPropertyChanged(nameof(PositionText));
            OnPropertyChanged(nameof(HasCurrentDetail));
            ((Command)PreviousCommand).ChangeCanExecute();
            ((Command)NextCommand).ChangeCanExecute();
        }
        public void Reset()
        {
            OrderNumber = null;
            AllDetails.Clear();
            CurrentDetail = null;
            CurrentIndex = 0;
            UpdateCurrentDetail();
            RefreshNavigation();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    
    }
}
