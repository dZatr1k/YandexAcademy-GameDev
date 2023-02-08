using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RocketBooster : MonoBehaviour
{
    [SerializeField] private float _startSpeed;
    [SerializeField] private float _boostSpeed;

    private Rigidbody2D _body;

    private void Start() 
    {
        _body = GetComponent<Rigidbody2D>();
        _body.velocity = Vector2.right * _startSpeed;
    }

    public void IncreaseSpeed() 
    {
        _body.velocity *= _boostSpeed;
    }

    public void DecreaseSpeed() 
    {
        _body.velocity /= _boostSpeed;
    }
}
