using UnityEngine.Pool;

namespace _Scripts.ObjectPool
{
    public interface IPoolObject<T> where T : class
    {
        void SetPool(ObjectPool<T> pool);
        void ReleasePool();
    }
}