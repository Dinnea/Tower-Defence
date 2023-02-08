using Personal.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDisplay : MonoBehaviour
{
    [SerializeField] private List<BuildingTypeSO> _buildingChoices;
    [SerializeField] private GameObject _currentGhost = null; //ghost chosen from the list
    private StructureGhost _ghostObject; //actual physical moved object

    [SerializeField] private LayerMask _layer;
    [Space]
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private WaveSpawner _waveSpawner;
    [Space]
    [SerializeField] private Material _available;
    [SerializeField] private Material _unavailable;
   
    private Vector3 _location;

    private void Awake()
    {
        _buildingChoices = _gridManager.GetBuildingTypes();
        _currentGhost = _buildingChoices[0].prefab;
       
        _ghostObject = GetComponentInChildren<StructureGhost>();
        //Instantiate(_currentGhost, _ghostObject.transform);
        _ghostObject.ChangeGhost(_currentGhost);
    }

    private void OnEnable()
    {
        _waveSpawner.switchGameState += ModesChanged;
    }

    private void OnDisable()
    {
        _waveSpawner.switchGameState -= ModesChanged;
    }
    private void Update()
    {
        _location = Utilities.GetMousePositionWorld(Camera.main, _layer);
        _ghostObject.transform.position = _location;
        if (_gridManager.CanBuild(_location)) _ghostObject.ChangeMaterials(_available);
        else _ghostObject.ChangeMaterials(_unavailable);
        
        
        //_test.visual.position = Utilities.GetMousePositionWorld(Camera.main, _layer);
    }
    public void SetBuilding(int i)
    {
        if (i >= 0 && i <= 5)
        {
            _currentGhost = _buildingChoices[i].prefab;

            _ghostObject.ChangeGhost(_currentGhost);
        }
        else Debug.Log("No such building");
    }

    public void ModesChanged (object sender, bool value)
    {
        _currentGhost?.gameObject.SetActive(value);
    }
}
