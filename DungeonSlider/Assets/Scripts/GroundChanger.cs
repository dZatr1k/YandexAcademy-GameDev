using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundChanger : MonoBehaviour
{
    public UnityAction OnColorChanged;

    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Color _color;

    private bool _isColorChanged = false;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_isColorChanged == false)
        {
            if(collision.TryGetComponent(out Player player))
            {
                _renderer.color = _color;
                _isColorChanged = true;
                OnColorChanged?.Invoke();
            }
        }
    }
}
