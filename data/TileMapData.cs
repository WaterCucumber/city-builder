using Godot;

namespace CityBuilder.Data;


public partial class TileMapData : TileMapLayer
{
    private static TileMapData _instance;

    public static TileMapData GetInstance() => _instance;

    public override void _Ready()
    {
        if(_instance != null) GD.PushError("Scene has more than 1 TileMapData (singletone)");
        _instance = this;
    }
}
