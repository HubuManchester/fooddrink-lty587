using System;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Newtonsoft.Json;

namespace FoodDrinkApp.Views
{
    public partial class FoodDiaryPage : ContentPage
    {
        private ObservableCollection<FoodRecord> foodRecords;

        public FoodDiaryPage()
        {
            InitializeComponent();
            LoadRecords();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadRecords();
        }

        private void LoadRecords()
        {
            string savedData = Preferences.Get("FoodRecords", "");
            if (!string.IsNullOrEmpty(savedData))
            {
                try
                {
                    foodRecords = JsonConvert.DeserializeObject<ObservableCollection<FoodRecord>>(savedData);
                    if (foodRecords == null) foodRecords = new ObservableCollection<FoodRecord>();
                    RecordsListView.ItemsSource = foodRecords;
                }
                catch
                {
                    foodRecords = new ObservableCollection<FoodRecord>();
                    RecordsListView.ItemsSource = foodRecords;
                }
            }
            else
            {
                foodRecords = new ObservableCollection<FoodRecord>();
                RecordsListView.ItemsSource = foodRecords;
            }
        }

        private async void OnDeleteRecordClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var record = button?.BindingContext as FoodRecord;

            if (record != null)
            {
                bool confirm = await DisplayAlert("删除记录", $"确定要删除「{record.FoodName}」吗？", "确定", "取消");
                if (confirm)
                {
                    if (File.Exists(record.ImagePath))
                    {
                        try { File.Delete(record.ImagePath); } catch { }
                    }

                    foodRecords.Remove(record);

                    string json = JsonConvert.SerializeObject(foodRecords);
                    Preferences.Set("FoodRecords", json);

                    await DisplayAlert("成功", "记录已删除", "好的");
                }
            }
        }
    }
}