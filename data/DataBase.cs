using System.Collections.Generic;
using Godot;

namespace CityBuilder.Data;


public static class DataBase
{
    public static class UI
    {
        public static PackedScene ItemResource => GD.Load<PackedScene>(@"res://scenes/ui/item_resource_ui/item_resource_ui.tscn");
    }

    public static class Item
    {
        public static ItemResource Wood => GD.Load<ItemResource>(@"res://data/item_resource/wood.tres");
        public static ItemResource Stone => GD.Load<ItemResource>(@"res://data/item_resource/stone.tres");
        public static ItemResource Energy => GD.Load<ItemResource>(@"res://data/item_resource/energy.tres");
        public static readonly ItemResource[] Items = [Wood, Stone, Energy];
    }

    public class ItemID
    {
        public Dictionary<string, ItemResource> IdResource = [];
        private static ItemID _instance;

        private ItemID()
        {
            for (int i = 0; i < Item.Items.Length; i++)
            {
                var item = Item.Items[i];
                IdResource[item.Id] = item;
            }
        }

        public static ItemID GetInstance() => _instance ??= new();
    }
}
