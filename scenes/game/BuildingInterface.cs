using System;
using CityBuilder.CustomNodes;
using Godot;

namespace CityBuilder.Scenes.Game;


public partial class BuildingInterface : PanelContainer
{
    [Export] private Control _dynamicInterface;
    private BuildingInstance _selectedBuilding;

    public override void _Ready()
    {
        BuildManager.GetInstance().GridData.GridPlaceChanged += OnGridPlaceChanged;
        Hide();
        VisibilityChanged += () => { if(!Visible) _selectedBuilding?.Deselect(); };
    }

    public override void _GuiInput(InputEvent @event)
    {
        GetViewport().SetInputAsHandled();
    }

    private void OnGridPlaceChanged(Vector2I position)
    {
        var building = BuildManager.GetInstance().GridData.GetBuildingAt(position);
        if(building != null) 
        {
            building.Selected += () => OnBuildingSelected(building);
            building.Deselected += () => OnBuildingDeselected(building);
        }
    }

    private void OnBuildingDeselected(BuildingInstance building)
    {
        _selectedBuilding = null;
        building.FreeUI();
        Hide();
    }

    private void OnBuildingSelected(BuildingInstance building)
    {
        _selectedBuilding = building;
        building.AddUI(_dynamicInterface);
        Show();
    }
}
