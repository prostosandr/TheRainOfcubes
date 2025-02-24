using UnityEngine;
using UnityEngine.Pool;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private int _numberOfCubes;

    public int NumberOfCubes => _numberOfCubes;
    public int PoolCapacity => _poolCapacity;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => ActingOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    public Cube GetCube()
    {
        _numberOfCubes++;

        return _pool.Get();
    }

    private void ActingOnGet(Cube cube)
    {
        cube.Deactivated += ReleaseCube;
    }

    private void ReleaseCube(Cube cube)
    {
        _numberOfCubes--;

        cube.Deactivated -= ReleaseCube;
        _pool.Release(cube);
    }
}
