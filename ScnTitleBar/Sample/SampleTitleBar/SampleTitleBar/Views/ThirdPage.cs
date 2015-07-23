using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ScnTitleBar.Forms;

namespace SampleTitleBar.Views
{
    public class ThirdPage : ContentPage
    {
        public ThirdPage()
        {
            var titleBarTop = new TitleBar(this, TitleBar.BarBtnEnum.bbBack, TitleBar.BarAlignEnum.baTop);
            titleBarTop.BarColor = Color.Gray;
            titleBarTop.Title = "THIRD";

            titleBarTop.BtnBack.Content.BackgroundColor = Color.Transparent;
            titleBarTop.BtnBack.Source = Device.OnPlatform("Icon/back.png", "ic_back.png", "Assets/Icon/back.png");

            var titleBarBottom = new TitleBar(this, TitleBar.BarBtnEnum.bbLeftRight, TitleBar.BarAlignEnum.baBottom);
            titleBarBottom.BarColor = Color.Transparent;

            titleBarBottom.BtnLeft.Content.BackgroundColor = Color.Gray;
            titleBarBottom.BtnLeft.Source = Device.OnPlatform("Icon/4.png", "ic_4.png", "Assets/Icon/4.png");
            titleBarBottom.BtnLeft.Click += (s, e) => { this.Navigation.PushAsync(new FourthPage()); };

            titleBarBottom.BtnRight.Content.BackgroundColor = Color.Gray;
            titleBarBottom.BtnRight.Source = Device.OnPlatform("Icon/5.png", "ic_5.png", "Assets/Icon/5.png");
            titleBarBottom.BtnRight.Click += (s, e) => { this.Navigation.PushAsync(new FifthPage()); };

            var relativeLayout = new RelativeLayout
            { 
                BackgroundColor = Color.Blue,
            };

            relativeLayout.Children.Add(titleBarTop,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent(parent => { return parent.Width; }));

            relativeLayout.Children.Add(titleBarBottom,
                Constraint.Constant(0),
                Constraint.RelativeToParent(parent => { return parent.Height - titleBarBottom.HeightBar; }),
                Constraint.RelativeToParent(parent => { return parent.Width; }));

            Content = relativeLayout;
        }
    }
}
