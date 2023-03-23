using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    public static UnityAction<Vector2> OnStartMoving;
    public static UnityAction OnEndMoving;

    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private Vector2 _movingDirection;
    private bool _isGrounded = true;
    private Coroutine _wait;
    private Vector2 _waitDirection;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        InputReader.OnWASDPressed += TryMove;
        Player.OnDied += ResetMove;
    }

    private void OnDisable()
    {
        InputReader.OnWASDPressed -= TryMove;
        Player.OnDied -= ResetMove;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_isGrounded == false)
        {
            if (IsTargerPoint(collision))
            {
                _isGrounded = true;
                OnEndMoving?.Invoke();
                if (_wait != null)
                    Move(_waitDirection);
            }
        }
    }

    private void TryMove(Vector2 direction)
    {
        if (_isGrounded) 
        {
            Move(direction);
        }
        else
        {
            if (_wait != null)
                StopCoroutine(_wait);
            _wait = StartCoroutine(WaitForMove(direction));
        }
    }

    private void Move(Vector2 direction)
    {
        _rigidbody.velocity = direction * _speed;
        OnStartMoving?.Invoke(direction);
        _movingDirection = direction;
        _isGrounded = false;
    }

    private IEnumerator WaitForMove(Vector2 direction)
    {
        _waitDirection = direction;
        yield return new WaitForSeconds(0.6f);

        _wait = null;
    }

    private bool IsTargerPoint(Collision2D collision)
    {
        List<ContactPoint2D> points = new List<ContactPoint2D>();
        int count = collision.otherCollider.GetContacts(points);
        
        foreach (var point in points)
        {
            if (Vector2.SignedAngle(point.normal, -_movingDirection) == 0)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetMove()
    {
        _rigidbody.velocity = Vector2.zero;
    }
}
