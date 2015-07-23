using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ScnTitleBar.Forms;

namespace SampleTitleBar.Views
{
    public class FourthPage : ContentPage
    {
        public FourthPage()
        {
            var titleBar = new TitleBar(this, TitleBar.BarBtnEnum.bbBackRightRight);
            titleBar.BarColor = Color.Gray;
            titleBar.Title = "FOURTH";

            titleBar.BtnBack.Content.BackgroundColor = Color.Transparent;
            titleBar.BtnBack.Source = Device.OnPlatform("Icon/back.png", "ic_back.png", "Assets/Icon/back.png");

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
