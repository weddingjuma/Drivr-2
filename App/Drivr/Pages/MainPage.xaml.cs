using ClassLibrary;

namespace Drivr.Pages
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            var hello = new Hello();
            Label.Text = hello.World;
        }
    }
}