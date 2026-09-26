using System.Collections.Generic;
using Godot;

namespace CityBuilder.Data;


public class DataBase
{
    public static ItemResource Wood => GD.Load<ItemResource>(@"res://data/item_resource/wood.tres");
    public static ItemResource Stone => GD.Load<ItemResource>(@"res://data/item_resource/stone.tres");
    public static ItemResource Energy => GD.Load<ItemResource>(@"res://data/item_resource/energy.tres");
    public static readonly ItemResource[] Items = [Wood, Stone, Energy];

    public Dictionary<string, ItemResource> IdResource = [];
    private static DataBase _instance;

    private DataBase()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            var item = Items[i];
            IdResource[item.Id] = item;
        }
    }

    public static DataBase GetInstance() => _instance ??= new();
}
