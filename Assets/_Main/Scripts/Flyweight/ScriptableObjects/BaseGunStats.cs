using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets._Main.Scripts.Flyweight.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BaseGunStats", menuName = "Flyweight/Stats/BaseGun", order = 1)]
    public class BaseGunStats : ScriptableObject
    {
        #region Serialize Fields

        [SerializeField] private int _maxExtraAmmo;
        [SerializeField] private int _maxMagazineAmmo;

        #endregion

        #region Propertys

        public int MaxExtraAmmo => _maxExtraAmmo;
        public int MaxMagazineAmmo => _maxMagazineAmmo;

        #endregion
    }
}
