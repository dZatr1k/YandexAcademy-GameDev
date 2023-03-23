using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private Vector2 _nextDirection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bat bat))
        {
            bat.ChangeDirection(_nextDirection);
        }
    }
}
