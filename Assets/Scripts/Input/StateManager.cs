using UnityEngine;

public class StateManager : MonoBehaviour
{
    private StateEnum _currentState = StateEnum.Idle;
    
    public StateEnum CurrentState => _currentState;
    public MapDataProvider MapData => MapManager.Instance.MapDataProvider;
    
    
    public void SetState(StateEnum newState)
    {
        _currentState = newState;
    }
    
    
    
    
    
   
}
