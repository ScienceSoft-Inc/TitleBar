using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ScnTitleBar.Forms;

namespace SampleTitleBar.Views
{
    public class FirstPage: ContentPage
    {
        public FirstPage()
        {
            var titleBar = new TitleBar(this, TitleBar.BarBtnEnum.bbLeftRight);
            titleBar.BarColor = Color.Gray;
            titleBar.Title = "FIRST";

            titleBar.BtnLeft.BackgroundColor = Color.Transparent;
            titleBar.BtnLeft.Source = Device.OnPlatform("Icon/back.png", "ic_2.png", "Assets/Icon/2.png");
            titleBar.BtnLeft.Click += async (s, e) => { await this.Navigation.PushAsync(new SecondPage()); };

            titleBar.BtnRight.BackgroundColor = Color.Transparent;
            titleBar.BtnRight.Source = Device.OnPlatform("Icon/back.png", "ic_3.png", "Assets/Icon/3.png");
            titleBar.BtnRight.Click += async (s, e) => { await this.Navigation.PushAsync(new ThirdPage()); };
           
            var stackLayout = new StackLayout
            {
                BackgroundColor = Color.Yellow,
                Children = 
                {
                    titleBar,
                }
            };

            Content = stackLayout;
        }

    }
}
