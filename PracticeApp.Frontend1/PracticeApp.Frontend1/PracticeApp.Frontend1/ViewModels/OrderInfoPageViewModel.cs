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
    public class OrderInfoPageViewModel : INotifyPropertyChanged
    {
        private readonly OrderService _orderService = new OrderService();
        private int _orderNumber;
        private Order _currentOrder;
        public Order CurrentOrder
        {
            get => _currentOrder;
            set
            {
                if (_currentOrder != value)
                {
                    _currentOrder = value;
                    OnPropertyChanged(nameof(CurrentOrder));
                }
            }
        }
        public int OrderNumber
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
        private ObservableCollection<OrderDetail> _orderDetails;
        public ObservableCollection<OrderDetail> OrderDetails
        {
            get => _orderDetails;
            set
            {
                if (_orderDetails != value)
                {
                    _orderDetails = value;
                    OnPropertyChanged(nameof(OrderDetails));
                }
            }
        }
        public ICommand GetOrderDetailsCommand { get; set; }
        public ICommand BackToMenuCommand { get; set; }

        public OrderInfoPageViewModel()
        {
            OrderDetails = new ObservableCollection<OrderDetail>();
            GetOrderDetailsCommand = new Command(async () => await GetOrderDetails());
            BackToMenuCommand = new Command(async () => Shell.Current.GoToAsync("//MenuPage"));
        }

        private async Task GetOrderDetails()
        {
            if (OrderNumber <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter a valid order number", "OK");
                return;
            }
            try
            {
                var order = await _orderService.GetOrderByOrderNumber(OrderNumber);
                if (order != null)
                {
                    if (order.UserId != LoginPageViewModel.LoggedInUserId)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "You do not have permission to view this order", "OK");
                        return;
                    }
                    CurrentOrder = order;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No order found for the given order number", "OK");
                    return;
                }

                    var orderDetails = await _orderService.GetOrderDetailsByOrderNumber(OrderNumber);
                if (orderDetails != null && orderDetails.Count > 0)
                {
                    OrderDetails = new ObservableCollection<OrderDetail>(orderDetails);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No order details found for the given order number", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
        public void Reset()
        {
            OrderNumber = 0;
            CurrentOrder = null;
            OrderDetails.Clear();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
