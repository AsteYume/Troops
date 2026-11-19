using System.Collections.Generic;
using UnityEngine;

public class StateContext
{
    private Stack<Vector2Int> _tilesStack;
    private List<Vector2Int> _tilesInRange;
    private Unit _selectUnit;
    private Tile _selectTile;
    private Vector2Int _selectVector;
    
    public Stack<Vector2Int> TilesStack => _tilesStack;
    public List<Vector2Int> TilesInRange => _tilesInRange;
    public Unit SelectUnit => _selectUnit;
    public Tile SelectTile => _selectTile;
    public Vector2Int SelectVector => _selectVector;

    public void SetInRangeList(List<Vector2Int> tilesInRange)
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
    
    public void SetSelectVector(Vector2Int vector)
    {
        _selectVector = vector;
        MapDataProvider.Instance.TryGetTile(vector.x, vector.y, out _selectTile);
    }

}
