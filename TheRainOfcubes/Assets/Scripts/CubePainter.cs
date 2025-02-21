using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubePainter : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void ChangeColor()
    {
        _renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }

    public void ResetColor()
    {
        _renderer.material.color = Color.white;
    }
}