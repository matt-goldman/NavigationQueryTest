namespace NavigationQueryTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnNavClicked(object sender, EventArgs e)
        {
            var args = new Dictionary<string, object>
            {
                { "param", "Hello from MainPage" }
            };

            await Shell.Current.GoToAsync("param", args);
        }
    }

}
