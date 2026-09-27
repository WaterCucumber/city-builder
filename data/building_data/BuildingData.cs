using CityBuilder.Data.BuildingComponents;
using Godot;
using Godot.Collections;

namespace CityBuilder.Data;


[GlobalClass]
public partial class BuildingData : Resource
{
    [Export] public string DisplayName { get; private set; }
    [Export] public Texture2D Texture { get; private set; }
    [Export] public Vector2I Size { get; private set; }
    [Export] public Array<int> PlaceLevels { get; private set; } = [];
    [Export] public Dictionary<ItemResource, int> Cost { get; private set; } = [];
    [Export] public BuildingComponent[] BaseComponents { get; set; } = [new DefaultBuildingComponent()];
}
