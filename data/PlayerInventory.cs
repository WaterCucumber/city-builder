using CityBuilder.Data;
using CityBuilder.Data.BuildingComponents;
using Godot;
using Godot.Collections;

namespace CityBuilder.CustomNodes;


public class PlayerInventory
{
    private const int DefaultStorage = 30;
    public Inventory Inventory { get; private set; }
    private readonly Dictionary<string, Array<GlobalStorage>> _storages = [];


    private static PlayerInventory _instance;
    public static PlayerInventory GetInstance() => _instance ??= new();

    private PlayerInventory() 
    { 
        Inventory = new(CalculateLimit); 
        Inventory.AddItems(new Dictionary<ItemResource, int>
        {
            {DataBase.Item.Wood, 10}, 
            {DataBase.Item.Stone, 30},
        });
        
        
        Inventory.InventoryChanged += (i) => GD.Print($"{DataBase.Item.Wood.Id}: {i[DataBase.Item.Wood.Id]}");
        BuildManager.GetInstance().GridData.GridPlaceAdded += OnPlaceBuilding;
    }


    private int CalculateLimit(string resourceId)
    {
        if(_storages.TryGetValue(resourceId, out var storages))
        {
            int result = 0;
            for (int i = 0; i < storages.Count; i++)
            {
                GlobalStorage storage = storages[i];

                result += storage.StorageValue;
            }
            return result;
        }
        return DefaultStorage;
    }

    private void OnPlaceBuilding(Vector2I place)
    {
        var building = BuildManager.GetInstance().GridData.GetBuildingAt(place);
        var components = building.GetComponents();
        foreach (var component in components)
        {
            if (component is GlobalStorage storage)
            {
                string key = storage.Type.Id;
                if(!_storages.TryGetValue(key, out var v)) v = _storages[key] = [];
                building.Deleting += () => _storages[key].Remove(storage);
                v.Add(storage);
            }
        }
    }
}
