using UnityEngine;

namespace Features.Inventory.Items
{
    internal interface IItem
    {
        string Id { get; }
        ItemInfo Info { get; }
    }

    internal class Item : IItem
    {
        public string Id { get; }
        public ItemInfo Info { get; }

        public Item(string id, ItemInfo info)
        {
            Id = id;
            Info = info;
        }
    }

    internal readonly struct ItemInfo
    {
        public string Title { get; }
        public Sprite Icon { get; }

        public ItemInfo(string title, Sprite icon)
        {
            Title = title;
            Icon = icon;
        }
    }
}
