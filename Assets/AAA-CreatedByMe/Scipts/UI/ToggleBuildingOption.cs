using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleBuildingOption : MonoBehaviour
{
    [SerializeField] private MoneyManager _moneyManager;
    private Button thisButton;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private int choiceID;
  
    private void Awake()
    {
        thisButton = GetComponent<Button>();
    }
    private void OnEnable()
    {
        _moneyManager.onMoneyChanged += toggle;
    }
    private void OnDisable()
    {
        _moneyManager.onMoneyChanged -= toggle;
    }
    private void toggle(MoneyData canAfford)
    {
        if(choiceID == canAfford.id) thisButton.interactable = canAfford.value;
    }

    public int getID()
    {
        return choiceID;
    }
}
