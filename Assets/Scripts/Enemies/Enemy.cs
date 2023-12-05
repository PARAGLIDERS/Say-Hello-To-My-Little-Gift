using Player;
using PoolSystem;
using Root;
using Units;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies {
	[RequireComponent(typeof(NavMeshAgent))]
	public abstract class Enemy : PoolObject {
		[SerializeField] private UnitAnimation _animation;
		[SerializeField] protected NavMeshAgent _agent;
        		
		protected virtual void Update() {
            Vector3 playerPosition = Core.LevelController.PlayerController.Position;
			_agent?.SetDestination(playerPosition);
			_animation?.Trigger();
		}	
	}
}