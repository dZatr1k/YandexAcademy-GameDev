using UnityEngine;
using UnityEngine.Events;

public class PlayerWaiter : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Hooker hooker))
        {
            hooker.TryHook(GetComponent<Rigidbody2D>());
        }
    }
}