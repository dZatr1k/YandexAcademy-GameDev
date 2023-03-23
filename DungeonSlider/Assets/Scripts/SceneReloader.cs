using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    private void OnEnable()
    {
        Player.OnDied += ReloadScene;
    }

    private void OnDisable()
    {
        Player.OnDied -= ReloadScene;
    }

    private void ReloadScene()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        Destroy(FindObjectOfType<Player>());
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
