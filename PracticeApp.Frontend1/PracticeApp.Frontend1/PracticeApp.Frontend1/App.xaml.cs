﻿using PracticeApp.Services;
using PracticeApp.Frontend1;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticeApp.Frontend1
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
