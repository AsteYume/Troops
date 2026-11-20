using System.Collections.Generic;
using UnityEngine;

public class StateContext
{
    private Stack<Vector2Int> _tilesStack;
    private List<Vector2Int> _tilesInRange;
    private Unit _selectUnit;
    private Tile _selectTile;
    private Vector2Int _selectVector;
    private WeaponData _selectWeapon;
    private UtilityData _selectUtility;
    
    public Stack<Vector2Int> TilesStack => _tilesStack;
    public List<Vector2Int> TilesInRange => _tilesInRange;
    public Unit SelectUnit => _selectUnit;
    public Tile SelectTile => _selectTile;
    public Vector2Int SelectVector => _selectVector;
    public WeaponData SelectWeapon => _selectWeapon;
    public UtilityData SelectUtility => _selectUtility;

    public void SetStack(Stack<Vector2Int> tilesStack)
    {
        _tilesStack = tilesStack;
    }
    
    public void SetList(List<Vector2Int> tilesInRange)
    {
        _tilesInRange = tilesInRange;
    }

    public void SetUnit(Unit unit)
    {
        _selectUnit = unit;
    }
    
    public void SetTile(Tile tile)
    {
        _selectTile = tile;
    }
    
    public void SetVector(Vector2Int vector)
    {
        _selectVector = vector;
        MapDataProvider.Instance.TryGetTile(vector.x, vector.y, out _selectTile);
    }

    public void SetWeapon(WeaponData weapon)
    {
        _selectWeapon = weapon;
    }

    public void SetUtility(UtilityData utility)
    {
        _selectUtility = utility;
    }
    
    

    public void ClearRange()
    {
        SetList(null);
        SetStack(null);
        SetTile(null);
    }

}
