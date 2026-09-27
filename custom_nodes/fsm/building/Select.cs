using Godot;

namespace CityBuilder.CustomNodes.FSM.Buildings;

public class Select : State
{
    private const string SelectAction = "place";
    private const string CancelAction = "cancel";
    private static BuildManager BuildManager => BuildManager.GetInstance();
    private BuildingInstance _selectedBuilding;


    public override void Exit()
    {
        _selectedBuilding?.Deselect();
    }

    public override void Input(InputEvent @event)
    {
        if (@event.IsActionPressed(CancelAction))
        {
            _selectedBuilding?.Deselect();
            _selectedBuilding = null;
        }
    }

    public override void UnhandledInput(InputEvent @event)
    {
        var building = GetBuildingUnderMouse();
        if (@event.IsActionPressed(SelectAction))
        {
            // FIXME: When we can't track that it was deselected, use this condition
            if(_selectedBuilding != null && !_selectedBuilding.IsSelected) _selectedBuilding = null;

            // If building is null: We deselect previous and set null as previous
            // If building is NOT null: We deselect previous and set current as previous, then select it
            _selectedBuilding?.Deselect();
            if(_selectedBuilding == building)
            {
                _selectedBuilding = null;
                return;
            }
            _selectedBuilding = building;
            _selectedBuilding?.Select();
        }
    }


    private static BuildingInstance GetBuildingUnderMouse()
    {
        Vector2I gridMousePosition = (Vector2I)(BuildManager.TileMapData.GetGlobalMousePosition() / GridVisualiser.GridSize);
        return BuildManager.GridData.GetBuildingOrDefault(gridMousePosition);
    }
}