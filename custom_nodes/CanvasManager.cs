using Godot;

namespace LowLevelGeometryLab.Static;

[GlobalClass]
public partial class CanvasManager : Node2D
{
    private CanvasManager()
    {
        Item = GetCanvasItem();
        GD.Print("Canvas is Ready");
        _instance = this;
    }

    private static CanvasManager _instance;
    public static CanvasManager GetInstance()
    {
        return _instance;
    }

    private Rid? Item { get; set; }

    public Rid GetMainCanvasItem()
    {
        if (Item == null) throw new System.Exception("CanvasItem's RID is null.");
        return (Rid)Item;
    }

    public Rid CreateCanvasItem(int zIndex = 0)
    {
        var item = RenderingServer.CanvasItemCreate();
        RenderingServer.CanvasItemSetParent(item, GetMainCanvasItem());
        RenderingServer.CanvasItemSetZIndex(item, zIndex);
        return item;
    }
}
