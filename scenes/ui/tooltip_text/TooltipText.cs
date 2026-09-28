using Godot;

namespace CityBuilder.Scenes.UI;


public partial class TooltipText : Control
{
    [Export] public RichTextLabel Label { get; set; }
}
