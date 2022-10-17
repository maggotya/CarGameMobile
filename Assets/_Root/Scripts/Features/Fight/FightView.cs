using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Fight
{
    internal class FightView : MonoBehaviour
    {
        [field: Header("Player Stats")]
        [field: SerializeField] public TMP_Text CountMoneyText { get; private set; }
        [field: SerializeField] public TMP_Text CountHealthText { get; private set; }
        [field: SerializeField] public TMP_Text CountPowerText { get; private set; }
        [field: SerializeField] public TMP_Text CountCrimeText { get; private set; }

        [field: Header("Enemy Stats")]
        [field: SerializeField] public TMP_Text CountPowerEnemyText{ get; private set; }

        [field: Header("Money Buttons")]
        [field: SerializeField] public Button AddMoneyButton{ get; private set; }
        [field: SerializeField] public Button MinusMoneyButton{ get; private set; }

        [field: Header("Health Buttons")]
        [field: SerializeField] public Button AddHealthButton{ get; private set; }
        [field: SerializeField] public Button MinusHealthButton{ get; private set; }

        [field: Header("Power Buttons")]
        [field: SerializeField] public Button AddPowerButton{ get; private set; }
        [field: SerializeField] public Button MinusPowerButton{ get; private set; }

        [field: Header("Crime Buttons")]
        [field: SerializeField] public Button AddCrimeButton{ get; private set; }
        [field: SerializeField] public Button MinusCrimeButton{ get; private set; }

        [field: Header("Other Buttons")]
        [field: SerializeField] public Button FightButton{ get; private set; }
        [field: SerializeField] public Button EscapeButton{ get; private set; }
    }
}
