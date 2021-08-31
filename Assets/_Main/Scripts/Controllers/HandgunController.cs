using Assets._Main.Scripts.Strategy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Controllers
{
    public class HandgunController : BaseGunController
    {
        public override void Attack()
        {
            base.Attack();
            //TODO: Handgun Attack
        }

        public override void Reload()
        {
            base.Reload();
            //TODO: Handgun Reload
        }
    }
}
