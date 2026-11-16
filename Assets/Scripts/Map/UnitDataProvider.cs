using System;
using UnityEngine;
using System.Collections.Generic;

public class UnitDataProvider : MonoBehaviour
{
    [SerializeField] private TextAsset _allyData;
    [SerializeField] private TextAsset _enemyData;
    [SerializeField] private TextAsset _aiData;

    private List<Unit> _unitList = new List<Unit>();
    private int _unitCount = 0;
    
    
    
    private void ReadData()
    {
        ReadAllyUnit();
        ReadEnemyUnit();
        ReadEnemyAI();
    }
    
    // ============================================================

    private void ReadAllyUnit()
    {
        string[] lines = _allyData.text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        
        for (int i = 1;  i < lines.Length; i++)
        {
            ParseAllyLine(lines[i]);
        }
    }

    private void ReadEnemyUnit()
    {
        string[] lines = _enemyData.text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        
        for (int i = 1;  i < lines.Length; i++)
        {
            ParseEnemyLine(lines[i]);
        }
    }
    
    private void ReadEnemyAI()
    {
        string[] lines = _aiData.text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        
        for (int i = 1;  i < lines.Length; i++)
        {
            ParseAILine(lines[i]);
        }
    }
    
    // ============================================================
    
    private void ParseAllyLine(string line)  
    {  
        string[] cells = line.Split(',');

        TrimCells(cells);

        if (Enum.TryParse(cells[0], out UnitTypeEnum ai)  
            && int.TryParse(cells[1], out int startX)  
            && int.TryParse(cells[2], out int startZ)  
            && Enum.TryParse(cells[3], out DirectionEnum direction))
        {
            
        }
        
    }
    
    private void ParseAILine(string line)  
    {  
        string[] cells = line.Split(',');

        TrimCells(cells);

        if (Enum.TryParse(cells[0], out EnemyAIType aiType)  
            && int.TryParse(cells[1], out int targetX)  
            && int.TryParse(cells[2], out int targetZ)  
            && Enum.TryParse(cells[3], out EnemyAIGroup aiGroup))
        {
            
        }
        
    }
    
    private void ParseEnemyLine(string line)  
    {  
        string[] cells = line.Split(',');

        TrimCells(cells);   

        if (Enum.TryParse(cells[0], out UnitTypeEnum ai)  
            && int.TryParse(cells[1], out int startX)  
            && int.TryParse(cells[2], out int startZ)  
            && Enum.TryParse(cells[3], out DirectionEnum direction)
            && Enum.TryParse(cells[5], out EnemyAIType aiType)
            && Enum.TryParse(cells[6], out EnemyAIGroup aiGroup))
        {
            
        }
    }
    
    // ============================================================
    
    private void TrimCells(string[] cells)  
    {  
        for (int i = 0; i < cells.Length; i++)  
        {  
            cells[i] = cells[i].Trim();  
        }  
    }

    private void ResetData()
    {
        _allyData = null;
        _enemyData = null;
        _aiData = null;
        _unitList.Clear();
        _unitCount = 0;
    }
}
