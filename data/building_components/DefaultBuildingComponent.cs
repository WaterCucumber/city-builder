using CityBuilder.CustomNodes;
using CityBuilder.Scenes.UI.ComponentUI;
using Godot;

namespace CityBuilder.Data.BuildingComponents;


[GlobalClass]
public partial class DefaultBuildingComponent : BuildingComponent
{
    public static PackedScene DefaultComponentScene => GD.Load<PackedScene>(@"res://scenes/ui/component_ui/default_component_ui/default_component_ui.tscn");
    private BuildingData _data;


    public override void AddedToBuilding(BuildingInstance building)
    {
        _data = building.Data;
    }

    protected override void ApplyUI(Control parent)
    {
        DefaultComponentUI ui = DefaultComponentScene.Instantiate<DefaultComponentUI>();
        ui.Title.Text = _data.DisplayName;

        parent.AddChild(ui);
    }
}
