using Assets._Main.Scripts.Strategy;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Generics
{
    public class Pool<T> : IPool<T> where T : MonoBehaviour
    {
        private List<T> inUse = new List<T>();
        private List<T> available = new List<T>();


        public Pool(List<T> values)
        {
            foreach (var item in values)
            {
                Store(item);
            }
        }

        public T GetInstance()
        {
            if(IsAvailable() > 0)
            {
                T temp = available[0];
                available.Remove(temp);
                inUse.Add(temp);
                temp.gameObject.SetActive(true);
                return temp;
            }
            return default(T);
        }

        public int IsAvailable()
        {
            return available.Count;
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
