using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace FoodDrinkApp.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();

            // 显示当前用户（模拟数据）
            UserNameLabel.Text = "美食家";
        }

        private async void OnDarkModeTapped(object sender, EventArgs e)
        {
            bool isDark = Application.Current.UserAppTheme == AppTheme.Dark;
            Application.Current.UserAppTheme = isDark ? AppTheme.Light : AppTheme.Dark;
            Preferences.Set("DarkMode", !isDark);
            await DisplayAlert("提示", $"已切换至{(isDark ? "浅色" : "深色")}模式", "好的");
        }

        private async void OnFontSizeTapped(object sender, EventArgs e)
        {
            await DisplayAlert("字体调整",
                "字体大小随系统设置自动调整\n\n请在手机【设置】→【显示】→【字体大小】中调整",
                "知道了");
        }

        private async void OnContactTapped(object sender, EventArgs e)
        {
            await DisplayAlert("联系管家",
                "专属服务热线\n400-888-8888\n\n服务时间\n11:00 - 22:00",
                "好的");
        }

        private async void OnLogoutTapped(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("退出登录", "确定要退出吗？", "确定", "取消");
            if (confirm)
            {
                // 清除登录状态
                Preferences.Set("IsLoggedIn", false);
                Preferences.Remove("CurrentUser");

                // 重新启动 App 回到首页（不需要 LoginPage）
                Application.Current.MainPage = new AppShell();
                await DisplayAlert("提示", "已退出登录", "好的");
            }
        }
    }
}