using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureGhost : MonoBehaviour
{
    [SerializeField] Renderer[] _renderers;

    void Start()
    {
        _renderers = GetComponentsInChildren<Renderer>();
    }
    public void ChangeMaterials(Material material)
    {
            foreach(Renderer renderer in _renderers)
            {
                renderer.sharedMaterial = material;
            }               
    }
}
