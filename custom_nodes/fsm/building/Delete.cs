using CityBuilder.Data;
using Godot;

namespace CityBuilder.CustomNodes.FSM.Buildings;


public class Delete : State
{
    private const string DeleteAction = "place";
    private const string DeleteContinuousAction = "place_continuous";
    private const string CancelAction = "cancel";
    private static Color DeleteColor => new(0.4f, 0.4f, 1f, 0.7f);
    private static Color DefaultColor => Colors.White;
    private static BuildManager BuildManager => BuildManager.GetInstance();
    private BuildingInstance _buildingUnderMouse;

    private bool _isContinuous;


    public override void Exit()
    {
        _buildingUnderMouse = null;
        ChangeBuildingModulate(DefaultColor);
    }

    public override void Process(double delta)
    {
        ChangeBuildingModulate(DeleteColor);
    }
    public override void UnhandledInput(InputEvent @event)
    {
        if(@event.IsActionPressed(DeleteContinuousAction)) _isContinuous = true;
        if(@event.IsActionReleased(DeleteContinuousAction)) _isContinuous = false;

        if (@event.IsActionPressed(DeleteAction))
        {
            var building = GetBuildingUnderMouse();
            if(building != null)
            {
                BuildManager.GridData.PlaceBuilding(building.Rect, null);
                _buildingUnderMouse = null;
                if(!_isContinuous)
                {
                    TransitTo(new Select());
                    return;
                }
            }
        }

        if (@event.IsActionPressed(CancelAction))
        {
            TransitTo(new Select());
            return;
        }
    }


    private void ChangeBuildingModulate(Color modulate)
    {
        var building = GetBuildingUnderMouse();
        if(building == _buildingUnderMouse) return;

        if(_buildingUnderMouse != null) 
        {
            UpdateModulate(_buildingUnderMouse, DefaultColor);
        }

        if(building == _buildingUnderMouse) return;
        _buildingUnderMouse = building;

        if(_buildingUnderMouse != null)
        {
            UpdateModulate(_buildingUnderMouse, modulate);
        }
    }

    private static void UpdateModulate(BuildingInstance building, Color modulate)
    {
        var canvasItem = BuildManager.GridVisualiser.GetCanvasItem(building.OriginPosition);
        Rect2 rect = building.Rect; 
        rect.Position *= GridVisualiser.GridSize; 
        rect.Size *= GridVisualiser.GridSize;

        RenderingServer.CanvasItemClear(canvasItem);
        RenderingServer.CanvasItemAddTextureRect(canvasItem, rect, building.Data.Texture.GetRid(), modulate: modulate);
    }

    private static BuildingInstance GetBuildingUnderMouse()
    {
        Vector2I gridMousePosition = (Vector2I)(BuildManager.TileMapData.GetGlobalMousePosition() / GridVisualiser.GridSize);
        return BuildManager.GridData.GetBuildingOrDefault(gridMousePosition);
    }
}
