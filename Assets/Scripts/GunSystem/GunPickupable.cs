using PoolSystem;
using Root;
using UnityEngine;

namespace GunSystem {
    public class GunPickupable : MonoBehaviour {
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private ParticleSystem[] _particles;

        public bool IsActive { get; private set; }

        private GunsSpawnerConfigItem _param;
        private const float _lifetime = 25f;
        private float _currentLifetime;

        public void Activate(GunsSpawnerConfigItem param, Vector3 position) {
            _param = param;
            _currentLifetime = Time.time + _lifetime;

            _meshFilter.mesh = _param.Mesh;

            foreach (ParticleSystem particle in _particles) {
                //ParticleSystem.MainModule particlesMain = particle.main;
               // particlesMain.startColor = param.Color;
            }

            transform.position = position;
            gameObject.SetActive(true);

            Core.PoolController.Spawn(PoolType.GunSpawnVfx, transform.position, Quaternion.identity);

            IsActive = true;
        }

        // configured collision mask so it only triggers by the player 
        private void OnTriggerEnter(Collider other) {
            Core.LevelController.GunsController.Pickup(_param.Type, _param.PickupAmmo);

            Core.SfxController.Play(SfxSystem.SfxType.VfxGunPickup);
            Core.PoolController.Spawn(PoolType.GunPickupVfx, transform.position, Quaternion.identity);
            
            Deactivate();
        }

        private void Update() {
            if(Time.time >= _currentLifetime) {
                Deactivate();
            }
        }

        private void Deactivate() {
            gameObject.SetActive(false);
            IsActive = false;
        }
    }
}
