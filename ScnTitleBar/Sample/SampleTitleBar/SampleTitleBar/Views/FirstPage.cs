using ScnTitleBar.Forms;
using Xamarin.Forms;

namespace SampleTitleBar.Views
{
    public class FirstPage: ContentPage
    {
        public FirstPage()
        {
            var titleBar = new TitleBar(this, TitleBar.BarBtnEnum.bbLeftRight)
            {
                BarColor = Color.Gray,
                Content = new Entry
                {
                    VerticalOptions = LayoutOptions.Center,
                    Placeholder = "Search text...",
                }
            };

            titleBar.BtnLeft.BackgroundColor = Color.Blue;
            titleBar.BtnLeft.Source = Device.OnPlatform("Icon/2.png", "ic_2.png", "Assets/Icon/2.png");
            titleBar.BtnLeft.Click += (sender, args) => Navigation.PushAsync(new SecondPage());

            titleBar.BtnRight.BackgroundColor = Color.Blue;
            titleBar.BtnRight.Source = Device.OnPlatform("Icon/3.png", "ic_3.png", "Assets/Icon/3.png");
            titleBar.BtnRight.Click += (sender, args) => Navigation.PushAsync(new ThirdPage());
           
            var stackLayout = new StackLayout
            {
                BackgroundColor = Color.Yellow,
                Children = 
                {
                    titleBar
                }
            };

            Content = stackLayout;
        }
    }
}
