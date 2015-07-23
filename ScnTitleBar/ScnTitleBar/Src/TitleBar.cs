using System;
using Xamarin.Forms;
using ScnViewGestures.Plugin.Forms;

namespace ScnTitleBar.Forms
{
    public class TitleBar : AbsoluteLayout 
    {
        /* Bar template 
         * 
         * [BtnBack] [BtnLeftLeft] [BtnLeft] [TITLE] [BtnRight] [BtnRightRight]
         * 
        */


        [Flags]
        public enum BarBtnEnum
        {
            bbNone = 0,
            bbBack = 1,
            bbLeft = 2,
            bbLeftLeft = 4,
            bbRight = 8,
            bbRightRight = 16,
            bbMore = 32, //TODO

            bbLeftRight = bbLeft | bbRight,
            bbLeftRightRight = bbLeft | bbRightRight,
            bbLeftLeftRight = bbLeftLeft | bbRight,
            bbLeftLeftRightRight = bbLeftLeft | bbRightRight,

            bbBackLeft = bbBack | bbLeft,
            bbBackRight = bbBack | bbRight,
            bbBackLeftLeft = bbBack | bbLeftLeft,
            bbBackRightRight = bbBack | bbRightRight,

            bbBackLeftRight = bbBackLeft | bbRight,
            bbBackLeftLeftRight = bbBackLeftLeft | bbRight,
            bbBackLeftRightRight = bbBackLeft | bbRightRight,
            bbBackLeftLeftRightRight = bbBackLeftLeft | bbRightRight
        }

        [Flags]
        public enum BarAlignEnum
        {
            baTop = 0,
            baBottom = 1
        }

        public int HeightBar = Device.OnPlatform(48, 48, 64);
        private int PaddingBar = 0;

        public TitleBar(Page page, BarBtnEnum barBtn = BarBtnEnum.bbNone, BarAlignEnum barAlign = BarAlignEnum.baTop)
        {
            NavigationPage.SetHasNavigationBar(page, false);
            NavigationPage.SetHasBackButton(page, false);
            page.Title = "";
            page.Appearing += (s, e) => { NavigationPage.SetHasNavigationBar(page, false); };
            page.Disappearing += (s, e) => { NavigationPage.SetHasNavigationBar(page, false); };

            if ((Device.OS == TargetPlatform.iOS) && (barAlign == BarAlignEnum.baTop))
                PaddingBar = 20;

            BackgroundColor = barColor;
            MinimumHeightRequest = HeightBar + PaddingBar;
            HeightRequest = HeightBar + PaddingBar;
            appBar.BackgroundColor = barColor;
            appBar.Padding = new Thickness(0, PaddingBar, 0, 0);
            appBar.MinimumHeightRequest = HeightRequest;
            appBar.HeightRequest = HeightRequest;
            
            boxPadding.BackgroundColor = barColor;

            #region Title create
            txtTitle = new Label 
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            AbsoluteLayout.SetLayoutFlags(txtTitle, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(txtTitle,
                new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)
            );
            appBar.Children.Add(txtTitle);
            #endregion

            #region Panel for left buttons
            var stackLeftBtn = new StackLayout
            {
                Padding = new Thickness (0),
                Spacing = 0,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Start
            };
            SetLayoutFlags(stackLeftBtn, AbsoluteLayoutFlags.PositionProportional);
            SetLayoutBounds(stackLeftBtn,
                new Rectangle(0, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            appBar.Children.Add(stackLeftBtn);
            #endregion

            #region Panel for right buttons
            var stackRightBtn = new StackLayout
            {
                Padding = new Thickness(0),
                Spacing = 0,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.End
            };

            AbsoluteLayout.SetLayoutFlags(stackRightBtn, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(stackRightBtn,
                new Rectangle(1, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            appBar.Children.Add(stackRightBtn);
            #endregion

            #region Back button
            BtnBack = new BackImageButton(page);
            BtnBack.WidthRequest = HeightBar;
            BtnBack.HeightRequest = HeightBar;

            if ((barBtn & BarBtnEnum.bbBack) != 0)
                stackLeftBtn.Children.Add(BtnBack);
            #endregion

            #region LeftLeft button
            BtnLeftLeft = new ImageButton();
            BtnLeftLeft.WidthRequest = HeightBar;
            BtnLeftLeft.HeightRequest = HeightBar;

            if ((barBtn & BarBtnEnum.bbLeftLeft) != 0)
                stackLeftBtn.Children.Add(BtnLeftLeft);
            #endregion

            #region Left button
            BtnLeft = new ImageButton();
            BtnLeft.WidthRequest = HeightBar;
            BtnLeft.HeightRequest = HeightBar;

            if (((barBtn & BarBtnEnum.bbLeft) != 0) || ((barBtn & BarBtnEnum.bbLeftLeft) != 0))
                stackLeftBtn.Children.Add(BtnLeft);
            #endregion

            #region Right button
            BtnRight = new ImageButton();
            BtnRight.WidthRequest = HeightBar;
            BtnRight.HeightRequest = HeightBar;

            if (((barBtn & BarBtnEnum.bbRight) != 0) || ((barBtn & BarBtnEnum.bbRightRight) != 0))
                stackRightBtn.Children.Add(BtnRight);
            #endregion

            #region RightRight button
            BtnRightRight = new ImageButton();
            BtnRightRight.WidthRequest = HeightBar;
            BtnRightRight.HeightRequest = HeightBar;

            if ((barBtn & BarBtnEnum.bbRightRight) != 0)
                stackRightBtn.Children.Add(BtnRightRight);
            #endregion

            AbsoluteLayout.SetLayoutFlags(appBar, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(appBar, new Rectangle(0f, 0f, 1f, 1f));
            Children.Add(appBar);

            if ((Device.OS == TargetPlatform.iOS) && (barAlign == BarAlignEnum.baTop))
            {
                AbsoluteLayout.SetLayoutFlags(boxPadding, AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(boxPadding,
                    new Rectangle(0, 0, 600, PaddingBar));
                Children.Add(boxPadding);
            }
        }

        private AbsoluteLayout appBar = new AbsoluteLayout();
        private BoxView boxPadding = new BoxView();
        public BoxView BoxPadding { get { return boxPadding; } }

        #region Background color
        private Color barColor = Color.White;
        public Color BarColor
        {
            get { return barColor; }
            set
            {
                barColor = value;
                BackgroundColor = barColor;
                appBar.BackgroundColor = barColor;
                boxPadding.BackgroundColor = barColor;
            }
        }
        #endregion

        #region Title label
        private Label txtTitle;
        private string title = "";
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                txtTitle.Text = title;
            }
        }

        public Style TitleStyle
        {
            get { return txtTitle.Style; }
            set { txtTitle.Style = value; }
        }
        #endregion

        public ImageButton BtnBack { get; private set; }
        public ImageButton BtnRight { get;  private set; }
        public ImageButton BtnRightRight { get; private set; }
        public ImageButton BtnLeft { get; private set; }
        public ImageButton BtnLeftLeft { get; private set; }
    
        private class BackImageButton : ImageButton
        {
            private Page curPage;
            public BackImageButton (Page page)
            {
                curPage = page;
            }
            public override void OnClick()
            {
                base.OnClick();

                if (curPage.Navigation.NavigationStack.Count > 0)
                    curPage.Navigation.PopAsync(true);
            }
        }
    }
}
