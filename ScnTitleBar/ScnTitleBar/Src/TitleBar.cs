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

        public double HeightBar
        {
            get => HeightRequest;
            set => HeightRequest = value;
        }

        private double _paddingBar;
        protected double PaddingBar
        {
            get => _paddingBar;
            set
            {
                _paddingBar = value;
                OnPropertyChanged();

                if (IsTopiOS)
                {
                    SetLayoutFlags(BoxPadding,
                        AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.WidthProportional);
                    SetLayoutBounds(BoxPadding, new Rectangle(0f, -value, 1f, value));
                }
            }
        }

        protected readonly StackLayout AppBar;
        protected readonly ContentView ContentView;
        protected readonly Label TxtTitle;

        public BoxView BoxPadding { get; }

        public ImageButton BtnBack { get; }
        public ImageButton BtnRight { get; }
        public ImageButton BtnRightRight { get; }
        public ImageButton BtnLeft { get; }
        public ImageButton BtnLeftLeft { get; }

        public BarBtnEnum BarBtn { get; protected set; }
        public BarAlignEnum BarAlign { get; protected set; }

        public bool IsTopiOS => Device.RuntimePlatform == Device.iOS && BarAlign == BarAlignEnum.baTop;

        public TitleBar(Page page, BarBtnEnum barBtn = BarBtnEnum.bbNone, BarAlignEnum barAlign = BarAlignEnum.baTop)
        {
            BarBtn = barBtn;
            BarAlign = barAlign;

            NavigationPage.SetHasNavigationBar(page, false);
            NavigationPage.SetHasBackButton(page, false);

            page.Title = string.Empty;

            HeightBar = 48;
            BackgroundColor = _barColor;

            BoxPadding = new BoxView
            {
                BackgroundColor = _barColor
            };

            #region Panel for left buttons

            var stackLeftBtn = new StackLayout
            {
                Padding = 0,
                Spacing = 0,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Start
            };

            #endregion

            #region Panel for right buttons

            var stackRightBtn = new StackLayout
            {
                Padding = 0,
                Spacing = 0,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.End
            };
            
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

            #region Title

            TxtTitle = new Label();

            ContentView = new ContentView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            #endregion

            AppBar = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = _barColor,
                Spacing = 0,
                Children =
                {
                    stackLeftBtn,
                    ContentView,
                    stackRightBtn
                }
            };

            SetLayoutFlags(AppBar, AbsoluteLayoutFlags.All);
            SetLayoutBounds(AppBar, new Rectangle(0f, 0f, 1f, 1f));
            Children.Add(AppBar);

            SetLayoutFlags(TxtTitle, AbsoluteLayoutFlags.PositionProportional);
            SetLayoutBounds(TxtTitle, new Rectangle(0.5, 0.5, AutoSize, AutoSize));
            Children.Add(TxtTitle);

            if (IsTopiOS)
            {
                page.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == Page.PaddingProperty.PropertyName)
                        PaddingBar = page.Padding.Top;
                };

                _paddingBar = page.Padding.Top;

                SetLayoutFlags(BoxPadding, AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.WidthProportional);
                SetLayoutBounds(BoxPadding, new Rectangle(0f, -_paddingBar, 1f, _paddingBar));
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

        #region Content

        public static readonly BindableProperty ContentProperty =
            BindableProperty.Create(nameof(Content), typeof(View), typeof(TitleBar));

        public View Content
        {
            get => (View) GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        #endregion

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(Title):
                    TxtTitle.Text = Title;
                    break;
                case nameof(Content):
                    ContentView.Content = Content;
                    break;
            }
        }
    }
}
