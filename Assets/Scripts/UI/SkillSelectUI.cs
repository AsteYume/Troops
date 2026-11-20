using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject _primaryObj;
    [SerializeField] private GameObject _secondaryObj;
    [SerializeField] private GameObject _tertiaryObj;
    [SerializeField] private Image _primaryIcon;
    [SerializeField] private Image _secondaryIcon;
    [SerializeField] private Image _tertiaryIcon;
    private SkillSO _primary;   
    private SkillSO _secondary;
    private SkillSO _tertiary;
    
    // ============================================================

    private void Start()
    {
        _primaryObj.SetActive(false);
        _secondaryObj.SetActive(false);
        _tertiaryObj.SetActive(false);
    }
    
    public void OpenSkillUI(Unit unit)
    {
        SetPrimary(unit);
        SetSecondary(unit);
        SetTertiary(unit);
    }

    public void CloseSkillUI()
    {
        ClearPrimary();
        ClearSecondary();
        ClearTertiary();
    }

    public void OnClick(SkillSlotEnum slot)
    {
        switch (slot)
        {
            case SkillSlotEnum.Primary:
                // 1번슬롯 스킬 실행
                break;
            case SkillSlotEnum.Secondary:
                break;
            case SkillSlotEnum.Tertiary:
                break;
            default:
                throw new NullReferenceException();
        }
    }
    
    // ============================================================

    private void SetPrimary(Unit unit)
    {
        if (unit.IsPrimaryExist)
        {
            _primary = unit.Primary;
            _primaryIcon.sprite = _primary.ButtonIcon;
            _primaryObj.SetActive(true);
        }
        else ClearPrimary();
    }
    
    private void SetSecondary(Unit unit)
    {
        if (unit.IsSecondaryExist)
        {
            _secondary = unit.Secondary;
            _secondaryIcon.sprite = _secondary.ButtonIcon;
            _secondaryObj.SetActive(true);
        }
        else ClearSecondary();
    }
    
    private void SetTertiary(Unit unit)
    {
        if (unit.IsTertiaryExist)
        {
            _tertiary = unit.Tertiary;
            _tertiaryIcon.sprite = _tertiary.ButtonIcon;
            _tertiaryObj.SetActive(true);
        }
        else ClearTertiary();
        
    }
    
    private void ClearPrimary()
    {
        _primary = null;
        _primaryIcon.sprite = null;
        _primaryObj.SetActive(false);
    }

    private void ClearSecondary()
    {
        _secondary = null;
        _secondaryIcon.sprite = null;
        _secondaryObj.SetActive(false);
    }

    private void ClearTertiary()
    {
        _tertiary = null;
        _tertiaryIcon.sprite = null;
        _tertiaryObj.SetActive(false);
    }
    
    
    
}
