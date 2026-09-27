using Godot;

namespace CityBuilder.Scenes.UI.ComponentUI;


public partial class GlobalStorageUI : PanelContainer
{
    [Export] public Label Title { get; set; }
    [Export] public Control ItemsContainer { get; set; }

    public override void _Ready()
    {
        ClearItems();
    }

    public void ClearItems()
    {
        for (int i = 0; i < ItemsContainer.GetChildCount(); i++)
        {
            ItemsContainer.GetChild(i).QueueFree();
        }
    }
}
