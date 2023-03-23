using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class HatchOpener : MonoBehaviour
{
    public static UnityAction OnHatchOpened;

    private List<GroundChanger> _groundChangers;
    private int _coloredBlockCount = 0;

    private void OnEnable()
    {
        _groundChangers = FindObjectsOfType<GroundChanger>().ToList();

        foreach (var item in _groundChangers)
        {
            item.OnColorChanged += TryOpenHatch;
        }
    }

    private void OnDisable()
    {
        foreach (var item in _groundChangers)
        {
            item.OnColorChanged -= TryOpenHatch;
        }
    }

    private void TryOpenHatch()
    {
        _coloredBlockCount++;
        
        if (_groundChangers.Count == _coloredBlockCount)
        {
            OnHatchOpened?.Invoke();
            return;
        }

    }
}
