using Microsoft.Maui.Controls;

namespace DeckBrew.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Views.HomePage());
        }
    }
}
