using Godot;
using Godot.Collections;

namespace CityBuilder.Scenes.UI;


public partial class Tooltip : VBoxContainer
{
    [Export] public Control AnchorObject { get; set; }
    [Export] private Vector2 _tooltipOffset = new(32, 0);
    [Export] private PackedScene _tooltipText;
    private Tween _tween;


    public override void _Ready()
    {
        Hide();
        Size = Vector2.Zero;
    }

    public void ShowTooltip(Array<string> otherText)
    {
        UpdateTooltip(otherText);
        Unfade();
    }

    public void Fade(float duration = 0.2f)
    {
        Modulate = Colors.White;

        _tween?.Kill();
        _tween = CreateTween();
        _tween.TweenProperty(this, "modulate:a", 0, duration);
        _tween.TweenCallback(new(this, CanvasItem.MethodName.Hide));
    }

    private void Unfade(float duration = 0.2f)
    {
        Modulate = Colors.Transparent;

        Show();
        _tween?.Kill();
        _tween = CreateTween();
        _tween.TweenProperty(this, "modulate:a", 1, duration);
    }

    public void UpdateTooltip(Array<string> otherText)
    {
        for (int i = 0; i < otherText.Count; i++)
        {
            string str = otherText[i];
            TooltipText tooltipText;
            if(GetChildCount() > i) tooltipText = GetChild<TooltipText>(i);
            else
            {
                tooltipText = _tooltipText?.Instantiate<TooltipText>();
                AddChild(tooltipText);
            }

            tooltipText.Label.Text = str;
        }
        SetupSize();
    }

    private async void SetupSize()
    {
        if(!IsInsideTree()) return;
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

        // Getting current screen size (with zoom)
        Vector2 screenSize = GetViewportRect().Size;

        // Default tooltip position: X-Right Y-Centered (+ offset)
        Vector2 globalPos = AnchorObject.GlobalPosition;
        Vector2 scaledSize = AnchorObject.Scale * AnchorObject.Size;
        float offsetRatio = Size.Y / (_tooltipOffset.X + Size.X);
        Vector2 xRightOffset = scaledSize * Vector2.Right;
        Vector2 yCentered = (scaledSize.Y - Size.Y) / 2 * Vector2.Down;
        Vector2 finalPos = globalPos + yCentered + xRightOffset + _tooltipOffset;
        
        // If right point is out of screen, then set X-Left
        if(finalPos.X + Size.X > screenSize.X)
        {
            float xOffset = Size.X + _tooltipOffset.X;
            finalPos.X = globalPos.X - xOffset;
        }

        // If right and left point are out of screen, then use Y axis instead
        if(finalPos.X < 0)
        {
            // Part 1: Center X axis
            float xOffset = (scaledSize.X - Size.X) / 2;
            float xCentered = globalPos.X + xOffset;
            finalPos.X = xCentered;
            // Part 2: Offset Y-Down
            float yOffset = scaledSize.Y + _tooltipOffset.X * offsetRatio;
            finalPos.Y = globalPos.Y + yOffset;
            // If bottom point is out of screen
            if(finalPos.Y + Size.Y > screenSize.Y)
            {
                finalPos.Y = globalPos.Y - Size.Y - _tooltipOffset.X * offsetRatio;
            }
        }

        // If bottom point is out of screen, then set it 
	    // to screen's bottom point (Y axis is inverted, 1 is down)
        if(finalPos.Y + Size.Y > screenSize.Y)
        {
            finalPos.Y = screenSize.Y - Size.Y - _tooltipOffset.Y;
        }

        GlobalPosition = finalPos;
    }
}