using System;
using UnityEngine;

namespace Features.Rewards.Currency
{
    internal class CurrencyModel
    {
        public event Action WoodChanged;
        public event Action DiamondChanged;

        private const string WoodKey = nameof(WoodKey);
        private const string DiamondKey = nameof(DiamondKey);

        public int Wood
        {
            get => PlayerPrefs.GetInt(WoodKey, 0);
            set => SetValue(WoodKey, Wood, value, WoodChanged);
        }

        public int Diamond
        {
            get => PlayerPrefs.GetInt(DiamondKey, 0);
            set => SetValue(DiamondKey, Diamond, value, DiamondChanged);
        }


        private void SetValue(string valueKey, int oldValue, int newValue, Action changedAction)
        {
            if (oldValue == newValue)
                return;

            PlayerPrefs.SetInt(valueKey, newValue);
            changedAction?.Invoke();
        }
    }
}
