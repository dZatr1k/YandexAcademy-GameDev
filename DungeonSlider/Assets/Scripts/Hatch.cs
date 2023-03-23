using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Hatch : MonoBehaviour
{
    public static UnityAction OnLevelEnd;

    [SerializeField] private string _nextLevel = "Level1";
    [SerializeField] private Sprite _openSprite;
    [SerializeField] private SpriteRenderer _renderer;
    
    private bool _isOpen = false;

    private void OnEnable()
    {
        HatchOpener.OnHatchOpened += OpenHatch;
    }

    private void OnDisable()
    {
        HatchOpener.OnHatchOpened -= OpenHatch;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isOpen)
        {
            if(collision.TryGetComponent(out Player player))
            {
                StartCoroutine(ChangeScene());
            }
        }
    }

    private void OpenHatch()
    {
        _renderer.sprite = _openSprite;
        _isOpen = true;
    }

    private IEnumerator ChangeScene()
    {
        OnLevelEnd?.Invoke();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(_nextLevel);
    }
}
