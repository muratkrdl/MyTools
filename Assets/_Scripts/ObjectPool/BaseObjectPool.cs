using _Scripts.Singleton;
using UnityEngine;
using UnityEngine.Pool;

namespace _Scripts.ObjectPool
{
    public abstract class BaseObjectPool<T> : MonoSingleton<BaseObjectPool<T>> where T : MonoBehaviour, IPoolObject<T>
    {
        [SerializeField] protected T prefab;

        [SerializeField] private int defaultPoolSize;
        [SerializeField] private int maxPoolSize;

        private ObjectPool<T> _pool;

        protected override void Awake()
        {
            _pool = new ObjectPool<T>
                (
                    ObCreate,
                    OnGet,
                    OnRelease,
                    OnDestroy,
                    true,
                    defaultPoolSize,
                    maxPoolSize
                );
        }

        protected virtual T ObCreate()
        {
            var obj = Instantiate(prefab, transform);
            obj.SetPool(_pool);
            return obj;
        }

        protected virtual void OnGet(T obj)
        {
            obj.gameObject.SetActive(true);
        }

        protected virtual void OnRelease(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        private void OnDestroy(T obj)
        {
            Destroy(obj);
        }
        
    }
}