using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            player.Kill();
        }
    }

    public void ChangeDirection(Vector2 direction)
    {
        _rigidbody.velocity = direction * _speed;
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}
