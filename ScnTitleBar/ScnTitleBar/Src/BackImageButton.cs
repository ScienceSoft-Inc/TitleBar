using Xamarin.Forms;

namespace ScnTitleBar.Forms
{
    public class BackImageButton : ImageButton
    {
        private readonly Page _page;

        public BackImageButton(Page page)
        {
            _page = page;
        }

        public override void OnClick()
        {
            base.OnClick();

            if (_page.Navigation.NavigationStack.Count > 0)
                _page.Navigation.PopAsync(true);
        }
    }
}
