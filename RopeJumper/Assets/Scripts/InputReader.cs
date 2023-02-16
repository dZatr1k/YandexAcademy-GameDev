using UnityEngine;
using UnityEngine.Events;

public class InputReader : MonoBehaviour
{
    public static UnityAction OnLeftMousePressed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnLeftMousePressed.Invoke();
    }
}
