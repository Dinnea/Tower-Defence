using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetectPrice : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;
    private int _id;
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _id = transform.parent.GetComponent<ToggleBuildingOption>().getID();
        List<BuildingTypeSO> buildingList = _gridManager.GetBuildingTypes();

        for (int i = 0; i < buildingList.Count; i++)
        {
            if (i == _id)
            {
                _text.text = "Cost: " + (buildingList[i].cost).ToString();
                break;
            }
        }
    }
}
