using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aoeFX : MonoBehaviour
{
    [SerializeField] private AbstractTower _owner;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private float _fadeSpeed = 0.5f;
    private Color _originalColor;
    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
        _originalColor = _mesh.material.color;
        _owner = GetComponentInParent<AbstractTower>();
    }
    private void Start()
    {
        setRadius(_owner.range.radius);
        _owner.onAction += triggerVFX;
    }
    private void setRadius(float radius)
    {
        transform.localScale = new Vector3(radius*2, transform.localScale.y, radius*2);
    }

    private void triggerVFX()
    {
        _mesh.enabled = true;
        StartCoroutine(FadeOut());

    }

    private IEnumerator FadeOut()
    {
        while (_mesh.material.color.a > 0)
        {
            Color color = _mesh.material.color;
            float fadeAmount = color.a - (_fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fadeAmount);
            _mesh.material.color = color;
            yield return null;
        }
        _mesh.material.color = _originalColor;
        _mesh.enabled = false;
    }
}
