using System;
using Godot;
using Godot.Collections;

namespace CityBuilder.Data;


public class Inventory(Func<string, int> getLimitFunc)
{
    [Export] public Dictionary<string, int> Items { get; set; }
    private readonly Func<string, int> _getLimitFunc = getLimitFunc;


    public bool HasEnough(Dictionary<string, int> items)
    {
        foreach (var keyValuePair in items)
        {
            if(!Items.TryGetValue(keyValuePair.Key, out var v)) v = 0;
            if(v < keyValuePair.Value) return false;
        }
        return true;
    }

    public bool CanAdd(Dictionary<string, int> items)
    {
        foreach (var keyValuePair in items)
        {
            if(!Items.TryGetValue(keyValuePair.Key, out var v)) v = 0;
            if(v + keyValuePair.Value > _getLimitFunc?.Invoke(keyValuePair.Key)) return false;
        }
        return true;
    }

    public bool AddItems(Dictionary<string, int> items)
    {
        foreach (var keyValuePair in items)
        {
            if(!Items.TryGetValue(keyValuePair.Key, out var v)) v = 0;
            if(v + keyValuePair.Value > _getLimitFunc?.Invoke(keyValuePair.Key)) return false;
        }
        return true;
    }
}
