using SampleTitleBar.Views;
using Xamarin.Forms;

namespace SampleTitleBar
{
    public class App : Application
	{
		public App ()
		{
            MainPage = new NavigationPage(new FirstPage());
		}
	}
}
