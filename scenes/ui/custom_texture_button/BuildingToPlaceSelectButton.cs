using CityBuilder.CustomNodes;
using CityBuilder.CustomNodes.FSM.Buildings;
using CityBuilder.Data;
using Godot;

namespace CityBuilder.Scenes.UI;


public partial class BuildingToPlaceSelectButton : CustomTextureButton
{
    [Export] private BuildingData _data;
    public override void _Ready()
    {
        Button.Pressed += OnButtonPressed;
    }

    private void OnButtonPressed()
    {
        BuildManager.GetInstance().FiniteStateMachine.TransitTo(new Ghost(_data));
    }
}
