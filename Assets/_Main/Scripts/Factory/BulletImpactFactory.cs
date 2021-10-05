using SimpleFPS.Projectiles;
using UnityEngine;

namespace SimpleFPS.Factory
{
    public class BulletImpactFactory : AbstractFactory<BulletImpact>
    {
        #region Constructor

        public BulletImpactFactory(BulletImpact prefab) : base(prefab) { }

        #endregion

        #region Public Methods

        public BulletImpact GetBulletImpact(Vector3 position, Quaternion rotation)
        {
            var bulletImpact = Pool.GetInstance();
            bulletImpact.transform.position = position;
            bulletImpact.transform.rotation = rotation;
            return bulletImpact;
        }

        public void StoreBulletImpact(BulletImpact bulletImpact)
        {
            Pool.StoreInstance(bulletImpact);
        }

        #endregion
    }
}
