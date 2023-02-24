using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player), typeof(HingeJoint2D))]
public class Hooker : MonoBehaviour
{
    public static UnityAction Hooked;

    private Player _player;
    private HingeJoint2D _hinge;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out LastRopeSegment lastRopeSegment)) 
        {
            collision.TryGetComponent(out Rigidbody2D ropeBody);
            TryHook(ropeBody);
        }
    }

    private void Start()
    {
        _player = GetComponent<Player>();
        _hinge = GetComponent<HingeJoint2D>();
    }

    public void TryHook(Rigidbody2D ropeBody)
    {
        if (_player.IsHooked)
            return;

        ropeBody.GetComponent<BoxCollider2D>().enabled = false;
        if (_player.LastHinge != null)
            _player.LastHinge.GetComponent<BoxCollider2D>().enabled = true;
        _player.IsHooked = true;
        _hinge.enabled = true;
        _hinge.connectedBody = ropeBody;
        Hooked.Invoke();
    }
}
