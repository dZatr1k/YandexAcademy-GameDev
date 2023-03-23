using System.Collections;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private bool _isShowed = false;
    private Coroutine _show;
    private Player _target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            _target = player;
            if (_isShowed)
            {
                _target.Kill();
            }
            else
            {
                if(_show == null)
                    _show = StartCoroutine(Show());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _target = null;
        }
    }

    private IEnumerator Show()
    {
        yield return new WaitForSeconds(0.5f);
        _animator.SetTrigger("Show");
        _isShowed = true;
        if (_target != null)
            _target.Kill();
        StartCoroutine(Hide());
    }

    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(2);
        _animator.SetTrigger("Hide");
        _isShowed = false;
        _show = null;
    }
}
