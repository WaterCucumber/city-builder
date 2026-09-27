using System;
using CityBuilder.CustomNodes;
using CityBuilder.CustomNodes.FSM.Buildings;
using Godot;

namespace CityBuilder.Scenes.UI;


public partial class SelectModeToggleButton : CustomTextureButton
{
    private bool _isSelectionState = false;

    public override void _Ready()
    {
        Button.Pressed += OnButtonPressed;
    }

    private void OnButtonPressed()
    {
        _isSelectionState = !_isSelectionState;
        if(_isSelectionState)
        {
            BuildManager.GetInstance().FiniteStateMachine.TransitTo(new Select());
        }
        else
        {
            BuildManager.GetInstance().FiniteStateMachine.TransitTo(new Idle());
        }
    }
}
