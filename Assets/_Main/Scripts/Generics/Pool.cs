using SimpleFPS.Strategy.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFPS.Generics.Pool
{
    public class Pool<T> : IPool<T> where T : MonoBehaviour
    {
        #region Private Fields

        // Prefab
        private T _prefab;

        // Lists
        private List<T> _inUse = new List<T>();
        private List<T> _available = new List<T>();

        #endregion

        #region Propertys

        public bool IsEmpty => (_available.Count <= 0);

        #endregion

        #region Constructor

        public Pool(T prefab)
        {
            _prefab = prefab;
        }

        #endregion

        #region Public Methods

        public T CreateInstance()
        {
            var instance = GameObject.Instantiate(_prefab);
            _inUse.Add(instance);
            return instance;
        }

        public T GetInstance()
        {
            if(!IsEmpty)
            {
                T instance = _available[0];
                _available.Remove(instance);
                _inUse.Add(instance);
                instance.gameObject.SetActive(true);
                return instance;
            }
            else
            {
                return CreateInstance();
            }
        }

        public void StoreInstance(T instance)
        {
            _available.Add(instance);
            instance.gameObject.SetActive(false);
            if (_inUse.Contains(instance))
                _inUse.Remove(instance);
        }

        #endregion
    }
}
