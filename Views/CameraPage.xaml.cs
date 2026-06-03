using System;
using System.IO;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;
using Newtonsoft.Json;

namespace FoodDrinkApp.Views
{
    public partial class CameraPage : ContentPage
    {
        private string currentImagePath;
        private ObservableCollection<FoodRecord> foodRecords;

        public CameraPage()
        {
            InitializeComponent();
            LoadSavedRecords();
        }

        private void LoadSavedRecords()
        {
            string savedData = Preferences.Get("FoodRecords", "");
            if (!string.IsNullOrEmpty(savedData))
            {
                try
                {
                    var records = JsonConvert.DeserializeObject<ObservableCollection<FoodRecord>>(savedData);
                    foodRecords = records ?? new ObservableCollection<FoodRecord>();
                }
                catch
                {
                    foodRecords = new ObservableCollection<FoodRecord>();
                }
            }
            else
            {
                foodRecords = new ObservableCollection<FoodRecord>();
            }
        }

        private void SaveRecords()
        {
            string json = JsonConvert.SerializeObject(foodRecords);
            Preferences.Set("FoodRecords", json);
        }

        private async void OnSelectPhotoClicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.Default.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "选择美食照片"
                });

                if (photo != null)
                {
                    currentImagePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                    using var stream = await photo.OpenReadAsync();
                    using var fileStream = File.OpenWrite(currentImagePath);
                    await stream.CopyToAsync(fileStream);

                    PreviewImage.Source = ImageSource.FromFile(currentImagePath);
                    await DisplayAlert("成功", "照片已选择！", "好的");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("提示", $"选择照片失败：{ex.Message}", "好的");
            }
        }

        private async void OnSaveRecordClicked(object sender, EventArgs e)
        {
            string foodName = FoodNameEntry.Text?.Trim();

            if (string.IsNullOrEmpty(foodName))
            {
                await DisplayAlert("提示", "请输入美食名称", "好的");
                return;
            }

            if (string.IsNullOrEmpty(currentImagePath))
            {
                await DisplayAlert("提示", "请先选择照片", "好的");
                return;
            }

            string fileName = $"{DateTime.Now:yyyyMMddHHmmss}.jpg";
            string savePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
            File.Copy(currentImagePath, savePath, true);

            var record = new FoodRecord
            {
                Id = Guid.NewGuid().ToString(),
                FoodName = foodName,
                ImagePath = savePath,
                Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                Notes = NotesEntry.Text?.Trim() ?? ""
            };

            foodRecords.Add(record);
            SaveRecords();

            await DisplayAlert("保存成功", $"已记录「{foodName}」\n时间：{record.Date}", "好的");

            FoodNameEntry.Text = "";
            NotesEntry.Text = "";
            PreviewImage.Source = null;
            currentImagePath = null;
        }
    }
}