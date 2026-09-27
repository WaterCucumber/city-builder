using System;
using Godot;

namespace CityBuilder.CustomNodes;


public class GridData(Vector2I gridSize)
{
    public event Action<Vector2I> GridPlaceChanged;
    public event Action<Vector2I> GridPlaceAdded;
    public event Action<Vector2I> GridPlaceRemoving;

    private BuildingInstance[,] _buildings = new BuildingInstance[gridSize.X, gridSize.Y];
    

    public void PlaceBuilding(Rect2I buildingRect, BuildingInstance value)
    {
        var origin = buildingRect.Position;
        var size = buildingRect.Size;

        if(value == null)
        {
            GridPlaceRemoving?.Invoke(origin);
            GetBuildingAt(origin).Delete();
        }

        for (int x = 0; x < size.X; x++)
        {
            for (int y = 0; y < size.Y; y++)
            {
                _buildings[origin.X + x, origin.Y + y] = value;
            }
        }


        NotifyNeighbors(origin, size);
        GridPlaceChanged?.Invoke(origin);
        if(value != null) GridPlaceAdded?.Invoke(origin);
    }

    public bool IsInsideGrid(Vector2I position) => position.X >= 0 && position.X < _buildings.GetLength(0) && position.Y >= 0 && position.Y < _buildings.GetLength(1);

    public BuildingInstance GetBuildingOrDefault(Vector2I position, BuildingInstance value = null)
    {
        if(IsInsideGrid(position) == false) return value;
        return _buildings[position.X, position.Y];
    }
    public BuildingInstance GetBuildingAt(Vector2I positon) => _buildings[positon.X, positon.Y];

    private void NotifyNeighbors(Vector2I origin, Vector2I size)
    {
        for (int x = -1; x < size.X + 1; x++)
        {
            for (int y = -1; y < size.Y + 1; y++)
            {
                // NOT Outline check
                if(x != -1 && x != size.X && y != -1 && y != size.Y) continue;
                // NOT Corners
                if(x == -1 && y == -1 || x == size.X && y == -1 || x == -1 && y == size.Y || x == size.X && y == size.Y) continue;

                Vector2I partPosition = new(origin.X + x, origin.Y + y);

                // Bounds check
                if(!IsInsideGrid(partPosition)) continue;

                // Notify only outline
                _buildings[partPosition.X, partPosition.Y]?.OnNeighborChanged(partPosition, origin, this);
            }
        }
    }
}
