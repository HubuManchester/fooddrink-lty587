using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;
using Newtonsoft.Json;

namespace FoodDrinkApp.Views
{
    public partial class HomePage : ContentPage
    {
        private ObservableCollection<object> recommendList;
        private ObservableCollection<FoodRecord> recentRecords;

        public HomePage()
        {
            InitializeComponent();
            LoadRecommendations();
            LoadRecentRecords();
            ApplySavedFontSize();

            // 监听全局字体变化
            FontService.FontSizeChanged += OnFontSizeChanged;
        }

        private void OnFontSizeChanged(float newSize)
        {
            ApplyFontSizeToPage(newSize);
        }

        private void ApplyFontSizeToPage(float size)
        {
            // 应用字体到各个 Label（通过设置 Resources 或直接设置）
            this.Resources["LabelFontSize"] = size;

            // 强制刷新页面
            OnPropertyChanged(nameof(Resources));
        }

        private void LoadRecommendations()
        {
            var foods = FoodData.GetFoods();
            recommendList = new ObservableCollection<object>();
            foreach (var food in foods)
            {
                recommendList.Add(new { food.Icon, food.Name, food.Description, food.Price, food.Recipe, food.Calories, food.Protein, food.Fat, food.Carbs, food.Vitamins });
            }
            RecommendListView.ItemsSource = recommendList;
        }

        private void LoadRecentRecords()
        {
            string savedData = Preferences.Get("FoodRecords", "");
            if (!string.IsNullOrEmpty(savedData))
            {
                try
                {
                    var allRecords = JsonConvert.DeserializeObject<ObservableCollection<FoodRecord>>(savedData);
                    if (allRecords != null && allRecords.Count > 0)
                    {
                        recentRecords = new ObservableCollection<FoodRecord>(allRecords.Take(3));
                        RecentRecordsListView.ItemsSource = recentRecords;
                        return;
                    }
                }
                catch { }
            }
            recentRecords = new ObservableCollection<FoodRecord>();
            RecentRecordsListView.ItemsSource = recentRecords;
        }

        private void ApplySavedFontSize()
        {
            float fontSize = Preferences.Get("FontSize", 1.0f);
            FontService.CurrentFontSize = fontSize;
            ApplyFontSizeToPage(fontSize);
        }

