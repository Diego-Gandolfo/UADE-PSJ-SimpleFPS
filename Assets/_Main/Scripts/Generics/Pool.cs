using Assets._Main.Scripts.Strategy;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Generics
{
    public class Pool<T> : IPool<T> where T : MonoBehaviour
    {
        private T _prefab;

        private List<T> _inUse = new List<T>();
        private List<T> _available = new List<T>();

        public bool IsEmpty => (_available.Count <= 0);

        public Pool() { }

        public Pool(T prefab)
        {
            _prefab = prefab;
        }

        public Pool(List<T> values)
        {
            foreach (var item in values)
            {
                Store(item);
            }
        }

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
                T temp = _available[0];
                _available.Remove(temp);
                _inUse.Add(temp);
                temp.gameObject.SetActive(true);
                return temp;
            }
            else
            {
                return CreateInstance();
            }
        }

        public void Store(T item)
        {
            _available.Add(item);
            item.gameObject.SetActive(false);
            if (_inUse.Contains(item))
                _inUse.Remove(item);
        }
        public List<T> GetInUseItems()
        {
            List<T> list = new List<T>();
            list.AddRange(_inUse);
            return list;
        }
    }
}
