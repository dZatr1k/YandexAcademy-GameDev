using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Hooker : MonoBehaviour
{
    public static UnityAction Hooked;

    [SerializeField] private Rigidbody2D _ropeBody;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            TryHook(player);
        }
    }

    private void Start()
    {
        _ropeBody = GetComponent<Rigidbody2D>();
    }

    private void TryHook(Player player)
    {
        if (player.IsHooked)
            return;

        player.TryGetComponent(out HingeJoint2D joint);

        if (joint == null)
            return;

        GetComponent<BoxCollider2D>().enabled = false;
        if (player.LastHinge != null)
            player.LastHinge.GetComponent<BoxCollider2D>().enabled = true;
        player.IsHooked = true;
        joint.enabled = true;
        joint.connectedBody = _ropeBody;
        Hooked.Invoke();
    }
}
