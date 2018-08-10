using System;
using Xamarin.Forms;

namespace ScnTitleBar.Forms
{
    public class TitleBar : AbsoluteLayout 
    {
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

        public int HeightBar = 48;

        protected readonly int PaddingBar;
        protected readonly AbsoluteLayout AppBar;
        protected readonly BoxView BoxPadding;
        protected readonly Label TxtTitle;

        public ImageButton BtnBack { get; }
        public ImageButton BtnRight { get; }
        public ImageButton BtnRightRight { get; }
        public ImageButton BtnLeft { get; }
        public ImageButton BtnLeftLeft { get; }

        public TitleBar(Page page, BarBtnEnum barBtn = BarBtnEnum.bbNone, BarAlignEnum barAlign = BarAlignEnum.baTop)
        {
            NavigationPage.SetHasNavigationBar(page, false);
            NavigationPage.SetHasBackButton(page, false);

            page.Title = string.Empty;
            page.Appearing += (sender, args) => NavigationPage.SetHasNavigationBar(page, false);
            page.Disappearing += (sender, args) => NavigationPage.SetHasNavigationBar(page, false);

            if (Device.RuntimePlatform == Device.iOS && barAlign == BarAlignEnum.baTop)
                PaddingBar = 20;

            BackgroundColor = _barColor;
            MinimumHeightRequest = HeightBar + PaddingBar;
            HeightRequest = HeightBar + PaddingBar;

            AppBar = new AbsoluteLayout
            {
                BackgroundColor = _barColor,
                Padding = new Thickness(0, PaddingBar, 0, 0),
                MinimumHeightRequest = HeightRequest,
                HeightRequest = HeightRequest
            };

            BoxPadding = new BoxView
            {
                BackgroundColor = _barColor
            };

            #region Title create
            TxtTitle = new Label 
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            SetLayoutFlags(TxtTitle, AbsoluteLayoutFlags.PositionProportional);
            SetLayoutBounds(TxtTitle, new Rectangle(0.5, 0.5, AutoSize, AutoSize));
            AppBar.Children.Add(TxtTitle);
            #endregion

            #region Panel for left buttons
            var stackLeftBtn = new StackLayout
            {
                Padding = new Thickness(0),
                Spacing = 0,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Start
            };
            SetLayoutFlags(stackLeftBtn, AbsoluteLayoutFlags.PositionProportional);
            SetLayoutBounds(stackLeftBtn, new Rectangle(0, 0.5, AutoSize, AutoSize));
            AppBar.Children.Add(stackLeftBtn);
            #endregion

            #region Panel for right buttons
            var stackRightBtn = new StackLayout
            {
                Padding = new Thickness(0),
                Spacing = 0,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.End
            };

            SetLayoutFlags(stackRightBtn, AbsoluteLayoutFlags.PositionProportional);
            SetLayoutBounds(stackRightBtn, new Rectangle(1, 0.5, AutoSize, AutoSize));
            AppBar.Children.Add(stackRightBtn);
            #endregion

            #region Back button
            BtnBack = new BackImageButton(page)
            {
                WidthRequest = HeightBar,
                HeightRequest = HeightBar
            };

            if ((barBtn & BarBtnEnum.bbBack) != 0)
                stackLeftBtn.Children.Add(BtnBack);
            #endregion

            #region LeftLeft button
            BtnLeftLeft = new ImageButton
            {
                WidthRequest = HeightBar,
                HeightRequest = HeightBar
            };

            if ((barBtn & BarBtnEnum.bbLeftLeft) != 0)
                stackLeftBtn.Children.Add(BtnLeftLeft);
            #endregion

            #region Left button
            BtnLeft = new ImageButton
            {
                WidthRequest = HeightBar,
                HeightRequest = HeightBar
            };

            if ((barBtn & BarBtnEnum.bbLeft) != 0 || (barBtn & BarBtnEnum.bbLeftLeft) != 0)
                stackLeftBtn.Children.Add(BtnLeft);
            #endregion

            #region Right button
            BtnRight = new ImageButton
            {
                WidthRequest = HeightBar,
                HeightRequest = HeightBar
            };

            if ((barBtn & BarBtnEnum.bbRight) != 0 || (barBtn & BarBtnEnum.bbRightRight) != 0)
                stackRightBtn.Children.Add(BtnRight);
            #endregion

            #region RightRight button
            BtnRightRight = new ImageButton
            {
                WidthRequest = HeightBar,
                HeightRequest = HeightBar
            };

            if ((barBtn & BarBtnEnum.bbRightRight) != 0)
                stackRightBtn.Children.Add(BtnRightRight);
            #endregion

            SetLayoutFlags(AppBar, AbsoluteLayoutFlags.All);
            SetLayoutBounds(AppBar, new Rectangle(0f, 0f, 1f, 1f));
            Children.Add(AppBar);

            if (Device.RuntimePlatform == Device.iOS && barAlign == BarAlignEnum.baTop)
            {
                SetLayoutFlags(BoxPadding,
                    AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.WidthProportional);
                SetLayoutBounds(BoxPadding, new Rectangle(0f, 0f, 1f, PaddingBar));
                Children.Add(BoxPadding);
            }
        }

        #region Background color

        private Color _barColor = Color.White;
        public Color BarColor
        {
            get => _barColor;
            set
            {
                _barColor = value;
                BackgroundColor = value;
                AppBar.BackgroundColor = value;
                BoxPadding.BackgroundColor = value;
            }
        }

        #endregion

        #region Title

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(TitleBar));

        public string Title
        {
            get => (string) GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public Style TitleStyle
        {
            get => TxtTitle.Style;
            set => TxtTitle.Style = value;
        }

        #endregion

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(Title))
                TxtTitle.Text = Title;
        }
    }
}
