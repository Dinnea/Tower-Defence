using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private MoneyManager _moneyManager;
    private TextMeshProUGUI _moneyDisplay;
    private void Awake()
    {
        _moneyDisplay = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _moneyManager.onMoneyChanged += setMoney;
    }

    private void OnDisable()
    {
        _moneyManager.onMoneyChanged -= setMoney;
    }

    private void setMoney(MoneyData moneyData)
    {
        _moneyDisplay.text = "Money: " + moneyData.money.ToString();
    }


}
