using UnityEngine;

public class CubePainter : MonoBehaviour
{
    [SerializeField] private Cube cube;

    private void OnEnable()
    {
        cube.Collided += Paint;
    }

    private void OnDisable()
    {
        cube.Collided -= Paint;
    }

    private void Paint(Renderer renderer, Color color)
    {
        renderer.material.color = color;
    }
}
