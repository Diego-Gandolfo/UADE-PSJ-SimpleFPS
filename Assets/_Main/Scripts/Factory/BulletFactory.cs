using SimpleFPS.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFPS.Factory
{
    public class BulletFactory : AbstractFactory<Bullet>
    {
        public BulletFactory(Bullet prefab) : base(prefab)
        {

        }
    }
}
