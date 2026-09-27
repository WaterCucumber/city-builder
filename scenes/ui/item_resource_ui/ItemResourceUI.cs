using Godot;

namespace CityBuilder.Scenes.UI;


public partial class ItemResourceUI : PanelContainer
{
    [Export] public TextureRect TextureRect { get; set; }
    [Export] public Label Title { get; set; }
}
