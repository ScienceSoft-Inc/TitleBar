using ScnTitleBar.Forms;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace SampleTitleBar.Views
{
    public class FourthPage : ContentPage
    {
        public FourthPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            BackgroundColor = Color.Green;

            var titleBar = new TitleBar(this, TitleBar.BarBtnEnum.bbBackRightRight)
            {
                BarColor = Color.Gray,
                Title = "FOURTH"
            };

            titleBar.BtnBack.Source = Device.OnPlatform("Icon/back.png", "ic_back.png", "Assets/Icon/back.png");

            var stackLayout = new StackLayout
            {
                Children = 
                {
                    titleBar
                }
            };

            Content = stackLayout;
        }
    }
}
