using CityBuilder.Data;
using Godot;

namespace CityBuilder.CustomNodes;


public class BuildingInstance(BuildingData data, Vector2I myOrigin)
{
    public BuildingData Data { get; private set; } = data;
    /// <summary>
    /// Top-left corner position
    /// </summary>
    public Vector2I OriginPosition { get; private set; } = myOrigin;
    public Rect2I Rect => new(OriginPosition, Data.Size);

    public virtual void OnNeighborChanged(Vector2I myPartPosition, Vector2I changedGridPosition, GridData grid)
    {
        
    }
}
