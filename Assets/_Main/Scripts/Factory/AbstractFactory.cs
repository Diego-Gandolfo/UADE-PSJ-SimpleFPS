using SimpleFPS.Generics.Pool;
using UnityEngine;

namespace SimpleFPS.Factory
{
    public abstract class AbstractFactory<T> where T : MonoBehaviour
    {
        #region Protected Fields

        protected T _prefab;
        protected Pool<T> _pool;

        #endregion

        #region Propertys

        public Pool<T> Pool => _pool;

        #endregion

        #region Constructor

        public AbstractFactory(T prefab)
        {
            _prefab = prefab;
            InitializePool();
        }

        #endregion

        #region Private Methods

        private void InitializePool()
        {
            _pool = new Pool<T>(_prefab);
        }

        #endregion
    }
}
