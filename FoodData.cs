using System.Collections.Generic;

namespace FoodDrinkApp
{
    public class FoodItem
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Recipe { get; set; }
        public int Calories { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbs { get; set; }
        public string Vitamins { get; set; }
    }

    public static class FoodData
    {
        public static List<FoodItem> GetFoods()
        {
            var foods = new List<FoodItem>();

            var food1 = new FoodItem();
            food1.Icon = "🍝";
            food1.Name = "Truffle Pasta";
            food1.Description = "Handmade pasta with black truffle, aromatic";
            food1.Price = "¥168";
            food1.Recipe = "Truffle Pasta Recipe: 1. Cook pasta for 8 minutes; 2. Sauté garlic and truffle sauce; 3. Mix and sprinkle with cheese.";
            food1.Calories = 580;
            food1.Protein = 18.5;
            food1.Fat = 22.3;
            food1.Carbs = 68.2;
            food1.Vitamins = "Vitamin B, Vitamin E";
            foods.Add(food1);

            var food2 = new FoodItem();
            food2.Icon = "🥩";
            food2.Name = "Beef Wellington";
            food2.Description = "Tenderloin wrapped in crispy pastry";
            food2.Price = "¥298";
            food2.Recipe = "Beef Wellington Recipe: 1. Sear the steak; 2. Wrap with mushroom duxelles and pastry; 3. Bake at 200°C for 25 minutes.";
            food2.Calories = 850;
            food2.Protein = 45.2;
            food2.Fat = 52.8;
            food2.Carbs = 35.6;
            food2.Vitamins = "Vitamin B12, Iron, Zinc";
            foods.Add(food2);

            var food3 = new FoodItem();
            food3.Icon = "🍰";
            food3.Name = "Tiramisu";
            food3.Description = "Perfect fusion of mascarpone and coffee";
            food3.Price = "¥58";
            food3.Recipe = "Tiramisu Recipe: 1. Whip mascarpone; 2. Dip ladyfingers in coffee; 3. Refrigerate for 4 hours and sprinkle cocoa powder.";
            food3.Calories = 420;
            food3.Protein = 8.5;
            food3.Fat = 28.5;
            food3.Carbs = 35.2;
            food3.Vitamins = "Vitamin A, Calcium";
            foods.Add(food3);

            var food4 = new FoodItem();
            food4.Icon = "🥗";
            food4.Name = "Mediterranean Salad";
            food4.Description = "Fresh vegetables with olive oil, light and healthy";
            food4.Price = "¥78";
            food4.Recipe = "Mediterranean Salad Recipe: 1. Chop vegetables; 2. Make vinaigrette; 3. Mix and sprinkle with pine nuts.";
            food4.Calories = 280;
            food4.Protein = 6.5;
            food4.Fat = 18.5;
            food4.Carbs = 15.2;
            food4.Vitamins = "Vitamin C, Vitamin K, Folate";
            foods.Add(food4);

            var food5 = new FoodItem();
            food5.Icon = "🍣";
            food5.Name = "Salmon Sashimi";
            food5.Description = "Norwegian salmon, melts in your mouth";
            food5.Price = "¥128";
            food5.Recipe = "Salmon Sashimi Recipe: 1. Chill salmon; 2. Slice diagonally; 3. Serve with wasabi and soy sauce.";
            food5.Calories = 380;
            food5.Protein = 32.5;
            food5.Fat = 26.8;
            food5.Carbs = 2.1;
            food5.Vitamins = "Omega-3, Vitamin D";
            foods.Add(food5);

            var food6 = new FoodItem();
            food6.Icon = "🍜";
            food6.Name = "Lobster Noodle Soup";
            food6.Description = "Sweet lobster broth, handmade noodles";
            food6.Price = "¥188";
            food6.Recipe = "Lobster Noodle Soup Recipe: 1. Simmer lobster head for 2 hours; 2. Cook noodles; 3. Pour hot broth over noodles.";
            food6.Calories = 620;
            food6.Protein = 28.5;
            food6.Fat = 18.2;
            food6.Carbs = 72.5;
            food6.Vitamins = "Vitamin A, Selenium";
            foods.Add(food6);

            var food7 = new FoodItem();
            food7.Icon = "🥘";
            food7.Name = "Seafood Paella";
            food7.Description = "Saffron rice topped with fresh seafood";
            food7.Price = "¥158";
            food7.Recipe = "Seafood Paella Recipe: 1. Sauté rice with saffron; 2. Add fish broth and cook for 15 minutes; 3. Top with seafood and cook for 10 more minutes.";
            food7.Calories = 680;
            food7.Protein = 32.5;
            food7.Fat = 15.2;
            food7.Carbs = 88.5;
            food7.Vitamins = "Vitamin B, Iron";
            foods.Add(food7);

            var food8 = new FoodItem();
            food8.Icon = "🥟";
            food8.Name = "Black Truffle Dumplings";
            food8.Description = "Handmade dumplings with truffle filling";
            food8.Price = "¥98";
            food8.Recipe = "Black Truffle Dumplings Recipe: 1. Make truffle meat filling; 2. Wrap dumplings; 3. Boil for 3 minutes.";
            food8.Calories = 450;
            food8.Protein = 18.5;
            food8.Fat = 22.3;
            food8.Carbs = 42.5;
            food8.Vitamins = "Vitamin B";
            foods.Add(food8);

            var food9 = new FoodItem();
            food9.Icon = "🍖";
            food9.Name = "Roasted Suckling Pig";
            food9.Description = "Crispy skin and tender meat, Cantonese classic";
            food9.Price = "¥388";
            food9.Recipe = "Roasted Suckling Pig Recipe: 1. Marinate for 4 hours; 2. Apply glaze and air dry; 3. Roast for 1 hour.";
            food9.Calories = 920;
            food9.Protein = 52.5;
            food9.Fat = 68.5;
            food9.Carbs = 12.5;
            food9.Vitamins = "Collagen, Vitamin B";
            foods.Add(food9);

            var food10 = new FoodItem();
            food10.Icon = "🦪";
            food10.Name = "Gillardeau Oysters";
            food10.Description = "French Gillardeau, rich sea flavor";
            food10.Price = "¥68/each";
            food10.Recipe = "Oyster Serving Method: 1. Freshly shuck oysters; 2. Preserve natural sea water; 3. Serve with lemon or shallot vinegar.";
            food10.Calories = 120;
            food10.Protein = 10.5;
            food10.Fat = 3.5;
            food10.Carbs = 5.2;
            food10.Vitamins = "Zinc, Selenium, Vitamin B12";
            foods.Add(food10);

            return foods;
        }
    }
}