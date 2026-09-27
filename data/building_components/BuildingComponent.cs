using CityBuilder.CustomNodes;
using Godot;

namespace CityBuilder.Data.BuildingComponents;


public abstract partial class BuildingComponent : Resource
{
    private Control _uiParent;


    public void CreateUI(Control parent)
    {
        _uiParent = new PanelContainer
        {
            SizeFlagsHorizontal = Control.SizeFlags.ExpandFill,
        };
        _uiParent.AddThemeStyleboxOverride("panel", new StyleBoxEmpty());
        parent.AddChild(_uiParent);
        ApplyUI(_uiParent);
    }

    public void FreeUI()
    {
        if(IsInstanceValid(_uiParent)) _uiParent.QueueFree();
    }

    public virtual void AddedToBuilding(BuildingInstance building) {}
    public virtual void RemovedFromBuilding(BuildingInstance building) {}

    protected abstract void ApplyUI(Control parent);
}
