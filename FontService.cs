using System;

namespace FoodDrinkApp
{
    public static class FontService
    {
        public static event Action<float> FontSizeChanged;
        private static float _currentFontSize = 1.0f;

        public static float CurrentFontSize
        {
            get => _currentFontSize;
            set
            {
                if (_currentFontSize != value)
                {
                    _currentFontSize = value;
                    FontSizeChanged?.Invoke(_currentFontSize);
                }
            }
        }
    }
}