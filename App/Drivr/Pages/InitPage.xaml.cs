using Drivr.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Drivr.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InitPage
    {
        private readonly InitPageViewModel _initPageViewModel = new InitPageViewModel();
        public InitPage()
        {
            InitializeComponent();
            BindingContext = _initPageViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            _initPageViewModel.Status = "Checking authentication...";
            _initPageViewModel.Progress = 0.1;
            var auth = App.Drivr.InitAuthentication();
            await auth.ContinueWith(r =>
            {
                App.Drivr.IsAuthenticated = r.Result;
                _initPageViewModel.Progress = 0.3;

                if (!r.Result)
                {
                    _initPageViewModel.Status = "User not logged in yet";
                    Device.BeginInvokeOnMainThread(async () => //need to invoke the main thread here because xamarin is a bitch
                    {
                        await Navigation.PopAsync(true);
                        await Navigation.PushAsync(new LoginPage(), true);
                    });
                }
                else
                {
                    _initPageViewModel.Status = "Hello " + App.Drivr.CurrentUser.UserName;
                }
            });
        }
    }
}