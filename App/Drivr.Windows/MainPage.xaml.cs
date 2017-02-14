namespace Drivr.Windows
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoadApplication(new Drivr.App());
        }
    }
}
