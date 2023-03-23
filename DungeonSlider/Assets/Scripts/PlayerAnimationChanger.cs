using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationChanger : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        PlayerMover.OnStartMoving += SetFall;
        PlayerMover.OnEndMoving += SetGrounded;
        Player.OnDied += SetDie;
    }

    private void OnDisable()
    {
        PlayerMover.OnStartMoving -= SetFall;
        PlayerMover.OnEndMoving -= SetGrounded;
        Player.OnDied -= SetDie;
    }

    private void SetFall(Vector2 direction)
    {
        _animator.SetTrigger("Fall");
    }

    private void SetGrounded()
    {
        _animator.SetTrigger("Grounded");
    }

    private void SetDie()
    {
        _animator.SetTrigger("Die");
    }
}
