using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFPS.Sounds
{
    [CreateAssetMenu(fileName = "FXSounds", menuName = "Flyweight/Sounds/FX", order = 0)]
    public class FXSounds : ScriptableObject
    {
        #region Serialized Fields

        [Header("Sounds")]
        [SerializeField] private List<AudioClip> _explosionSounds = new List<AudioClip>();
        [SerializeField] private List<AudioClip> _impactSounds = new List<AudioClip>();
        [SerializeField] private AudioClip _shootSound;
        [SerializeField] private AudioClip _scoutMoveSound;
        [SerializeField] private AudioClip _turretRotationSound;
        [SerializeField] private AudioClip _hitSound;
        [SerializeField] private AudioClip _aimSound;
        [SerializeField] private AudioClip _reloadAmmoLeftSound;
        [SerializeField] private AudioClip _reloadOutOfAmmoSound;
        [SerializeField] private AudioClip _walkSound;
        [SerializeField] private AudioClip _runSound;
        [SerializeField] private AudioClip _sneakSound;
        [SerializeField] private AudioClip _holsterWeaponSound;
        [SerializeField] private AudioClip _takeOutGunSound;
        [SerializeField] private AudioClip _knifeCutAttackSound;
        [SerializeField] private AudioClip _knifeStabAttackSound;

        #endregion

        #region Propertys

        public List<AudioClip> ExplosionSounds => _explosionSounds;
        public List<AudioClip> ImpactSounds => _impactSounds;
        public AudioClip ShootSound => _shootSound;
        public AudioClip ScoutMoveSound => _scoutMoveSound;
        public AudioClip TurretRotationRound => _turretRotationSound;
        public AudioClip HitSound => _hitSound;
        public AudioClip AimSound => _aimSound;
        public AudioClip ReloadAmmoLeftSound => _reloadAmmoLeftSound;
        public AudioClip ReloadOutOfAmmoSound => _reloadOutOfAmmoSound;
        public AudioClip WalkSound => _walkSound;
        public AudioClip RunSound => _runSound;
        public AudioClip SneakSound => _sneakSound;
        public AudioClip HolsterWeaponSound => _holsterWeaponSound;
        public AudioClip TakeOutGunSound => _takeOutGunSound;
        public AudioClip KnifeCutAttackSound => _knifeCutAttackSound;
        public AudioClip KnifeStabAttackSound => _knifeStabAttackSound;

        #endregion
    }
}
