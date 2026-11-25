public class EnemyAI
{
    private EnemyAIGroup _aiGroup;
    private AIType _aiType;
    private int _targetX;
    private int _targetZ;
    
    public EnemyAIGroup AIGroup => _aiGroup;
    public AIType AIType => _aiType;
    public int TargetX => _targetX;
    public int TargetZ => _targetZ;

    public EnemyAI(EnemyAIGroup aiGroup, AIType aiType, int x, int z)
    {
        _aiGroup = aiGroup;
        _aiType = aiType;
        _targetX = x;
        _targetZ = z;
    }
}
