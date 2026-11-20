using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectUI : MonoBehaviour
{
    private GameObject _primaryObj;
    private GameObject _secondaryObj;
    private GameObject _tertiaryObj;
    private SkillSO _primary;   
    private SkillSO _secondary;
    private SkillSO _tertiary;
    private Image _primaryIcon;
    private Image _secondaryIcon;
    private Image _tertiaryIcon;
    
    // ============================================================
    
    public void SetSkillUI(Unit unit)
    {
        SetPrimary(unit);
        SetSecondary(unit);
        SetTertiary(unit);
    }

    public void ClearSkillUI()
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
