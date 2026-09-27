using CityBuilder.Scenes.UI;
using CityBuilder.Scenes.UI.ComponentUI;
using Godot;

namespace CityBuilder.Data.BuildingComponents;


[GlobalClass]
public partial class GlobalStorage : BuildingComponent
{
    public static PackedScene GlobalStorageScene => GD.Load<PackedScene>(@"res://scenes/ui/component_ui/global_storage_ui/global_storage_ui.tscn");
    [Export] public int StorageValue
    {
        get => _storageValue;
        set
        {
            _storageValue = value;
            TryUpdateUI();
        }
    }
    [Export] public ItemResource Type
    {
        get => _type;
        set
        {
            _type = value;
            TryUpdateUI();
        }
    }


    private GlobalStorageUI _ui;
    private int _storageValue;
    private ItemResource _type;


    protected override void ApplyUI(Control parent)
    {
        if (!IsInstanceValid(_ui))
        {
            _ui = GlobalStorageScene.Instantiate<GlobalStorageUI>();
            parent.AddChild(_ui);
        }
        TryUpdateUI();
    }
    
    private void TryUpdateUI()
    {
        if(!IsInstanceValid(_ui)) return;
        _ui.ClearItems();

        ItemResourceUI itemUI = DataBase.UI.ItemResource.Instantiate<ItemResourceUI>();
        itemUI.TextureRect.Texture = Type.Texture;
        itemUI.Title.Text = $"{Type.Name} +{StorageValue} Space";

        _ui.ItemsContainer.AddChild(itemUI);
    }
}
