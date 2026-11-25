public class AllyUnit : Unit
{
    public AllyUnit(UnitDataSo unitData, DirectionEnum direction, int x, int z, AIType ai) 
        : base(unitData, direction, x, z, ai)
    {
        _team = UnitTeam.Blue;
    }
}
