using UnityEngine;

public class UnitPlacer : MonoBehaviour
{
    private UnitDataProvider _provider;
    
    public void PlaceUnits(UnitDataProvider provider)
    {
        _provider = provider;
        PlaceAllyUnits();
        PlaceEnemyUnits();
    }

    private void PlaceAllyUnits()
    {
        foreach (AllyUnit data in StageData.Instance.AllyUnits)
        {
            int x = data.X, z = data.Z;
            Vector3 vec = new  Vector3(x, 0, z);
            GameObject prefab = _provider.GetPrefab(data.UnitType, UnitTeam.Blue);
            GameObject unit = Instantiate(prefab);
            data.SetPrefab(unit);
            unit.transform.position = vec;
        }
    }
    
    private void PlaceEnemyUnits()
    {
        foreach (EnemyUnit data in StageData.Instance.EnemyUnits)
        {
            int x = data.X, z = data.Z;
            Vector3 vec = new Vector3(x, 0, z);
            GameObject prefab = _provider.GetPrefab(data.UnitType, UnitTeam.Red);
            GameObject unit = Instantiate(prefab);
            data.SetPrefab(unit);
            unit.transform.position = vec;
        }
    }
    
    
}
