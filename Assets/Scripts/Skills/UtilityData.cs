using UnityEngine;

[CreateAssetMenu(fileName = "UtilityData", menuName = "Scriptable Objects/Skills")]
public class UtilityData : SkillSO
{
    [SerializeField] private SkillsEnum _skillType;
    [SerializeField] private int _tagNumber;
    [SerializeField] private string _name;
    [SerializeField] private int _supplyCost;
    [SerializeField] private int _activeCost;
    [SerializeField] private Sprite _buttonIcon;

    [SerializeField] private UtilityType _utilityType;
    [SerializeField] private UnitTeam _targetType;
    [SerializeField] private int _minRange;
    [SerializeField] private int _maxRange;
    [SerializeField] private int _radius;
    [SerializeField] private int _duraction;
    
    public override SkillsEnum SkillType => _skillType;
    public override int TagNumber => _tagNumber;
    public override string Name => _name;
    public override int SupplyCost => _supplyCost;
    public override int ActiveCost => _activeCost;
    public override Sprite ButtonIcon => _buttonIcon;

    public UtilityType UtilityType => _utilityType;
    public UnitTeam TargetType => _targetType;
    public int MinRange => _minRange;
    public int MaxRange => _maxRange;
    public int Radius => _radius;
    public int Duraction => _duraction;
}
