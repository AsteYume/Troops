using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Scriptable Objects/UnitDataSo")]
public class UnitDataSo : ScriptableObject
{
    [SerializeField] private UnitTypeEnum _unitType;
    [SerializeField] private string _unitName;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _maxSupply;
    [SerializeField] private int _maxActivePoint;
    [SerializeField] private int _activeRecover;
    [SerializeField] private int _sight;
    [SerializeField] private int _moveRange;
    [SerializeField] private SkillSO _primary;
    [SerializeField] private SkillSO _secondary;
    [SerializeField] private SkillSO _tertiary;

    public UnitTypeEnum UnitType => _unitType;
    public string UnitName => _unitName;
    public int MaxHealth => _maxHealth;
    public int MaxSupply => _maxSupply;
    public int MaxActivePoint => _maxActivePoint;
    public int ActiveRecover => _activeRecover;
    public int Sight => _sight;
    public int MoveRange => _moveRange;
    public SkillSO Primary => _primary;
    public SkillSO Secondary => _secondary;
    public SkillSO Tertiary => _tertiary;
}
