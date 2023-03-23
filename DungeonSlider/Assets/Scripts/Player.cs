using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static UnityAction OnDied;

    public void Kill()
    {
        OnDied?.Invoke();
    }
}
