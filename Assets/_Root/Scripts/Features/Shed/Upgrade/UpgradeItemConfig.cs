using UnityEngine;
using Features.Inventory.Items;

namespace Features.Shed.Upgrade
{
    [CreateAssetMenu(fileName = nameof(UpgradeItemConfig), menuName = "Configs/" + nameof(UpgradeItemConfig))]
    internal class UpgradeItemConfig : ScriptableObject
    {
        [SerializeField] private ItemConfig _itemConfig;
        [field: SerializeField] public UpgradeType Type { get; private set; }
        [field: SerializeField] public float Value { get; private set; }

        public string Id => _itemConfig.Id;
    }
}
