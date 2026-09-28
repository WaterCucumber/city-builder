using Godot;
using Godot.Collections;

namespace CityBuilder.Scenes.UI;


public partial class TooltipInteract : Control
{
    [Export] public Array<string> DisplayText
    {
        get => displayText;
        set
        {
            displayText = value;
            if(IsInstanceValid(_tooltip) && displayText != null) _tooltip.UpdateTooltip(displayText);
        }
    }
    [Export] private Tooltip _tooltip;

    private Array<string> displayText = [];
    private bool mouseHover = false;
    private bool canShow = true;


    public override void _Ready()
    {
        _tooltip.AnchorObject ??= this;
        _tooltip.Hide();
        MouseEntered += ShowTooltip;
        MouseExited += HideTooltip;
    }

    private void ShowTooltip()
    {
        mouseHover = true;
        if(canShow) _tooltip.ShowTooltip(DisplayText);
    }

    private void HideTooltip()
    {
        mouseHover = false;
        if(canShow) _tooltip.Fade();
    }
}
