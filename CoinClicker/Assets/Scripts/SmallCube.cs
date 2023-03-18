using UnityEngine;

public class SmallCube : MonoBehaviour
{
    [SerializeField] private float _spawnForce = 5f;

    private Rigidbody _body;
    private Renderer _renderer;

    private void OnEnable()
    {
        _renderer = GetComponent<Renderer>();
        _body = GetComponent<Rigidbody>();
        _body.AddForce(Vector2.up * _spawnForce, ForceMode.Impulse);
    }

    public void ChangeMaterial(Material material) 
    {
        _renderer.material = material;
    }
}
