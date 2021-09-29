using SimpleFPS.Generics.Pool;
using UnityEngine;

namespace SimpleFPS.Factory
{
    public abstract class AbstractFactory<T> where T : MonoBehaviour
    {
        protected T _prefab;
        protected Pool<T> _pool;

        public Pool<T> Pool => _pool;

        public AbstractFactory(T prefab)
        {
            _prefab = prefab;
            InitializePool();
        }

        private void InitializePool()
        {
            _pool = new Pool<T>(_prefab);
        }
    }
}
