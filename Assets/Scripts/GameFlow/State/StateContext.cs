using System.Collections.Generic;
using UnityEngine;

public class StateContext
{
    private Stack<Vector2Int> _tilesStack;
    private List<Vector2Int> _tilesInRange;
    private Unit _selectUnit;
    private Tile _selectTile;
    
    public Stack<Vector2Int> TilesStack => _tilesStack;
    public List<Vector2Int> TilesInRange => _tilesInRange;
    public Unit CurrentUnit => _selectUnit;

    public void SetTilesInRange(List<Vector2Int> tilesInRange)
    {
        _tilesInRange = tilesInRange;
    }

    public void SetSelectUnit(Unit unit)
    {
        _selectUnit = unit;
    }

    public void SetTilesStack(Stack<Vector2Int> tilesStack)
    {
        _tilesStack = tilesStack;
    }
    
    public bool TrySetSelectTile(Vector2Int vector)
    {
        return MapDataProvider.Instance.TryGetTile(vector.x, vector.y, out _selectTile);
    }
}
