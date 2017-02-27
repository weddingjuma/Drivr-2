using Xamarin.Forms;

namespace Drivr.Pages
{
    public partial class MainPage
    {
        public static INavigation Nav;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            Nav = Navigation;
            await Nav.PushAsync(new InitPage());
        }
    }
}