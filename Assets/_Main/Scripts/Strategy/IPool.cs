using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Main.Scripts.Strategy
{
    interface IPool<T>
    {
        void Store(T item);
        T GetInstance();
        int IsAvailable();
    }
}
