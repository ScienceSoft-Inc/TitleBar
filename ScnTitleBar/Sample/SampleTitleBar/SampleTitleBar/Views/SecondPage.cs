using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ScnTitleBar.Forms;

namespace SampleTitleBar.Views
{
    public class SecondPage : ContentPage
    {
        public SecondPage()
        {
            var titleBar = new TitleBar(this, TitleBar.BarBtnEnum.bbBackLeftRightRight);
            titleBar.BarColor = Color.Gray;
            titleBar.Title = "SECOND";

            titleBar.BtnBack.BackgroundColor = Color.Transparent;
            titleBar.BtnBack.Source = Device.OnPlatform("Icon/back.png", "ic_back.png", "Assets/Icon/back.png");

            titleBar.BtnLeft.BackgroundColor = Color.Transparent;
            titleBar.BtnLeft.Source = Device.OnPlatform("Icon/3.png", "ic_3.png", "Assets/Icon/3.png");
            titleBar.BtnLeft.Click += (s, e) => { this.Navigation.PushAsync(new ThirdPage()); };

            titleBar.BtnRight.BackgroundColor = Color.Transparent;
            titleBar.BtnRight.Source = Device.OnPlatform("Icon/4.png", "ic_4.png", "Assets/Icon/4.png");
            titleBar.BtnRight.Click += (s, e) => { this.Navigation.PushAsync(new FourthPage()); };

            titleBar.BtnRightRight.BackgroundColor = Color.Transparent;
            titleBar.BtnRightRight.Source = Device.OnPlatform("Icon/5.png", "ic_5.png", "Assets/Icon/5.png");
            titleBar.BtnRightRight.Click += (s, e) => { this.Navigation.PushAsync(new FifthPage()); };

            var stackLayout = new StackLayout
            {
                BackgroundColor = Color.Green,
                Children = 
                {
                    titleBar,
                }
            };

            Content = stackLayout;
        }
    }
}
