using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureGhost : MonoBehaviour
{
    [SerializeField] private List<Renderer> _renderers;
    [SerializeField] private GameObject _ghostObject = null;

    public void ChangeMaterials(Material material)
    {
        foreach(Renderer renderer in _renderers)
        {
            if (renderer!= null) renderer.sharedMaterial = material;
        }               
    }

    public void ChangeGhost(GameObject ghost)
    {
        _renderers.Clear();
        if (_ghostObject != null)
        {
            Destroy(_ghostObject);
        }
        
        _ghostObject = Instantiate(ghost, this.transform);
        Renderer[] temp = _ghostObject.GetComponentsInChildren<Renderer>();
        _renderers.Clear();
        foreach ( Renderer renderer in temp)
        {
            if (renderer.gameObject.CompareTag("TowerMesh"))_renderers.Add(renderer);
        }
        //_renderers = GetComponentsInChildren<Renderer>();
    }
}
