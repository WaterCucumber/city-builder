using System.Collections.Generic;
using Godot;
using LowLevelGeometryLab.Static;

namespace CityBuilder.CustomNodes;


public class GridVisualiser
{
    public const float GridSize = 16;
    private readonly GridData _data;
    private readonly Dictionary<Vector2I, Rid> _canvasItems = [];

    
    public GridVisualiser(GridData data)
    {
        _data = data;
        _data.GridPlaceChanged += GridPlaceChanged;
    }


    public Rid GetCanvasItem(Vector2I gridPosition) => _canvasItems[gridPosition];


    private void GridPlaceChanged(Vector2I gridPosition)
    {
        var building = _data.GetBuildingAt(gridPosition);
        if(!_canvasItems.TryGetValue(gridPosition, out Rid canvasItem) && building != null) _canvasItems[gridPosition] = canvasItem = CanvasManager.GetInstance().CreateCanvasItem();
        if(building == null)
        {
            if(canvasItem.IsValid)  RenderingServer.CanvasItemClear(canvasItem);
        }
        else
        {
            var texture = building.Data.Texture;
            Vector2 position = (Vector2)gridPosition * GridSize;
            if (texture == null)
            {
                RenderingServer.CanvasItemAddCircle(canvasItem, position + Vector2.One * GridSize * 0.5f, 16f * 0.5f, Colors.Black);
            }
            else
            {
                Rect2 rect = new(position, (Vector2)building.Data.Size * GridSize);
                RenderingServer.CanvasItemAddTextureRect(canvasItem, rect, texture.GetRid());
            }    
        }
    }
}
