using SimpleFPS.Projectiles;
using UnityEngine;

namespace SimpleFPS.Factory
{
    public class BulletFactory : AbstractFactory<Bullet>
    {
        #region Constructor

        public BulletFactory(Bullet prefab) : base (prefab) { }

        #endregion

        #region Public Methods

        public Bullet GetBullet(BulletStats stats, Vector3 position, Quaternion rotation, float damage, float speed)
        {
            var bullet = Pool.GetInstance();
            bullet.SetStats(stats);
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            bullet.SetDamage(damage);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * speed;
            return bullet;
        }

        public void StoreBullet(Bullet bullet)
        {
            Pool.StoreInstance(bullet);
        }

        #endregion
    }
}
