using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace FoodDrinkApp.Views
{
    public partial class RegisterPage : ContentPage
    {
        private Dictionary<string, string> _users;
        private System.Action _saveCallback;

        public RegisterPage(Dictionary<string, string> users, System.Action saveCallback)
        {
            InitializeComponent();
            _users = users;
            _saveCallback = saveCallback;
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            try
            {
                string username = UsernameEntry.Text?.Trim();
                string email = EmailEntry.Text?.Trim();
                string password = PasswordEntry.Text;
                string confirm = ConfirmPasswordEntry.Text;

                // 验证
                if (string.IsNullOrEmpty(username))
                {
                    await DisplayAlert("提示", "请输入用户名", "好的");
                    return;
                }

                if (string.IsNullOrEmpty(email))
                {
                    await DisplayAlert("提示", "请输入邮箱", "好的");
                    return;
                }

                if (!email.Contains("@"))
                {
                    await DisplayAlert("提示", "请输入有效的邮箱地址", "好的");
                    return;
                }

                if (string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("提示", "请输入密码", "好的");
                    return;
                }

                if (password.Length < 6)
                {
                    await DisplayAlert("提示", "密码长度不能少于6位", "好的");
                    return;
                }

                if (password != confirm)
                {
                    await DisplayAlert("提示", "两次输入的密码不一致", "好的");
                    return;
                }

                // 检查用户名是否已存在
                if (_users.ContainsKey(username))
                {
                    await DisplayAlert("提示", "用户名已存在，请使用其他用户名", "好的");
                    return;
                }

                // 保存新用户
                _users[username] = password;
                _saveCallback?.Invoke();

                await DisplayAlert("注册成功", "账号已创建，请登录", "好的");

                // 返回登录页
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("错误", $"注册失败: {ex.Message}", "好的");
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}