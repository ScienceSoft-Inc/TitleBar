using System;
using Xamarin.Forms;

namespace ScnTitleBar.Forms
{
    public class ImageButton : Image
    {
        public ImageButton()
        {
            BackgroundColor = Color.Transparent;
            
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (sender, args) =>
            {
                await this.ScaleTo(0.9, 100, Easing.CubicOut);

                OnClick();

                await this.ScaleTo(1, 100, Easing.CubicOut);
            };

            GestureRecognizers.Add(tapGestureRecognizer);
        }

        public event EventHandler Click;

        public virtual void OnClick()
        {
            Click?.Invoke(this, EventArgs.Empty);
        }
    }
}
