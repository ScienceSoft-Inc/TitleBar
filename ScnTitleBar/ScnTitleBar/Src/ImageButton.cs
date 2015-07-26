using System;
using Xamarin.Forms;
using ScnViewGestures.Plugin.Forms;

namespace ScnTitleBar.Forms
{
    public class ImageButton : ViewGestures
    {
        public ImageButton()
        {
            BackgroundColor = Color.Transparent;
            
            Tap += (s, e) => { OnClick(); };
            TouchBegan += boxGesture_PressBegan;
            TouchEnded += boxGesture_PressEnded;

            image = new Image();
            var contentLayout = new AbsoluteLayout();
            AbsoluteLayout.SetLayoutFlags(image, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(image,
                new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)
            );
            contentLayout.Children.Add(image);
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

        async void boxGesture_PressBegan(object sender, EventArgs e)
        {
            await this.ScaleTo(0.9, 100, Easing.CubicOut);
        }

        async void boxGesture_PressEnded(object sender, EventArgs e)
        {
            await this.ScaleTo(1, 100, Easing.CubicOut);
        }

        private Image image;
        public ImageSource Source
        {
            get { return image.Source; }
            set { image.Source = value; }
        }

        public event EventHandler Click;
        public virtual void OnClick()
        {
            if (Click != null) Click(this, EventArgs.Empty);
        }
    }
}
