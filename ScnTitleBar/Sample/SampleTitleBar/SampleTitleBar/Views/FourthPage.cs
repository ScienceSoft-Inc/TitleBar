using ScnTitleBar.Forms;
using Xamarin.Forms;

namespace SampleTitleBar.Views
{
    public class FourthPage : ContentPage
    {
        public FourthPage()
        {
            var titleBar = new TitleBar(this, TitleBar.BarBtnEnum.bbBackRightRight)
            {
                BarColor = Color.Gray,
                Title = "FOURTH"
            };

            titleBar.BtnBack.Source = Device.OnPlatform("Icon/back.png", "ic_back.png", "Assets/Icon/back.png");

            var stackLayout = new StackLayout
            {
                BackgroundColor = Color.Green,
                Children = 
                {
                    titleBar
                }
            };

            Content = stackLayout;
        }
    }
}
