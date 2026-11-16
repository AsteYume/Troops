using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    PhaseEnum _currentPhase;

    private void Awake()
    {
        _currentPhase = PhaseEnum.None;
    }




    private void StartNextPhase()
    {
        
    }
    
    private void CheckNextPhase()
    {
        switch (_currentPhase)
        {
            case PhaseEnum.None:
                _currentPhase = PhaseEnum.Init;
                break;
            case PhaseEnum.Init:
                _currentPhase = PhaseEnum.Start;
                break;
            case PhaseEnum.Start:
                _currentPhase = PhaseEnum.Ally;
                break;
            case PhaseEnum.Ally:
                _currentPhase = PhaseEnum.Enemy;
                break;
            case PhaseEnum.Enemy:
                _currentPhase = PhaseEnum.Ally;
                break;
            case PhaseEnum.Result:
                _currentPhase = PhaseEnum.None;
                break;
        }
    }
    
    
    
    
    
}
