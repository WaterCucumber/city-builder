using CityBuilder.CustomNodes;
using Godot;

namespace CityBuilder.Data.BuildingComponents;


[GlobalClass]
public partial class DefaultBuildingComponent : BuildingComponent
{
    private BuildingData _data;


    public override void AddedToBuilding(BuildingInstance building)
    {
        _data = building.Data;
    }

    protected override void ApplyUI(Control parent)
    {
        // Name
        var nameLabel = new Label
        {
            SizeFlagsHorizontal = Control.SizeFlags.ExpandFill,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Text = _data.DisplayName,
        };

        parent.AddChild(nameLabel);
    }
}
