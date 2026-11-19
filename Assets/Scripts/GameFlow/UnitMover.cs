using UnityEngine;

public class UnitMover : MonoBehaviour
{
    MoveState _moveState;

    public void SetMoveState(MoveState moveState)
    {
        _moveState = moveState;
    }
    
    void Update()
    {
        _moveState.Moving();
    }
}
