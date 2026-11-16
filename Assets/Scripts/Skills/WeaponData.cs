using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/Skills")]
public class WeaponData : SkillSO
{
    [SerializeField] private SkillsEnum _skillType;
    [SerializeField] private int _tagNumber;
    [SerializeField] private string _name;
    [SerializeField] private int _supplyCost;
    [SerializeField] private int _activeCost;

    [SerializeField] private WeaponEnum _attackType;
    [SerializeField] private int _damage;
    [SerializeField] private int _shots;
    [SerializeField] private int _pierce;
    [SerializeField] private int _minRange;
    [SerializeField] private int _maxRange;
    [SerializeField] private int _radius;
    [SerializeField] private int _accuracy;
    [SerializeField] private bool _isNeedMount;

    public override SkillsEnum SkillType => _skillType;
    public override int TagNumber => _tagNumber;
    public override string Name => _name;
    public override int SupplyCost => _supplyCost;
    public override int ActiveCost => _activeCost;

    public WeaponEnum AttackType => _attackType;
    public int Damage => _damage;
    public int Shots => _shots;
    public int Pierce => _pierce;
    public int MinRange => _minRange;
    public int MaxRange => _maxRange;
    public int Radius => _radius;
    public int Accuracy => _accuracy;
    public bool IsNeedMount => _isNeedMount;
}
