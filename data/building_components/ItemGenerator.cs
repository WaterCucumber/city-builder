using CityBuilder.CustomNodes;
using Godot;
using Godot.Collections;

namespace CityBuilder.Data.BuildingComponents;


[GlobalClass]
public partial class ItemGenerator : BuildingComponent
{
    [Export] private Dictionary<ItemResource, int> _itemsToGenerate = [];
    [Export] private float _waitTime = 1;
    private float _timer;
    private int _connections;
    private InventoryComponent _component;

    public override void AddedToBuilding(BuildingInstance building)
    {
        foreach (var component in building.GetComponents())
        {
            if(component is InventoryComponent c)
            {
                _component = c;
                break;
            }
        }

        if(_component == null)
        {
            throw new System.Exception($"Building {building.Data.DisplayName} has no component {nameof(InventoryComponent)}");
        }

        ((SceneTree)Engine.GetMainLoop()).PhysicsFrame += PhysicsFrame;
    }
    public override void RemovedFromBuilding(BuildingInstance building)
    {
        ((SceneTree)Engine.GetMainLoop()).PhysicsFrame -= PhysicsFrame;
    }

    protected override void ApplyUI(Control parent)
    {
        GD.PrintErr($"No UI for {nameof(ItemGenerator)}!");
    }

    private void PhysicsFrame()
    {
        if(_timer > _waitTime)
        {
            if (_component.Inventory.CanAdd(_itemsToGenerate))
            {
                _timer = 0;
                _component.Inventory.AddItems(_itemsToGenerate);
            }
        }
        else
        {
            float delta = 1.0f / Engine.PhysicsTicksPerSecond;
            _timer += delta;
        }
    }
}
