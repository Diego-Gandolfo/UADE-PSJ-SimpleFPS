using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Main.Scripts.Strategy
{
    interface IPool<T>
    {
        bool IsEmpty { get; }

        T CreateInstance();
        void Store(T item);
        T GetInstance();
    }
}
