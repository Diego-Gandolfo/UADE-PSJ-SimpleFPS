using SimpleFPS.Components;
using UnityEngine;

namespace SimpleFPS.Factory
{
    public class ExplosionFactory : AbstractFactory<Explosion>
    {
        #region Constructor

        public ExplosionFactory(Explosion prefab) : base(prefab) { }

        #endregion

        #region Public Methods

        public Explosion GetExplosion(Vector3 position, Quaternion rotation)
        {
            var explosion = Pool.GetInstance();
            explosion.transform.position = position;
            explosion.transform.rotation = rotation;
            return explosion;
        }

        public void StoreExplosion(Explosion explosion)
        {
            Pool.StoreInstance(explosion);
        }

        #endregion
    }
}
