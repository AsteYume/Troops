using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject _primaryObj;
    [SerializeField] private GameObject _secondaryObj;
    [SerializeField] private GameObject _tertiaryObj;
    [SerializeField] private Button _primaryButton;
    [SerializeField] private Button _secondaryButton;
    [SerializeField] private Button _tertiaryButton;
    private SkillSO _primary;   
    private SkillSO _secondary;
    private SkillSO _tertiary;
    
    public event Action<SkillSlotEnum> OnSkillSelected;
    
    // ============================================================

    private void Start()
    {
        _primaryObj.SetActive(false);
        _secondaryObj.SetActive(false);
        _tertiaryObj.SetActive(false);
    }

    private void OnEnable()
    {
        _primaryButton.onClick.AddListener(() => OnSkillSelected(SkillSlotEnum.Primary));
        _secondaryButton.onClick.AddListener(() => OnSkillSelected(SkillSlotEnum.Secondary));
        _tertiaryButton.onClick.AddListener(() => OnSkillSelected(SkillSlotEnum.Tertiary));
    }

    private void OnDisable()
    {
        _primaryButton.onClick.RemoveListener(() => OnSkillSelected(SkillSlotEnum.Primary));
        _secondaryButton.onClick.RemoveListener(() => OnSkillSelected(SkillSlotEnum.Secondary));
        _tertiaryButton.onClick.RemoveListener(() => OnSkillSelected(SkillSlotEnum.Tertiary));
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
    
    // ============================================================

    private void SetPrimary(Unit unit)
    {
        if (unit.IsPrimaryExist)
        {
            _primary = unit.Primary;
            _primaryButton.image.sprite = _primary.ButtonIcon;
            _primaryObj.SetActive(true);
        }
        else ClearPrimary();
    }
    
    private void SetSecondary(Unit unit)
    {
        if (unit.IsSecondaryExist)
        {
            _secondary = unit.Secondary;
            _secondaryButton.image.sprite = _secondary.ButtonIcon;
            _secondaryObj.SetActive(true);
        }
        else ClearSecondary();
    }
    
    private void SetTertiary(Unit unit)
    {
        if (unit.IsTertiaryExist)
        {
            _tertiary = unit.Tertiary;
            _tertiaryButton.image.sprite = _tertiary.ButtonIcon;
            _tertiaryObj.SetActive(true);
        }
        else ClearTertiary();
        
    }
    
    private void ClearPrimary()
    {
        _primary = null;
        _primaryButton.image.sprite = null;
        _primaryObj.SetActive(false);
    }

    private void ClearSecondary()
    {
        _secondary = null;
        _secondaryButton.image.sprite = null;
        _secondaryObj.SetActive(false);
    }

    private void ClearTertiary()
    {
        _tertiary = null;
        _tertiaryButton.image.sprite = null;
        _tertiaryObj.SetActive(false);
    }
    
    
    
}
