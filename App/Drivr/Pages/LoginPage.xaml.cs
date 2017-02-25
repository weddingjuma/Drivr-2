using System;
using Drivr.ViewModels;
using Xamarin.Forms;

namespace Drivr.Pages
{
    public partial class LoginPage
    {
        private readonly LoginPageViewModel _loginPageViewModel;
        public LoginPage()
        {
            InitializeComponent();
            _loginPageViewModel = new LoginPageViewModel();
            BindingContext = _loginPageViewModel;
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            _loginPageViewModel.Status = await App.Drivr.Authenticate(_loginPageViewModel.Username, _loginPageViewModel.Password);
        }
    }
}