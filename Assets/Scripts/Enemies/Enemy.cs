using Player;
using PoolSystem;
using Units;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies {
	[RequireComponent(typeof(NavMeshAgent))]
	public abstract class Enemy : PoolObject {
		[SerializeField] private UnitAnimation _animation;
		[SerializeField] private NavMeshAgent _agent;
        		
		protected virtual void Update() {
			_agent?.SetDestination(PlayerController.POSITION);
			_animation?.Trigger();
		}	
	}
}