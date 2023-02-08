using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RocketBooster))]
public class InputReader : MonoBehaviour
{
    private RocketBooster _booster;

    private void Start()
    {
        _booster = GetComponent<RocketBooster>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
            _booster.IncreaseSpeed();

        if (Input.GetMouseButtonUp(0))
            _booster.DecreaseSpeed();
    }
}
