﻿using Drivr.Pages;
using Xamarin.Forms;

namespace Drivr
{
    public partial class App
    {
        public static Drivr Drivr;
        public App()
        {
            InitializeComponent();
            Drivr = new Drivr();
            MainPage = Drivr.IsAuthenticated ? (Page) new MainPage() : new LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}