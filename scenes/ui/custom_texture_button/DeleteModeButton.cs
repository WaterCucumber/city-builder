using CityBuilder.CustomNodes;
using CityBuilder.CustomNodes.FSM.Buildings;
using Godot;

namespace CityBuilder.Scenes.UI;


public partial class DeleteModeButton : CustomTextureButton
{
    public override void _Ready()
    {
        Button.Pressed += OnButtonPressed;
    }

    private void OnButtonPressed()
    {
        BuildManager.GetInstance().FiniteStateMachine.TransitTo(new Delete());
    }
}
