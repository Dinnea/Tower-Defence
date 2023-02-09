using Personal.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _currentGhost = null; //ghost chosen from the list
    private StructureGhost _ghostObject; //actual physical moved object

    [SerializeField] private LayerMask _layer;
    [Space]
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private LevelFSM _waveSpawner;
    [Space]
    [SerializeField] private Material _available;
    [SerializeField] private Material _unavailable;
   
    private Vector3 _location;

    private void Awake()
    {
        _ghostObject = GetComponentInChildren<StructureGhost>();
    }

    private void OnEnable()
    {
        _gridManager.onStructureChanged += SetGhostModel;
        _waveSpawner.onSwitchGameState += ModesChanged;
    }

    private void OnDisable()
    {
        _gridManager.onStructureChanged -= SetGhostModel;
        _waveSpawner.onSwitchGameState -= ModesChanged;
    }
    private void Update()
    {
        if(_currentGhost != null)
        {
            _location = Utilities.GetMousePositionWorld(Camera.main, _layer);
            _ghostObject.transform.position = _location;
            if (_gridManager.CanBuild(_location)) _ghostObject.ChangeMaterials(_available);
            else _ghostObject.ChangeMaterials(_unavailable);
        }      
    }

    public void SetGhostModel(StructureChanged structureChanged)
    {
        _currentGhost = structureChanged.test.prefab;
        _ghostObject.ChangeGhost(_currentGhost);
    }

    public void ModesChanged (object sender, bool value)
    {
        _currentGhost?.gameObject.SetActive(value);
    }
}
