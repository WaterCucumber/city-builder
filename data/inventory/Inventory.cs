using Godot;
using Godot.Collections;

namespace CityBuilder.Data;


[GlobalClass]
public partial class Inventory : Resource
{
    [Export] public Dictionary<ItemResource, int> Items { get; set; }

    public bool HasEnough(Dictionary<ItemResource, int> items)
    {
        foreach (var keyValuePair in items)
        {
            if(Items.TryGetValue(keyValuePair.Key, out int v))
            {
                if(v < keyValuePair.Value) return false;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public void AddItems(Dictionary<ItemResource, int> items, bool minus = false)
    {
        foreach (var keyValuePair in items)
        {
            Items.TryGetValue(keyValuePair.Key, out int v);
            Items[keyValuePair.Key] = v + (minus ? -1 : 1) * keyValuePair.Value;
        }
    }
}
