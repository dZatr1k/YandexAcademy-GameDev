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
            if (Input.GetKeyDown(KeyCode.W) | Input.GetKeyDown(KeyCode.UpArrow))
                OnWASDPressed?.Invoke(Vector2.up);
            else if(Input.GetKeyDown(KeyCode.S) | Input.GetKeyDown(KeyCode.DownArrow))
                OnWASDPressed?.Invoke(Vector2.down);
            else if (Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.LeftArrow))
                OnWASDPressed?.Invoke(Vector2.left);
            else if (Input.GetKeyDown(KeyCode.D) | Input.GetKeyDown(KeyCode.RightArrow))
                OnWASDPressed?.Invoke(Vector2.right);
        }
    }

    private void Block()
    {
        _isBlocked = true;
    }
}
