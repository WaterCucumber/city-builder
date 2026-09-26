using CityBuilder.CustomNodes;
using Godot;

namespace CityBuilder.Data.BuildingComponents;


[GlobalClass]
public partial class InventoryComponent : BuildingComponent
{
    [Export] private int _limit;
    public Inventory Inventory { get; private set; }

    public override void AddedToBuilding(BuildingInstance building)
    {
        Inventory = new(CalculateLimit);
    }

    protected override void ApplyUI(Control parent)
    {
        throw new System.NotImplementedException();
    }

    private int CalculateLimit(string resourceId) => _limit;
}
