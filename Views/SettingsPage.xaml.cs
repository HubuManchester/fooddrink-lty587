using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Devices;

namespace FoodDrinkApp.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            bool isDark = Preferences.Get("DarkMode", false);
            DarkModeSwitch.IsToggled = isDark;
            Application.Current.UserAppTheme = isDark ? AppTheme.Dark : AppTheme.Light;

            float fontSize = Preferences.Get("FontSize", 1.0f);
            FontSizeSlider.Value = fontSize;
            ApplyFontSize(fontSize);

            // 同步全局字体
            FontService.CurrentFontSize = fontSize;
        }

        private void OnDarkModeToggled(object sender, ToggledEventArgs e)
        {
            bool isDark = e.Value;
            Preferences.Set("DarkMode", isDark);
            Application.Current.UserAppTheme = isDark ? AppTheme.Dark : AppTheme.Light;
            DisplayAlert("Settings", isDark ? "Dark mode enabled" : "Light mode enabled", "OK");
        }

        private void OnFontSizeChanged(object sender, ValueChangedEventArgs e)
        {
            float size = (float)e.NewValue;
            Preferences.Set("FontSize", size);
            ApplyFontSize(size);

            // 触发全局字体变化事件
            FontService.CurrentFontSize = size;

            string sizeText = size < 0.9 ? "Small" : (size > 1.2 ? "Large" : "Standard");
            DisplayAlert("Font Size", $"Font size changed to {sizeText}", "OK");
        }

        private void ApplyFontSize(float size)
        {
            PreviewLabel.FontSize = 16 * size;

            if (size < 0.9)
                FontSizeLabel.Text = "Current: Small";
            else if (size > 1.2)
                FontSizeLabel.Text = "Current: Large";
            else
                FontSizeLabel.Text = "Current: Standard";
        }

        private async void OnDeviceInfoClicked(object sender, EventArgs e)
        {
            string deviceModel = DeviceInfo.Current.Model;
            string deviceManufacturer = DeviceInfo.Current.Manufacturer;
            string deviceName = DeviceInfo.Current.Name;
            string platform = DeviceInfo.Current.Platform.ToString();
            string version = DeviceInfo.Current.VersionString;
            string deviceType = DeviceInfo.Current.DeviceType == DeviceType.Virtual ? "Simulator" : "Real Device";

            double batteryLevel = Battery.Default.ChargeLevel;
            BatteryState batteryState = Battery.Default.State;

            await DisplayAlert("Device Information",
                $"━━━━━━━━━━━━━━━━━━━━\n" +
                $"📱 Model: {deviceModel}\n" +
                $"🏭 Manufacturer: {deviceManufacturer}\n" +
                $"📛 Device Name: {deviceName}\n" +
                $"💻 Platform: {platform}\n" +
                $"📀 OS Version: {version}\n" +
                $"🔧 Device Type: {deviceType}\n" +
                $"━━━━━━━━━━━━━━━━━━━━\n" +
                $"🔋 Battery Level: {batteryLevel:P0}\n" +
                $"⚡ Battery State: {(batteryState == BatteryState.Charging ? "Charging" : "Discharging")}\n" +
                $"━━━━━━━━━━━━━━━━━━━━",
                "OK");
        }
    }
}