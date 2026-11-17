using UnityEngine;

public class StateManager : MonoBehaviour
{
    private StateEnum _currentState = StateEnum.Select;
    
    public StateEnum CurrentState => _currentState;
    public MapDataProvider MapData => MapManager.Instance.MapDataProvider;
    
    
    
    
    
    
    
    
   
}
