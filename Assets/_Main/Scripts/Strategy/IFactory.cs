using SimpleFPS.Generics.Pool;
using UnityEngine;

namespace SimpleFPS.Factory
{
    public interface IFactory<T> where T : MonoBehaviour
    {
        Pool<T> Pool { get; }
    }
}
