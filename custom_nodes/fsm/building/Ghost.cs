using CityBuilder.Data;
using Godot;
using LowLevelGeometryLab.Static;

namespace CityBuilder.CustomNodes.FSM.Buildings;


public class Ghost(BuildingData building) : State
{
    private const string PlaceAction = "place";
    private const string CancelAction = "cancel";
    private const string ContinuousPlaceAction = "place_continuous";
    private const string PlaceLevelTagName = "place_level";

    private static readonly Color InvalidColor = new(1, 0.4f, 0.4f, 0.7f);
    private static readonly Color ValidColor = new(0.4f,1,0.4f,0.7f);
    private static readonly Rid _canvasItem = CanvasManager.GetInstance().CreateCanvasItem(1);

    private readonly TileMapData _tilemap = TileMapData.GetInstance();
    private Vector2 _previousPos;
    private bool _isContinuous;


    public override void PhysicsProcess(double delta)
    {
        if(Input.IsActionJustPressed(ContinuousPlaceAction)) _isContinuous = true;
        if(Input.IsActionJustReleased(ContinuousPlaceAction)) _isContinuous = false;

        if (Input.IsActionJustPressed(PlaceAction))
        {
            if(CanBuild()) 
            {
                PlaceBuilding();
                if(!_isContinuous)
                {
                    TransitTo(new Idle());
                    return;
                }
            }
            // TODO:
            //else InvalidPlacementPositionNotification();
        }

        if (Input.IsActionJustPressed(CancelAction))
        {
            TransitTo(new Idle());
            return;
        }


        if(building != null) DrawBuilding();
        else if(_previousPos != Vector2.Inf)
        {
            _previousPos = Vector2.Inf;
            RenderingServer.CanvasItemClear(_canvasItem);
        }
    }

    public override void Exit() => RenderingServer.CanvasItemClear(_canvasItem);

    private void DrawBuilding()
    {
        Vector2 gridMousePosition = _tilemap.GetGlobalMousePosition() / GridVisualiser.GridSize;
        Vector2 snappedPos = (gridMousePosition - (Vector2)building.Size * 0.5f).Floor() * GridVisualiser.GridSize;

        if(_previousPos == snappedPos) return;
        _previousPos = snappedPos;

        Rect2 rect = new(snappedPos, building.Size * (int)GridVisualiser.GridSize);
        Color modulate = GetBuildingModulate();
        
        RenderingServer.CanvasItemClear(_canvasItem);
        RenderingServer.CanvasItemAddTextureRect(_canvasItem, rect, building.Texture.GetRid(), modulate: modulate);
    }

    private Color GetBuildingModulate() => CanBuild() ? ValidColor : InvalidColor;

    private bool CanBuild()
    {
        Vector2 gridMousePosition = _tilemap.GetGlobalMousePosition() / GridVisualiser.GridSize;
        Vector2 snappedPosition = (gridMousePosition - (Vector2)building.Size * 0.5f).Floor() * GridVisualiser.GridSize;
        for (int x = 0; x < building.Size.X; x++)
        {
            for (int y = 0; y < building.Size.Y; y++)
            {
                if(!IsPlaceValid(snappedPosition + new Vector2(x, y) * GridVisualiser.GridSize))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool IsPlaceValid(Vector2 position)
    {
        var gridPosition = _tilemap.LocalToMap(position);

        if(BuildManager.GetInstance().GridData.IsInsideGrid(gridPosition) == false) return false;
        if(BuildManager.GetInstance().GridData.GetBuildingAt(gridPosition) != null) return false;

        var data = _tilemap.GetCellTileData(gridPosition);
        if(data == null) return false;

        int level = (int)data.GetCustomData(PlaceLevelTagName);
        return building.PlaceLevels.Contains(level);
    }

    private void PlaceBuilding()
    {
        Vector2 gridMousePosition = _tilemap.GetGlobalMousePosition() / GridVisualiser.GridSize;
        Vector2I snappedPos = (Vector2I)(gridMousePosition - (Vector2)building.Size * 0.5f);
        BuildingInstance instance = new(building, snappedPos);
        BuildManager.GetInstance().GridData.PlaceBuilding(instance.Rect, instance);
    }
}
