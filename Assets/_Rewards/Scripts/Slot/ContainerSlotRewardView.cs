using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rewards.Slot
{
    internal class ContainerSlotRewardView : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Image _originalBackground;
        [SerializeField] private Image _selectBackground;
        [SerializeField] private Image _iconCurrency;
        [SerializeField] private TMP_Text _textCooldownPeriod;
        [SerializeField] private TMP_Text _countReward;

        [Header("Settings")]
        [SerializeField] private string _cooldownPeriodKey;

        public void SetData(Reward reward, int countCooldownPeriods, bool isSelected)
        {
            _iconCurrency.sprite = reward.IconCurrency;
            _textCooldownPeriod.text = $"{_cooldownPeriodKey} {countCooldownPeriods}";
            _countReward.text = reward.CountCurrency.ToString();

            UpdateBackground(isSelected);
        }


        private void UpdateBackground(bool isSelect)
        {
            _originalBackground.gameObject.SetActive(!isSelect);
            _selectBackground.gameObject.SetActive(isSelect);
        }
    }
}