using UnityEngine;

[RequireComponent(typeof(HingeJoint2D), typeof(Player))]
public class Unhooker : MonoBehaviour
{
    private HingeJoint2D _hingeJoint;
    private Player _player;

    private void OnEnable()
    {
        InputReader.OnLeftMousePressed += TryUnhook;
    }

    private void OnDisable()
    {
        InputReader.OnLeftMousePressed -= TryUnhook;
    }

    private void Start()
    {
        _hingeJoint = GetComponent<HingeJoint2D>();
        _player = GetComponent<Player>();
    }

    private void TryUnhook() 
    {
        if(_hingeJoint.enabled)
        {
            _player.IsHooked = false;
            _player.LastHinge = _hingeJoint.connectedBody.GetComponent<HingeJoint2D>();
            _hingeJoint.connectedBody = null;
            _hingeJoint.enabled = false;
        }
    }
}
