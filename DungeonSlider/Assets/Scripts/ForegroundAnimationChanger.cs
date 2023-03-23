using UnityEngine;

public class ForegroundAnimationChanger : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        Hatch.OnLevelEnd += SetHide;
        Player.OnDied += SetHide;
    }

    private void OnDisable()
    {
        Hatch.OnLevelEnd -= SetHide;
        Player.OnDied -= SetHide;
    }

    private void SetHide()
    {
        _animator.SetTrigger("Hide");
    }
}
