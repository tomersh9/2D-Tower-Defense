using UnityEngine;

namespace Managers {
    public class AudioManager : MonoBehaviour
    {
        [Header("Hit Sounds")]
        [SerializeField] private AudioClip enemyHitSound;
        [SerializeField] private AudioClip enemyDeathSound;
        [SerializeField] private AudioClip playerHitSound;
        [SerializeField] private AudioClip playerDeathSound;

        [Header("Shop Sounds")]
        [SerializeField] private AudioClip towerPlacedSound;
        [SerializeField] private AudioClip towerSoldSound;
        [SerializeField] private AudioClip towerSelectedSound;
        [SerializeField] private AudioClip towerHoverSound;
        
   
        private AudioSource _audioSource;

        #region Singleton
        private static AudioManager _instace;
        public static AudioManager GetInstance() => _instace;
        private void Awake() {
            if(FindObjectsOfType<AudioManager>().Length > 1) {
                Destroy(gameObject);
            } else {
                _instace = this;
                DontDestroyOnLoad(gameObject);
            }
            _audioSource = GetComponent<AudioSource>();
        }
        #endregion
    
        private void Start() {
            _audioSource.mute = !Preferences.GetToggleSfx();
        }

        public void PlayEnemyHitSfx() => _audioSource.PlayOneShot(enemyHitSound,0.12f);

        public void PlayEnemyDeathSfx() => _audioSource.PlayOneShot(enemyDeathSound,0.15f);

        public void PlayPlayerHitSfx() => _audioSource.PlayOneShot(playerHitSound,0.8f);

        public void PlayPlayerDeathSfx() => _audioSource.PlayOneShot(playerDeathSound,1f);

        public void PlayTowerDownSfx() => _audioSource.PlayOneShot(towerPlacedSound,0.5f);

        public void PlayTowerSoldSfx() => _audioSource.PlayOneShot(towerSoldSound,0.5f);

        public void PlayTowerSelectedSfx() => _audioSource.PlayOneShot(towerSelectedSound, 0.5f);

        public void PlayTowerHoverSfx() => _audioSource.PlayOneShot(towerHoverSound,0.2f);
    }
}
