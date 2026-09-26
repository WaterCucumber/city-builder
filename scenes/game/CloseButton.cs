using Godot;

namespace CityBuilder.Scenes.Game;


public partial class CloseButton : Button
{
    [Export] private Control[] _itemsToClose;

    public override void _Pressed()
    {
        foreach (var item in _itemsToClose)
        {
            item.Visible = false;
        }
    }
}
