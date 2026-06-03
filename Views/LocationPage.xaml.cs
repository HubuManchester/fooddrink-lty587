using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.ApplicationModel;

namespace FoodDrinkApp.Views
{
    public partial class LocationPage : ContentPage
    {
        private ObservableCollection<RestaurantItem> restaurants;

        private List<LocationOption> locations = new List<LocationOption>
        {
            new LocationOption { Name = "Hubei University Wuchang Campus", Address = "368 Youyi Avenue" },
            new LocationOption { Name = "Chu River Han Street", Address = "Zhongbei Road, Wuchang" },
            new LocationOption { Name = "Optics Valley Square", Address = "Luoyu Road, Hongshan" },
            new LocationOption { Name = "Jianghan Road Pedestrian Street", Address = "Zhongshan Avenue, Jianghan" },
            new LocationOption { Name = "Jiedaokou", Address = "Luoyu Road, Hongshan" }
        };

        private int currentLocationIndex = 0;

        public LocationPage()
        {
            InitializeComponent();
            LoadRestaurantsForCurrentLocation();
        }

        private void LoadRestaurantsForCurrentLocation()
        {
            string currentLoc = CurrentLocationLabel.Text;

            restaurants = new ObservableCollection<RestaurantItem>();

            if (currentLoc == "Hubei University Wuchang Campus")
            {
                restaurants.Add(new RestaurantItem { Icon = "🍕", Name = "Pizza Hut", Distance = "200m", Rating = "⭐⭐⭐⭐ 4.5", Address = "Opposite Hubei University Gate" });
                restaurants.Add(new RestaurantItem { Icon = "🍔", Name = "McDonald's", Distance = "350m", Rating = "⭐⭐⭐⭐ 4.3", Address = "Youyi Avenue & QinYuan Road" });
                restaurants.Add(new RestaurantItem { Icon = "🍜", Name = "Cai Lin Ji", Distance = "500m", Rating = "⭐⭐⭐⭐⭐ 4.8", Address = "18 Xudong Avenue" });
                restaurants.Add(new RestaurantItem { Icon = "🥘", Name = "University Canteen", Distance = "100m", Rating = "⭐⭐⭐⭐ 4.2", Address = "Inside Hubei University" });
                restaurants.Add(new RestaurantItem { Icon = "☕", Name = "Starbucks", Distance = "800m", Rating = "⭐⭐⭐⭐ 4.4", Address = "1st Floor, Xiao Pin Mao" });
            }
            else if (currentLoc == "Chu River Han Street")
            {
                restaurants.Add(new RestaurantItem { Icon = "🍣", Name = "Sushi Master", Distance = "150m", Rating = "⭐⭐⭐⭐⭐ 4.9", Address = "Han Street Block 1" });
                restaurants.Add(new RestaurantItem { Icon = "🍷", Name = "Le Bistrot", Distance = "300m", Rating = "⭐⭐⭐⭐ 4.7", Address = "Han Street Wanda Plaza" });
                restaurants.Add(new RestaurantItem { Icon = "🥩", Name = "Korean BBQ", Distance = "500m", Rating = "⭐⭐⭐⭐ 4.6", Address = "Han Street Block 3" });
            }
            else if (currentLoc == "Optics Valley Square")
            {
                restaurants.Add(new RestaurantItem { Icon = "🍲", Name = "Haidilao", Distance = "200m", Rating = "⭐⭐⭐⭐⭐ 4.9", Address = "Optics Valley World City" });
                restaurants.Add(new RestaurantItem { Icon = "🍗", Name = "KFC", Distance = "100m", Rating = "⭐⭐⭐⭐ 4.2", Address = "Optics Valley Pedestrian Street" });
                restaurants.Add(new RestaurantItem { Icon = "🥟", Name = "Four Seasons Soup Bun", Distance = "400m", Rating = "⭐⭐⭐⭐⭐ 4.7", Address = "Optics Valley TianDi" });
            }
            else
            {
                restaurants.Add(new RestaurantItem { Icon = "🍜", Name = "Cai Lin Ji", Distance = "300m", Rating = "⭐⭐⭐⭐⭐ 4.8", Address = "Local Food Street" });
                restaurants.Add(new RestaurantItem { Icon = "🍕", Name = "Pizza Hut", Distance = "500m", Rating = "⭐⭐⭐⭐ 4.4", Address = "Shopping Mall" });
            }

            RestaurantsList.ItemsSource = restaurants;
        }

        private async void OnChangeLocationTapped(object sender, EventArgs e)
        {
            string[] locationNames = locations.Select(l => l.Name).ToArray();

            string selected = await DisplayActionSheet("Select Location", "Cancel", null, locationNames);

            if (!string.IsNullOrEmpty(selected) && selected != "Cancel")
            {
                var loc = locations.FirstOrDefault(l => l.Name == selected);
                if (loc != null)
                {
                    CurrentLocationLabel.Text = loc.Name;
                    CurrentAddressLabel.Text = loc.Address;
                    LoadRestaurantsForCurrentLocation();
                    await DisplayAlert("Location Changed", $"Current location: {loc.Name}", "OK");
                }
            }
        }

        private async void OnRestaurantSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is RestaurantItem selected)
            {
                string phone = GetPhoneForRestaurant(selected.Name);
                await DisplayAlert(selected.Name,
                    $"📍 Address: {selected.Address}\n" +
                    $"📞 Phone: {phone}\n" +
                    $"⭐ Rating: {selected.Rating}",
                    "OK");

                ((CollectionView)sender).SelectedItem = null;
            }
        }

        private string GetPhoneForRestaurant(string name)
        {
            var phones = new Dictionary<string, string>
            {
                { "Pizza Hut", "027-8888-6666" },
                { "McDonald's", "027-8888-7777" },
                { "Cai Lin Ji", "027-8888-8888" },
                { "University Canteen", "027-8866-1111" },
                { "Starbucks", "027-8888-9999" },
                { "Haidilao", "027-6666-8888" },
                { "KFC", "027-6666-7777" },
                { "Sushi Master", "027-8888-5555" },
                { "Le Bistrot", "027-8888-4444" },
                { "Korean BBQ", "027-8888-3333" },
                { "Four Seasons Soup Bun", "027-8888-2222" }
            };
            return phones.ContainsKey(name) ? phones[name] : "027-8888-0000";
        }
    }

    public class LocationOption
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class RestaurantItem
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Distance { get; set; }
        public string Rating { get; set; }
        public string Address { get; set; }
    }
}