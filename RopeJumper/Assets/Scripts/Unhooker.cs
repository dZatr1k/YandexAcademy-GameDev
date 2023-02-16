using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class Unhooker : MonoBehaviour
{
    private HingeJoint2D _hingeJoint;

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
    }

    private void TryUnhook() 
    {
        if(_hingeJoint.enabled)
        {
            _hingeJoint.connectedBody = null;
            _hingeJoint.enabled = false;
        }
    }
}
