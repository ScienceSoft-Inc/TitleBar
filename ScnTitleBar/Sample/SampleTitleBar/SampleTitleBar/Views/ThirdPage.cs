using ScnTitleBar.Forms;
using Xamarin.Forms;

namespace SampleTitleBar.Views
{
    public class ThirdPage : ContentPage
    {
        public ThirdPage()
        {
            var titleBarTop = new TitleBar(this, TitleBar.BarBtnEnum.bbBack)
            {
                BarColor = Color.Gray,
                Title = "THIRD"
            };

            titleBarTop.BtnBack.Source = Device.OnPlatform("Icon/back.png", "ic_back.png", "Assets/Icon/back.png");

            var titleBarBottom =
                new TitleBar(this, TitleBar.BarBtnEnum.bbLeftRight, TitleBar.BarAlignEnum.baBottom)
                {
                    BarColor = Color.Transparent
                };

            titleBarBottom.BtnLeft.Source = Device.OnPlatform("Icon/4.png", "ic_4.png", "Assets/Icon/4.png");
            titleBarBottom.BtnLeft.Click += (sender, args) => Navigation.PushAsync(new FourthPage());

            titleBarBottom.BtnRight.Source = Device.OnPlatform("Icon/5.png", "ic_5.png", "Assets/Icon/5.png");
            titleBarBottom.BtnRight.Click += (sender, args) => Navigation.PushAsync(new FifthPage());

            var relativeLayout = new RelativeLayout
            { 
                BackgroundColor = Color.Blue
            };

            relativeLayout.Children.Add(titleBarTop,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent(parent => parent.Width));

            relativeLayout.Children.Add(titleBarBottom,
                Constraint.Constant(0),
                Constraint.RelativeToParent(parent => parent.Height - titleBarBottom.HeightBar),
                Constraint.RelativeToParent(parent => parent.Width));

            Content = relativeLayout;
        }
    }
}
