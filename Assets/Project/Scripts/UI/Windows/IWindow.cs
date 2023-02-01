namespace Project
{
    public interface IWindow
    {
        public bool IsPopup
        {
            get;
        }

        public void Prepare(UISystem uiSystem);
        public void Show();
        public void Hide();
    }
}