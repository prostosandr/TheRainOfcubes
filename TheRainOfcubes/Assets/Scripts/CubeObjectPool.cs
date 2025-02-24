using UnityEngine;
using UnityEngine.Pool;

public class CubeObjectPool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    public Cube GetCube()
    {
        return _pool.Get();
    }

    private void ActionOnGet(Cube cube)
    {
        cube.Deactivated += ReleaseCube;
    }

    private void ReleaseCube(Cube cube)
    {
        cube.Deactivated -= ReleaseCube;
        _pool.Release(cube);
    }
}