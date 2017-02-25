using System;
using Drivr.ViewModels;

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

        private void Button_OnClicked(object sender, EventArgs e)
        {
            App.Drivr.Authenticate(_loginPageViewModel.Username, _loginPageViewModel.Password);
        }
    }
}