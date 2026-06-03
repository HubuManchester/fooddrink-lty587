namespace FoodDrinkApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            const int newWidth = 1080;
            const int newHeight = 2400;

            if (window.MinimumWidth < newWidth)
                window.MinimumWidth = newWidth;
            if (window.MinimumHeight < newHeight)
                window.MinimumHeight = newHeight;

            return window;
        }
    }
}