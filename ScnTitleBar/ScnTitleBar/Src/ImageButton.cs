using System;
using Xamarin.Forms;
using ScnGesture.Plugin.Forms;

namespace ScnTitleBar.Forms
{
    public class ImageButton : AbsoluteLayout
    {
        public ImageButton()
        {
            BackgroundColor = Color.Transparent;
            
            image = new Image();
            SetLayoutFlags(image, AbsoluteLayoutFlags.PositionProportional);
            SetLayoutBounds(image,
                new Rectangle(0.5, 0.5, image.Width, image.Height)
            );
            Children.Add(image);

            var boxGesture = new BoxViewGesture(this);
            SetLayoutFlags(boxGesture, AbsoluteLayoutFlags.PositionProportional);
            SetLayoutBounds(boxGesture,
                new Rectangle(0.5, 0.5, this.Width, this.Height)
            );

            boxGesture.Tap += (s, e) => { OnClick(); };
            boxGesture.TapBegan += boxGesture_PressBegan;
            boxGesture.TapEnded += boxGesture_PressEnded;
            boxGesture.TapMoved += boxGesture_PressEnded;

            boxGesture.LongTapEnded += boxGesture_PressEnded;
            boxGesture.LongTapMoved += boxGesture_PressEnded;

            boxGesture.SwipeEnded += boxGesture_PressEnded;

            Children.Add(boxGesture);

            if (Device.OS == TargetPlatform.WinPhone)
            {
                var tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += (sender, e) =>
                {
                    OnClick();
                };
                GestureRecognizers.Add(tapGesture);
                image.GestureRecognizers.Add(tapGesture);
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
