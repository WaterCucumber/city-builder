using System;
using Godot;
using Godot.Collections;

namespace CityBuilder.Data;


public class Inventory(Func<string, int> getLimitFunc)
{
    public event Action<Dictionary<string, int>> InventoryChanged;
    private Dictionary<string, int> _items = [];
    private readonly Func<string, int> _getLimitFunc = getLimitFunc;


    public Dictionary<string, int> GetItems() => _items;

    public bool HasEnough(Dictionary<ItemResource, int> items)
    {
        foreach (var keyValuePair in items)
        {
            if(!_items.TryGetValue(keyValuePair.Key.Id, out var v)) v = 0;
            if(v < keyValuePair.Value) return false;
        }
        return true;
    }
    public bool CanAdd(Dictionary<ItemResource, int> items)
    {
        foreach (var keyValuePair in items)
        {
            if(!_items.TryGetValue(keyValuePair.Key.Id, out var v)) v = 0;
            if(v + keyValuePair.Value > _getLimitFunc?.Invoke(keyValuePair.Key.Id)) return false;
        }
        return true;
    }

    public void AddItems(Dictionary<ItemResource, int> items)
    {
        foreach (var keyValuePair in items)
        {
            if(!_items.TryGetValue(keyValuePair.Key.Id, out var v)) v = 0;
            _items[keyValuePair.Key.Id] = Mathf.Min(_getLimitFunc(keyValuePair.Key.Id), v + keyValuePair.Value);
        }
        InventoryChanged?.Invoke(_items);
    }
    public void RemoveItems(Dictionary<ItemResource, int> items)
    {
        foreach (var keyValuePair in items)
        {
            if(!_items.TryGetValue(keyValuePair.Key.Id, out var v)) v = 0;
            _items[keyValuePair.Key.Id] = Mathf.Max(0, v - keyValuePair.Value);
        }
        InventoryChanged?.Invoke(_items);
    }
}
