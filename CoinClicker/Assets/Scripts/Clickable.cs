using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Clickable : MonoBehaviour
{

    [SerializeField] private AnimationCurve _scaleCurve;
    [SerializeField] private float _scaleTime = 0.25f;
    [SerializeField] private HitEffect _hitEffectPrefab;
    [SerializeField] private Resources _resources;
    [SerializeField] private GameObject _smallCube;
    [SerializeField] private Transform _spawnPoint;

    private int _coinsPerClick = 1;

    public void Hit()
    {
        HitEffect hitEffect = Instantiate(_hitEffectPrefab, transform.position, Quaternion.identity);
        hitEffect.Init(_coinsPerClick);
        Instantiate(_smallCube, _spawnPoint.position, Quaternion.identity);
        StartCoroutine(HitAnimation());
    }

    public void HitSmallCube()
    {
        _resources = FindObjectOfType<Resources>();
        _resources.CollectCoins(1, transform.position);
        Destroy(gameObject);
    }

    private IEnumerator HitAnimation()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime / _scaleTime)
        {
            float scale = _scaleCurve.Evaluate(t);
            transform.localScale = Vector3.one * scale;
            yield return null;
        }
        transform.localScale = Vector3.one;
    }

    public void AddCoinsPerClick(int value)
    {
        _coinsPerClick += value;
    }
}
