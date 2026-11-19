using UnityEngine;

public abstract class SkillSO : ScriptableObject
{
    public abstract SkillsEnum SkillType { get; }
    public abstract int TagNumber { get; }
    public abstract string Name { get; }
    public abstract int SupplyCost { get; }
    public abstract int ActiveCost { get; }
    public abstract Sprite ButtonIcon { get; }
}
