using UnityEngine;

public class PlayerFlipper : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        PlayerMover.OnStartMoving += Flip;
    }

    private void OnDisable()
    {
        PlayerMover.OnStartMoving -= Flip;
    }

    private void Flip(Vector2 direction)
    {
        int angle = 0;
        if (direction.x == 1)
            angle = 90;
        else if (direction.x == -1)
            angle = -90;
        else if (direction.y == 1)
            angle = 180;
        transform.rotation = Quaternion.identity;
        transform.Rotate(new Vector3(0, 0, angle));
    }
}
