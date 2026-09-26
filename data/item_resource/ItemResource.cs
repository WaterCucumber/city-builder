using Godot;

namespace CityBuilder.Data;


[GlobalClass]
public partial class ItemResource : Resource
{
    [Export] public string Name { get; private set; }
    [Export] public Texture2D Texture { get; private set; }
    public string Id => ResourceName;
}
