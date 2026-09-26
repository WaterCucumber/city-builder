using System;
using CityBuilder.Data;
using Godot;

namespace CityBuilder.CustomNodes;


public class BuildingInstance
{
    public BuildingData Data { get; private set; }
    /// <summary>
    /// Top-left corner position
    /// </summary>
    public Vector2I OriginPosition { get; private set; }
    public Rect2I Rect => new(OriginPosition, Data.Size);
    public Rect2 VisibleRect => new((Vector2)OriginPosition * GridVisualiser.GridSize, (Vector2)Data.Size * GridVisualiser.GridSize);

    public BuildingInstance(BuildingData data, Vector2I myOrigin)
    {
        Data = data;
        OriginPosition = myOrigin;

        for (int i = 0; i < Data.Components.Length; i++)
        {
            Data.Components[i].AddedToBuilding(this);
        }
    }


    public delegate void NeighborChangedEvent(Vector2I myPartPosition, Vector2I changedGridPosition, GridData grid);
    public event NeighborChangedEvent NeighborChanged;
    public event Action Selected;
    public event Action Deselected;


    public void AddUI(Control parent)
    {
        for (int i = 0; i < Data.Components.Length; i++)
        {
            Data.Components[i].CreateUI(parent);
        }
    }

    public void FreeUI()
    {
        for (int i = 0; i < Data.Components.Length; i++)
        {
            Data.Components[i].FreeUI();
        }
    }

    public void Select()
    {
        Selected?.Invoke();
        var c = BuildManager.GetInstance().GridVisualiser.GetCanvasItem(OriginPosition);
        RenderingServer.CanvasItemClear(c);
        RenderingServer.CanvasItemAddTextureRect(c, new(VisibleRect.Position, VisibleRect.Size * 1.3f), Data.Texture.GetRid());
    }
    public void Deselect()
    {
        Deselected?.Invoke();
        var c = BuildManager.GetInstance().GridVisualiser.GetCanvasItem(OriginPosition);
        RenderingServer.CanvasItemClear(c);
        RenderingServer.CanvasItemAddTextureRect(c, VisibleRect, Data.Texture.GetRid());
    }
    public void OnNeighborChanged(Vector2I myPartPosition, Vector2I changedGridPosition, GridData grid) => NeighborChanged?.Invoke(myPartPosition, changedGridPosition, grid);
}
