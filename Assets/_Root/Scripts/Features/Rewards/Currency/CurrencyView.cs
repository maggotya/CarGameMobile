using UnityEngine;

namespace Features.Rewards.Currency
{
    internal class CurrencyView : MonoBehaviour
    {
        [SerializeField] private CurrencySlotView _currencyWood;
        [SerializeField] private CurrencySlotView _currentDiamond;


        public void Init(int woodCount, int diamondCount)
        {
            SetWood(woodCount);
            SetDiamond(diamondCount);
        }

        public void SetWood(int value) => _currencyWood.SetData(value);
        public void SetDiamond(int value) => _currentDiamond.SetData(value);
    }
}
