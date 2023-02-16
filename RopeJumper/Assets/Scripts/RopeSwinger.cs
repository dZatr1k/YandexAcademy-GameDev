using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RopeSwinger : MonoBehaviour
{
    [SerializeField] private float _startSpeed;

    private Rigidbody2D _playerBody;
    private Coroutine _swing;

    private void OnEnable()
    {
        InputReader.OnLeftMousePressed += TryEndSwing;
    }

    private void OnDisable()
    {
        InputReader.OnLeftMousePressed -= TryEndSwing;
    }

    private void Start()
    {
        _playerBody = GetComponent<Rigidbody2D>();
        TryStartSwing();
    }

    private IEnumerator Swing() 
    {
        while (true) 
        {
            int roundingFactor = 1;
            float roundedYVelocity = Mathf.RoundToInt(_playerBody.velocity.y * roundingFactor) / roundingFactor;
            roundingFactor = 100;
            float roundedXVelocity = Mathf.RoundToInt(_playerBody.velocity.x * roundingFactor) / roundingFactor;
            if (roundedYVelocity == 0 && roundedXVelocity != 0)
                _playerBody.velocity = _playerBody.velocity.normalized * _startSpeed;

            yield return new WaitForFixedUpdate();
        }
    }

    private void TryStartSwing()
    {
        _playerBody.velocity = Vector2.right * _startSpeed;
        if (_swing == null)
            _swing = StartCoroutine(Swing());
    }

    private void TryEndSwing() 
    {
        if(_swing != null) 
        {
            StopCoroutine(_swing);
            _swing = null;
        }
    }
}