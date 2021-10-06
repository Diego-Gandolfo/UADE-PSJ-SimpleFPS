using SimpleFPS.Projectiles;
using UnityEngine;
using SimpleFPS.Components;
using SimpleFPS.FPS;
using SimpleFPS.Factory;
using SimpleFPS.Enemy.Boss;
using SimpleFPS.Life;
using UnityEngine.UI;

namespace SimpleFPS.Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Static

        public static LevelManager Instance { get; private set; }

        #endregion

        #region Serialize Fields

        [Header("Character")]
        [SerializeField] private FPSCharacterController _character;

        [Header("Boss")]
        [SerializeField] private Boss _boss;

        [Header("Factory Prefabs")]
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private BulletImpact _bulletImpactPrefab;
        [SerializeField] private Explosion _explotionPrefab;

        [Header("Canvas InGame")]
        [SerializeField] private Animator _canvasAnimator;
        [SerializeField] private Text _counterText;

        #endregion

        #region Private Fields

        private BulletFactory _bulletFactory;
        private BulletImpactFactory _bulletImpactFactory;
        private ExplosionFactory _explosionFactory;

        private int _batteryDeadCounter;

        #endregion

        #region Propertys

        // Character
        public FPSCharacterController Character => _character;

        // Factorys
        public BulletFactory BulletFactory => _bulletFactory;
        public BulletImpactFactory BulletImpactFactory => _bulletImpactFactory;
        public ExplosionFactory ExplosionFactory => _explosionFactory;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            _bulletFactory = new BulletFactory(_bulletPrefab);
            _bulletImpactFactory = new BulletImpactFactory(_bulletImpactPrefab);
            _explosionFactory = new ExplosionFactory(_explotionPrefab);
        }

        private void Start()
        {
            GameManager.Instance.SetCharacter(_character);
            var bossHealth = _boss.gameObject.GetComponent<Health>();
            bossHealth.OnDie += OnDieBossHandler;
        }

        #endregion

        #region Private Methods

        private void OnDieBossHandler()
        {
            GameManager.Instance.Invoke("Victory", 1.5f);
        }

        private void TriggerAnimatorFadeIn()
        {
            _counterText.text = _batteryDeadCounter.ToString();
            _canvasAnimator.SetTrigger("DoFadeIn");
        }

        private void TriggerAnimatorFinalBoss()
        {
            _canvasAnimator.SetTrigger("DoFinalBoss");
        }

        #endregion

        #region Public Methods

        public void IncreaseBatteryDeadCounter()
        {
            _canvasAnimator.SetTrigger("DoFadeOut");
            _batteryDeadCounter++;
            Invoke("TriggerAnimatorFadeIn", 0.5f);

            if (_batteryDeadCounter >= 5)
            {
                _boss.gameObject.SetActive(true);
                Invoke("TriggerAnimatorFinalBoss", 1f);
            }
        }

        #endregion
    }
}
