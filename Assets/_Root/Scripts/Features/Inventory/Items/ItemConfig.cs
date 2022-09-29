using UnityEngine;

namespace Features.Inventory.Items
{
    [CreateAssetMenu(fileName = nameof(ItemConfig), menuName = "Configs/" + nameof(ItemConfig))]
    internal class ItemConfig : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}
