using Assets._Main.Scripts.Strategy;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Generics
{
    public class Pool<T> : IPool<T> where T : MonoBehaviour
    {
        private List<T> inUse = new List<T>();
        private List<T> available = new List<T>();

        public bool IsEmpty => (available.Count <= 0);

        public Pool() { }

        public Pool(List<T> values)
        {
            foreach (var item in values)
            {
                Store(item);
            }
        }

        public T CreateInstance()
        {
            // TODO: Agregar creacion de Instance en Pool
            return default(T);
        }

        public T GetInstance()
        {
            if(!IsEmpty)
            {
                T temp = available[0];
                available.Remove(temp);
                inUse.Add(temp);
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
            available.Add(item);
            item.gameObject.SetActive(false);
            if (inUse.Contains(item)) //Si esta en la lista...
                inUse.Remove(item); //Removelo
        }
        public List<T> GetInUseItems()
        {
            List<T> list = new List<T>();
            list.AddRange(inUse);
            return list;
        }
    }
}
