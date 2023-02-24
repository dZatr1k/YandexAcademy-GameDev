using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private Slide _slide;

    private void Update()
    {
        if (Input.GetKeyDown("space"))
            _slide.TryJump();
    }
}
