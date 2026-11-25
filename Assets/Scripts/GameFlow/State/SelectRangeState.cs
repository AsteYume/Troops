using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectRangeState : State
{
    private const StateEnum Type = StateEnum.SelectRange;
    private InputReader _inputReader;
    private RangeViewer _rangeViewer;
    private StateContext _stateContext;
    
    // ============================================================

    public SelectRangeState(InputReader inputReader, RangeViewer rangeViewer,  StateContext context)
    {
        _inputReader = inputReader;
        _rangeViewer = rangeViewer;
        _stateContext = context;
    }
    
    // ============================================================
    
    public override StateEnum StateType => Type;
    public override event Action<StateEnum> StateEnd;
    
    // ============================================================
    public override void OnEnter()
    {
        // 스킬 판별
        SkillsEnum type = CheckSkillType();
        List<Vector2Int> range;
        MarkerType marker;
        switch (type)
        {
            case SkillsEnum.Attack:
                range = SetWeaponRange(out marker);
                break;
            case SkillsEnum.Utility:
                range = SetUtilityRange(out marker);
                break;
            default:
                throw new ArgumentNullException();
        }
        _stateContext.SetList(range);
        _rangeViewer.ListView(range,marker);
        _inputReader.Confirm += OnConfirm;
        _inputReader.Cancel += OnCancel;
    }

    public override void OnExit()
    {
        _inputReader.Confirm -= OnConfirm;
        _inputReader.Cancel -= OnCancel;
    }
    
    // ============================================================

    private List<Vector2Int> SetWeaponRange(out MarkerType marker)
    {
        List<Vector2Int> range = new List<Vector2Int>();
        var type = _stateContext.SelectWeapon.AttackType;
        var center = _stateContext.SelectVector;
        var min = _stateContext.SelectWeapon.MinRange;
        var max = _stateContext.SelectWeapon.MaxRange;
        switch (type)
        {
            case WeaponAttackType.SingleTarget:
                range = LocateAlgorithm.Bfs(center, max, min, UnitTeam.Red);
                marker = MarkerType.TileRed;
                break;
            case WeaponAttackType.RadiusCone:
                range.AddRange(ConeAlgorithm.Cone(center, max, _stateContext.SelectUnit.Direction, UnitTeam.None));
                marker = MarkerType.TileRed;
                break;
            case WeaponAttackType.RadiusCircle:
                range = LocateAlgorithm.Bfs(center, max, min, UnitTeam.None);
                marker = MarkerType.TileRed;
                break;
            default:
                throw new ArgumentNullException();
        }
        return range;
    }

    private List<Vector2Int> SetUtilityRange(out MarkerType marker)
    {
        List<Vector2Int> range = new List<Vector2Int>();
        var type = _stateContext.SelectUtility.UtilityType;
        var center = _stateContext.SelectVector;
        var min = _stateContext.SelectUtility.MinRange;
        var max = _stateContext.SelectUtility.MaxRange;
        switch (type)
        {
            case UtilityType.SelectDirection :
                range.AddRange(ConeAlgorithm.Cone(center, max, DirectionEnum.North, UnitTeam.None));
                range.AddRange(ConeAlgorithm.Cone(center, max, DirectionEnum.South, UnitTeam.None));
                range.AddRange(ConeAlgorithm.Cone(center, max, DirectionEnum.East, UnitTeam.None));
                range.AddRange(ConeAlgorithm.Cone(center, max, DirectionEnum.West, UnitTeam.None));
                marker = MarkerType.TileRed;
                break;
            case UtilityType.TargetAlly :
                range = LocateAlgorithm.Bfs(center, max, min, UnitTeam.Blue);
                marker = MarkerType.UnitGreen;
                break;
            case UtilityType.TargetTile :
                range = LocateAlgorithm.Bfs(center, max, min, UnitTeam.None);
                marker = MarkerType.TileGreen;
                break;
            default:
                throw new ArgumentNullException();
        }
        return range;
    }
    
    private SkillsEnum CheckSkillType()
    {
        switch (_stateContext.SelectSkill.SkillType)
        {
            case SkillsEnum.Attack :
                _stateContext.SetWeapon((WeaponData)_stateContext.SelectSkill);
                break;
            case SkillsEnum.Utility :
                _stateContext.SetUtility((UtilityData)_stateContext.SelectSkill);
                break;
            default :
                throw new ArgumentNullException();
        }
        return _stateContext.SelectSkill.SkillType;
    }
    
    private void OnCancel()
    {
        ClearView();
        StateEnd?.Invoke(StateEnum.SelectSkill);
    }
    
    private void OnConfirm()
    {
        if (!_inputReader.TryGetVector(out Vector2Int vec)) return; // 맵 밖 클릭시 return
        if (!_stateContext.TilesInRange.Contains(vec)) return;
        _stateContext.SetVector(vec);
        
        
        
        // 실행후 state 변경
        ClearView();
        StateEnd?.Invoke(StateEnum.Skill);
    }
    
    private void ClearView()
    {
        _rangeViewer.ClearViewerObject();
        _stateContext.SetList(null);
    }
    
    // ============================================================
    
    
}
