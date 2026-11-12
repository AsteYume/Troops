using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "Scriptable Objects/SkillsSo")]
public abstract class SkillsSo : ScriptableObject
{
    private SkillsEnum _type;
    private bool _canUse;
    
    public SkillsEnum Type => _type;
    public bool CanUse => _canUse;

    public abstract void Prepare();
    public abstract void Use();



}
