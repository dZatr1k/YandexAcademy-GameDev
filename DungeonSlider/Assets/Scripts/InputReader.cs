using UnityEngine;
using UnityEngine.Events;

public class InputReader : MonoBehaviour
{
    public static UnityAction<Vector2> OnWASDPressed;

    private bool _isBlocked = false;

    private void OnEnable()
    {
        Player.OnDied += Block;
    }

    private void OnDisable()
    {
        Player.OnDied -= Block;
    }

    private void Update()
    {
        if(_isBlocked == false)
        {
            if (Input.GetKeyDown(KeyCode.W))
                OnWASDPressed?.Invoke(Vector2.up);
            else if(Input.GetKeyDown(KeyCode.S))
                OnWASDPressed?.Invoke(Vector2.down);
            else if (Input.GetKeyDown(KeyCode.A))
                OnWASDPressed?.Invoke(Vector2.left);
            else if (Input.GetKeyDown(KeyCode.D))
                OnWASDPressed?.Invoke(Vector2.right);
        }
    }

    private void Block()
    {
        _isBlocked = true;
    }
}
