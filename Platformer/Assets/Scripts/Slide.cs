using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Slide : MonoBehaviour
{
    [SerializeField] private float _minGroundNormalY = .65f;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpVelocity;

    private Rigidbody2D _body;
    private Vector2 _groundNormal;
    private Vector2 _targetVelocity;
    private bool _grounded;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);

    private const float MinMoveDistance = 0.001f;
    private const float ShellRadius = 0.1f;
    private const float Ratio = 180 / Mathf.PI;

    private void OnEnable()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_ground);
        _contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        Vector2 alongSurface = Vector2.Perpendicular(_groundNormal);

        float angle = Vector2.SignedAngle(Vector2.right, alongSurface);
        _targetVelocity = -Mathf.Sin(angle * Ratio) * alongSurface * _speed;
    }

    private void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.fixedDeltaTime;
        
        _velocity.x = _targetVelocity.x;

        _grounded = false;

        Vector2 deltaPosition = _velocity * Time.fixedDeltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        MakeMove(move, false);

        move = Vector2.up * deltaPosition.y;

        MakeMove(move, true);
    }

    private void RedefineHitBufferList(int count) 
    {
        _hitBufferList.Clear();

        for (int i = 0; i < count; i++)
        {
            _hitBufferList.Add(_hitBuffer[i]);
        }
    }

    private float CalculateModifiedDistance(RaycastHit2D hit, bool yMovement) 
    {
        Vector2 currentNormal = hit.normal;
        if (currentNormal.y > _minGroundNormalY)
        {
            _grounded = true;

            if (yMovement)
            {
                _groundNormal = currentNormal;
                currentNormal.x = 0;
            }
        }
        float projection = Vector2.Dot(_velocity, currentNormal);
        if (projection < 0)
            _velocity = _velocity - projection * currentNormal;

        return hit.distance - ShellRadius;
    }

    private void MakeMove(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > MinMoveDistance)
        {
            int count = _body.Cast(move, _contactFilter, _hitBuffer, distance + ShellRadius);

            RedefineHitBufferList(count);
            
            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                float modifiedDistance = CalculateModifiedDistance(_hitBufferList[i], yMovement);
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _body.position = _body.position + move.normalized * distance;
    }

    public void TryJump() 
    {
        if(_grounded)
        {
            _velocity = _jumpVelocity * _groundNormal;
        }
    }
}