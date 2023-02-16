using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RopeSwinger : MonoBehaviour
{
    [SerializeField] private float _startSpeed;

    private Rigidbody2D _playerBody;
    private Coroutine _swing;
    private const int _roundingHundredthsFactor = 100;

    private void OnEnable()
    {
        Hooker.Hooked += TryStartSwing;
        InputReader.OnLeftMousePressed += TryEndSwing;
    }

    private void OnDisable()
    {
        Hooker.Hooked += TryStartSwing;
        InputReader.OnLeftMousePressed -= TryEndSwing;
    }

    private void Start()
    {
        _playerBody = GetComponent<Rigidbody2D>();
    }

    private IEnumerator Swing() 
    {
        while (true) 
        {
            float roundedYVelocity = Mathf.RoundToInt(_playerBody.velocity.y);
            float roundedXVelocity = Mathf.RoundToInt(_playerBody.velocity.x * _roundingHundredthsFactor) / _roundingHundredthsFactor;

            if (roundedYVelocity == 0 && roundedXVelocity != 0)
                _playerBody.velocity = _playerBody.velocity.normalized * _startSpeed;

            yield return new WaitForFixedUpdate();
        }
    }

    public void TryStartSwing()
    {
        if(_playerBody.velocity.x == 0)
            _playerBody.velocity = Vector2.right     * _startSpeed;
        else
            _playerBody.velocity = _playerBody.velocity.normalized * _startSpeed;

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
