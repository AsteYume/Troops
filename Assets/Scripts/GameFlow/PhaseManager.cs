using System;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    private static PhaseManager _instance;
    private static int _stageNumber;
    static PhaseEnum _currentPhase = PhaseEnum.None;

    private PreparePhase _prepare;
    // private StartPhase _start;
    // private PlayerPhase _player;
    // private EnemyPhase _enemy;
    // private ResultPhase _result;
    

    // ============================================================
    public event Action<PhaseEnum> PhaseStart;
    public int StageNumber => _stageNumber;

    // ============================================================

    public static PhaseManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<PhaseManager>();
            }
            if (_instance == null)
            {
                GameObject go = new GameObject();
                go.AddComponent<PhaseManager>();
            }
            return _instance;
        }
    }
    
    // ============================================================

    private void Awake()
    {
        SetSingleTon();
        Init();
    }

    private void Start()
    {
        // StageStart(1);
    }

    // ============================================================
    
    public void GameEnd()
    {
        // 게임 결과 검사기에서 유닛 제거마다 조건 확인
        // 페이즈를 Player / Enemy에서 Result 페이즈로 이동
        
    }

    public void PhaseChange(PhaseEnum phase)
    {
        _currentPhase = CheckNextPhase(phase);
        StartNextPhase();
    }
    
    // ============================================================
    
    private void StageStart(int stage)
    {
        SetValue(stage);
        StartNextPhase();
    }

    private void StartNextPhase()
    {
        _currentPhase = CheckNextPhase(_currentPhase);
        PhaseStart?.Invoke(_currentPhase);
    }

    private PhaseEnum CheckNextPhase(PhaseEnum phase)
    {
        switch (phase)
        {
            case PhaseEnum.None:
                return PhaseEnum.Prepare;
            case PhaseEnum.Prepare:
                return PhaseEnum.Start;
            case PhaseEnum.Start:
                return PhaseEnum.Player;
            case PhaseEnum.Player:
                return PhaseEnum.Enemy;
            case PhaseEnum.Enemy:
                return PhaseEnum.Player;
            case PhaseEnum.Result:
                return PhaseEnum.None;
        }
        return PhaseEnum.None;
    }
    
    private void Init()
    {
        _prepare = new PreparePhase();
    }

    private void SetValue(int stage)
    {
        _stageNumber = stage;
        _currentPhase = PhaseEnum.None;
    }
    
    private void SetSingleTon()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
}
