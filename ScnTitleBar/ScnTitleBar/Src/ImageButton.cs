using System;
using Xamarin.Forms;

namespace ScnTitleBar.Forms
{
    public class ImageButton : ContentView
    {
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source),
            typeof(ImageSource), typeof(ImageButton), propertyChanged: SourcePropertyChanged);

        public static readonly BindableProperty ImageScaleProperty = BindableProperty.Create(nameof(ImageScale),
            typeof(double), typeof(ImageButton), 0.7, propertyChanged: ImageScalePropertyChanged);

        public ImageSource Source
        {
            get => (ImageSource) GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public double ImageScale
        {
            get => (double) GetValue(ImageScaleProperty);
            set => SetValue(ImageScaleProperty, value);
        }

        protected Image Image { get; set; }

        public ImageButton()
        {
            Image = new Image
            {
                Scale = ImageScale
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (sender, args) =>
            {
                await this.ScaleTo(0.9, 100, Easing.CubicOut);

                OnClick();

                await this.ScaleTo(1, 100, Easing.CubicOut);
            };

            GestureRecognizers.Add(tapGestureRecognizer);

            Content = Image;
        }

        private static void SourcePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var origin = (ImageButton) bindable;
            origin.Image.Source = (ImageSource) newvalue;
        }

        private static void ImageScalePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var origin = (ImageButton) bindable;
            origin.Image.Scale = (double) newvalue;
        }

        public event EventHandler Click;

        public virtual void OnClick()
        {
            Click?.Invoke(this, EventArgs.Empty);
        }
    }
}
