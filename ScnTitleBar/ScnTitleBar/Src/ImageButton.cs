using System;
using Xamarin.Forms;
using ScnViewGestures.Plugin.Forms;

namespace ScnTitleBar.Forms
{
    public class ImageButton : ViewGestures
    {
        private readonly Image _image;

        public ImageButton()
        {
            BackgroundColor = Color.Transparent;
            
            Tap += (s, e) => OnClick();
            TouchBegan += BoxGesture_PressBegan;
            TouchEnded += BoxGesture_PressEnded;

            _image = new Image();
            AbsoluteLayout.SetLayoutFlags(_image, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(_image,
                new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            var contentLayout = new AbsoluteLayout();
            contentLayout.Children.Add(_image);

            Content = contentLayout;
        }

        public new Color BackgroundColor
        {
            get 
            {
                return base.BackgroundColor;
            }
            set 
            {
                base.BackgroundColor = value;

                if (Device.OS != TargetPlatform.Android)
                {
                    if (Content != null)
                        Content.BackgroundColor = value;
                }
            }
        }

        public new double Opacity
        {
            get
            {
                return base.Opacity;
            }
            set
            {
                if (Device.OS != TargetPlatform.Android)
                {
                    if (Content != null)
                        Content.Opacity = value;
                }
                else
                    base.Opacity = value;
            }
        }

        private async void BoxGesture_PressBegan(object sender, EventArgs e)
        {
            await this.ScaleTo(0.9, 100, Easing.CubicOut);
        }

        private async void BoxGesture_PressEnded(object sender, EventArgs e)
        {
            await this.ScaleTo(1, 100, Easing.CubicOut);
        }

        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create<ImageButton, ImageSource>(i => i.Source, null,
                propertyChanged: SourcePropertyChanged);

        public ImageSource Source
        {
            get { return (ImageSource) GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        private static void SourcePropertyChanged(BindableObject bindable, ImageSource oldvalue, ImageSource newvalue)
        {
            var origin = (ImageButton) bindable;
            origin._image.Source = newvalue;
        }

        public event EventHandler Click;

        public virtual void OnClick()
        {
            if (Click != null)
                Click(this, EventArgs.Empty);
        }
    }
}
