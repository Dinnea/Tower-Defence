using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleBuildingOption : MonoBehaviour
{
    [SerializeField] private MoneyManager _moneyManager;
    private Button thisButton;
    [SerializeField] private int choiceID;

    private void Awake()
    {
        thisButton = GetComponent<Button>();
    }
    private void OnEnable()
    {
        _moneyManager.canAfford += toggle;
    }
    private void OnDisable()
    {
        _moneyManager.canAfford -= toggle;
    }
    private void toggle(CanAfford canAfford)
    {
        if(choiceID == canAfford.id) thisButton.interactable = canAfford.value;
    }
}
