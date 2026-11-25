public class EnemyUnit : Unit
{
    private EnemyAIGroup _aiGroup;
    
    public EnemyAIGroup AIGroup => _aiGroup;
    
    public EnemyUnit(UnitDataSo unitData, DirectionEnum direction, int x, int z, AIType ai, EnemyAIGroup aiGroup) 
        : base(unitData, direction, x, z, ai)
    {
        _team = UnitTeam.Red;
        _aiGroup = aiGroup;
    }
}
