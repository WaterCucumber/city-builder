using Godot;

namespace CityBuilder.Data.BuildingComponents;


public partial class GlobalStorage : BuildingComponent
{
    [Export] public int StorageValue { get; private set; }
    [Export] public ItemResource Type { get; set; }
    
    protected override void ApplyUI(Control parent)
    {
        throw new System.NotImplementedException();
    }
}
