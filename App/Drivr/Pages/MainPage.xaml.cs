using ClassLibrary;

namespace Drivr.Pages
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            Hello hello = new Hello();
            Label.Text = hello.World;
        }
    }
}
