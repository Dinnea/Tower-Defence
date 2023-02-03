using Personal.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDisplay : MonoBehaviour
{
    [SerializeField] private List<Transform> _ghosts;
    [SerializeField] private Transform _currentGhost;
    [SerializeField] private LayerMask _layer;
    [Space]
    [SerializeField] private StructureController _structureController;
    [SerializeField] private WaveSpawner _waveSpawner;
    [Space]
    [SerializeField] private Material _available;
    [SerializeField] private Material _unavailable;
    [Space]
    [SerializeField] private StructureGhost _canYouBuild;
    private Vector3 _location;

    private void Awake()
    {
        _currentGhost = _ghosts[0];
        _canYouBuild = _ghosts[0].GetComponent<StructureGhost>();
        _waveSpawner.switchGameState += ModesChanged;
    }


    private void Update()
    {
        _location = Utilities.GetMousePositionWorld(Camera.main, _layer);
        _currentGhost.position = _location;
        
        if (_structureController.CanBuild(_location)) _canYouBuild.ChangeMaterials(_available);
        else _canYouBuild.ChangeMaterials(_unavailable);
        
        
        //_test.visual.position = Utilities.GetMousePositionWorld(Camera.main, _layer);
    }
    public void SetBuilding(int i)
    {
        if (i >= 0 && i <= 5)
        {
            _ghosts[i].gameObject.SetActive(true);
            _currentGhost.gameObject.SetActive(false);
            _currentGhost = _ghosts[i];
            _canYouBuild = _ghosts[i].GetComponent<StructureGhost>();
        }
        else Debug.Log("No such building");
    }

    public void ModesChanged (object sender, bool value)
    {
        _currentGhost?.gameObject.SetActive(value);
    }
}
