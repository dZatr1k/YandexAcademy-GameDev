using UnityEngine;

public class Player : MonoBehaviour
{
    private HingeJoint2D _lastHinge;
    private bool _isHooked = false;

    public bool IsHooked
    {
        get { return _isHooked; }
        set { _isHooked = value; }
    }

    public HingeJoint2D LastHinge 
    {
        get { return _lastHinge; } 
        set { _lastHinge = value; }
    }
}
