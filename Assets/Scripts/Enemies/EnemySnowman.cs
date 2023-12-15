using Root;
using UnityEngine;
using Units;

namespace Enemies {
    public class EnemySnowman : Enemy {
        [SerializeField] private Transform _muzzle;

        protected override void Attack() {
            Core.PoolController.Spawn(PoolSystem.PoolType.EnemyProjectile_Snowball, _muzzle.position, _muzzle.rotation);
            Core.SfxController.Play(SfxSystem.SfxType.EnemySnowmanThrow, _muzzle.position);
        }
    }
}
