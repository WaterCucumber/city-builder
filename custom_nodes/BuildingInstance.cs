using System;
using System.Collections.Generic;
using CityBuilder.Data;
using CityBuilder.Data.BuildingComponents;
using Godot;

namespace CityBuilder.CustomNodes;


public class BuildingInstance
{
    public delegate void NeighborChangedEvent(Vector2I myPartPosition, Vector2I changedGridPosition, GridData grid);
    public event NeighborChangedEvent NeighborChanged;
    public event Action Deleting;
    public event Action Selected;
    public event Action Deselected;

    public BuildingData Data { get; private set; }
    /// <summary>
    /// Top-left corner position
    /// </summary>
    public Vector2I OriginPosition { get; private set; }
    public Rect2I Rect => new(OriginPosition, Data.Size);
    public Rect2 VisibleRect => new((Vector2)OriginPosition * GridVisualiser.GridSize, (Vector2)Data.Size * GridVisualiser.GridSize);
    public bool IsSelected { get; set; } = false;

    private readonly BuildingComponent[] _components;

    public BuildingInstance(BuildingData data, Vector2I myOrigin)
    {
        Data = data;
        OriginPosition = myOrigin;

        _components = new BuildingComponent[data.BaseComponents.Length];
        for (int i = 0; i < _components.Length; i++)
        {
            _components[i] = (BuildingComponent)data.BaseComponents[i].Duplicate();
            _components[i].AddedToBuilding(this);
        }
    }


    public IEnumerable<BuildingComponent> GetComponents() => [.. _components];

    public void AddUI(Control parent)
    {
        for (int i = 0; i < _components.Length; i++)
        {
            _components[i].CreateUI(parent);
        }
    }
    public void FreeUI()
    {
        for (int i = 0; i < _components.Length; i++)
        {
            _components[i].FreeUI();
        }
    }
    public void Delete()
    {
        Deleting?.Invoke();
        for (int i = 0; i < _components.Length; i++)
        {
            _components[i].RemovedFromBuilding(this);
        }
    }
    public void Select()
    {
        IsSelected = true;
        Selected?.Invoke();
        var c = BuildManager.GetInstance().GridVisualiser.GetCanvasItem(OriginPosition);
        RenderingServer.CanvasItemClear(c);
        RenderingServer.CanvasItemAddEllipse(c, VisibleRect.Position + VisibleRect.Size * 0.5f, VisibleRect.Size.X * 0.5f + 4, VisibleRect.Size.Y * 0.5f + 4, Colors.White);
        RenderingServer.CanvasItemAddTextureRect(c, new(VisibleRect.Position - Vector2.One * 0, VisibleRect.Size + Vector2.One * 0), Data.Texture.GetRid(), modulate: new Color(1f,1f,0.7f,1f));
    }
    public void Deselect()
    {
        IsSelected = false;
        Deselected?.Invoke();
        var c = BuildManager.GetInstance().GridVisualiser.GetCanvasItem(OriginPosition);
        RenderingServer.CanvasItemClear(c);
        RenderingServer.CanvasItemAddTextureRect(c, VisibleRect, Data.Texture.GetRid());
    }
    public void OnNeighborChanged(Vector2I myPartPosition, Vector2I changedGridPosition, GridData grid) => NeighborChanged?.Invoke(myPartPosition, changedGridPosition, grid);
}
