using NavigationQueryTest.Pages;

namespace NavigationQueryTest
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("param", typeof(ParamPage));
        }
    }
}
