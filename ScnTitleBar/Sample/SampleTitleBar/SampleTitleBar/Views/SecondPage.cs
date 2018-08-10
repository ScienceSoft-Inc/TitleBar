using ScnTitleBar.Forms;
using Xamarin.Forms;

namespace SampleTitleBar.Views
{
    public class SecondPage : ContentPage
    {
        public SecondPage()
        {
            var titleBar = new TitleBar(this, TitleBar.BarBtnEnum.bbBackLeftRightRight)
            {
                BarColor = Color.Gray,
                Title = "SECOND"
            };

            titleBar.BtnBack.Source = Device.OnPlatform("Icon/back.png", "ic_back.png", "Assets/Icon/back.png");

            titleBar.BtnLeft.Source = Device.OnPlatform("Icon/3.png", "ic_3.png", "Assets/Icon/3.png");
            titleBar.BtnLeft.Click += (sender, args) => Navigation.PushAsync(new ThirdPage());

            titleBar.BtnRight.Source = Device.OnPlatform("Icon/4.png", "ic_4.png", "Assets/Icon/4.png");
            titleBar.BtnRight.Click += (sender, args) => Navigation.PushAsync(new FourthPage());

            titleBar.BtnRightRight.Source = Device.OnPlatform("Icon/5.png", "ic_5.png", "Assets/Icon/5.png");
            titleBar.BtnRightRight.Click += (sender, args) => Navigation.PushAsync(new FifthPage());

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
