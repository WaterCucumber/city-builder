using CityBuilder.CustomNodes;
using CityBuilder.Scenes.UI;
using CityBuilder.Scenes.UI.ComponentUI;
using Godot;
using Godot.Collections;

namespace CityBuilder.Data.BuildingComponents;


[GlobalClass]
public partial class InventoryComponent : BuildingComponent
{
    public static PackedScene InventoryComponentScene => GD.Load<PackedScene>(@"res://scenes/ui/component_ui/inventory_component_ui/inventory_component_ui.tscn");
    [Export] private int _limit;
    public Inventory Inventory { get; private set; }
    private InventoryComponentUI _ui;

    public override void AddedToBuilding(BuildingInstance building)
    {
        Inventory = new((r) => _limit);
        Inventory.InventoryChanged += OnInventoryChanged;
    }

    protected override void ApplyUI(Control parent)
    {
        if (!IsInstanceValid(_ui))
        {
            _ui = InventoryComponentScene.Instantiate<InventoryComponentUI>();
            parent.AddChild(_ui);
        }

        OnInventoryChanged(Inventory.GetItems());
    }

    private void OnInventoryChanged(Dictionary<string, int> inventory)
    {
        if (!IsInstanceValid(_ui)) return;

        _ui.ClearItems();
        foreach (var keyValuePair in inventory)
        {
            ItemResourceUI itemUI = DataBase.UI.ItemResource.Instantiate<ItemResourceUI>();
            var item = DataBase.ItemID.GetInstance().IdResource[keyValuePair.Key];
            itemUI.TextureRect.Texture = item.Texture;
            itemUI.Title.Text = $"{item.Name} x{keyValuePair.Value} /{_limit}";
            _ui.ItemsContainer.AddChild(itemUI);
        }
    }
}