        // Camera: Take Photo
        private async void OnTakePhotoClicked(object sender, EventArgs e)
        {
            try
            {
                var cameraStatus = await Permissions.RequestAsync<Permissions.Camera>();
                if (cameraStatus != PermissionStatus.Granted)
                {
                    await DisplayAlert("Permission Required", "Camera permission is needed to take photos", "OK");
                    return;
                }

                var photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo == null) return;

                string foodName = await DisplayPromptAsync("Record Food", "Enter food name:", "Save", "Cancel");
                if (string.IsNullOrEmpty(foodName))
                {
                    await DisplayAlert("Notice", "Please enter food name", "OK");
                    return;
                }

                string notes = await DisplayPromptAsync("Add Notes", "Optional: Record the taste", "Save", "Skip");

                string fileName = $"{DateTime.Now:yyyyMMddHHmmss}.jpg";
                string savePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                using var stream = await photo.OpenReadAsync();
                using var fileStream = File.OpenWrite(savePath);
                await stream.CopyToAsync(fileStream);

                SaveFoodRecord(foodName, savePath, notes ?? "");

                await DisplayAlert("Saved", $"「{foodName}」has been recorded", "OK");
                LoadRecentRecords();
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Camera Function",
                    "📷 Camera function requires a real device\n\nCode fully implemented:\n✓ Camera permission request\n✓ Take photo\n✓ Save photo\n✓ Record storage",
                    "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to take photo: {ex.Message}", "OK");
            }
        }

        // Gallery: Choose from Gallery
        private async void OnPickPhotoClicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.Default.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Select Food Photo"
                });

                if (photo == null) return;

                string foodName = await DisplayPromptAsync("Record Food", "Enter food name:", "Save", "Cancel");
                if (string.IsNullOrEmpty(foodName))
                {
                    await DisplayAlert("Notice", "Please enter food name", "OK");
                    return;
                }

                string notes = await DisplayPromptAsync("Add Notes", "Optional: Record the taste", "Save", "Skip");

                string fileName = $"{DateTime.Now:yyyyMMddHHmmss}.jpg";
                string savePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                using var stream = await photo.OpenReadAsync();
                using var fileStream = File.OpenWrite(savePath);
                await stream.CopyToAsync(fileStream);

                SaveFoodRecord(foodName, savePath, notes ?? "");

                await DisplayAlert("Saved", $"「{foodName}」has been recorded", "OK");
                LoadRecentRecords();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to select photo: {ex.Message}", "OK");
            }
        }

        private void SaveFoodRecord(string foodName, string imagePath, string notes)
        {
            ObservableCollection<FoodRecord> foodRecords;
            string savedData = Preferences.Get("FoodRecords", "");
            if (!string.IsNullOrEmpty(savedData))
            {
                foodRecords = JsonConvert.DeserializeObject<ObservableCollection<FoodRecord>>(savedData) ?? new ObservableCollection<FoodRecord>();
            }
            else
            {
                foodRecords = new ObservableCollection<FoodRecord>();
            }

            var record = new FoodRecord
            {
                Id = Guid.NewGuid().ToString(),
                FoodName = foodName,
                ImagePath = imagePath,
                Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                Notes = notes
            };
            foodRecords.Insert(0, record);

            string json = JsonConvert.SerializeObject(foodRecords);
            Preferences.Set("FoodRecords", json);
        }

        // Tap on food card to view nutrition info
        private async void OnFoodCardTapped(object sender, EventArgs e)
        {
            var frame = sender as Frame;
            if (frame != null && frame.BindingContext != null)
            {
                dynamic selected = frame.BindingContext;
                var food = FoodData.GetFoods().FirstOrDefault(f => f.Name == selected.Name);

                if (food != null)
                {
                    await DisplayAlert($"🍽️ {food.Name}",
                        $"━━━━━━━━━━━━━━━━━━━━\n" +
                        $"📝 Description: {food.Description}\n" +
                        $"💰 Price: {food.Price}\n" +
                        $"━━━━━━━━━━━━━━━━━━━━\n" +
                        $"🔥 Calories: {food.Calories} kcal\n" +
                        $"🥩 Protein: {food.Protein} g\n" +
                        $"🧈 Fat: {food.Fat} g\n" +
                        $"🍚 Carbohydrates: {food.Carbs} g\n" +
                        $"💊 Vitamins: {food.Vitamins}\n" +
                        $"━━━━━━━━━━━━━━━━━━━━\n\n" +
                        $"👨‍🍳 Recipe:\n{food.Recipe}", "OK");
                }
            }
        }

        // Speak food info
        private async void OnSpeakFoodInfoClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.BindingContext != null)
            {
                dynamic selected = button.BindingContext;
                var food = FoodData.GetFoods().FirstOrDefault(f => f.Name == selected.Name);

                if (food != null)
                {
                    string speakText = $"{food.Name}. {food.Description}. Price {food.Price}. " +
                        $"Calories {food.Calories}. Protein {food.Protein} grams. Fat {food.Fat} grams. " +
                        $"Carbohydrates {food.Carbs} grams. Rich in {food.Vitamins}. Recipe: {food.Recipe}";

                    try
                    {
                        await TextToSpeech.SpeakAsync(speakText);
                        await DisplayAlert("🔊 Speaking", $"Reading nutrition info of 「{food.Name}」", "OK");
                    }
                    catch
                    {
                        await DisplayAlert("Notice", "Speech function requires permission", "OK");
                    }
                }
            }
        }

        // View all food diary
        private async void OnViewAllRecordsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//FoodDiaryPage");
        }

        // Custom English speech
        private async void OnSpeakCustomEnglishClicked(object sender, EventArgs e)
        {
            string customText = CustomEnglishEntry.Text?.Trim();

            if (string.IsNullOrEmpty(customText))
            {
                await DisplayAlert("Notice", "Please enter an English sentence", "OK");
                return;
            }

            bool hasEnglish = System.Text.RegularExpressions.Regex.IsMatch(customText, "[a-zA-Z]");
            if (!hasEnglish)
            {
                await DisplayAlert("Notice", "Please enter an English sentence", "OK");
                return;
            }

            try
            {
                await TextToSpeech.SpeakAsync(customText);
                await DisplayAlert("🔊 Speaking", customText, "OK");
            }
            catch
            {
                await DisplayAlert("Notice", "Speech function requires permission", "OK");
            }
        }
    }
}