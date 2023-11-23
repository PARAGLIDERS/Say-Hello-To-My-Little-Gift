using System;
using DamageSystem;
using Player;
using PoolSystem;
using Units;
using Units.UnitConfigs;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies {
	[RequireComponent(typeof(NavMeshAgent))]
	public abstract class Enemy : MonoBehaviour {
		[SerializeField] private Transform _bodyTransform;
		[SerializeField] private UnitAnimationConfig _animationConfig;
		[SerializeField] private UnitMotionConfig _motionConfig;

		private UnitAnimation _animation;
		public event Action OnKill;
		private NavMeshAgent _agent;

		private void Awake() {
			_animation = new UnitAnimation(_animationConfig, _bodyTransform);
			_agent = GetComponent<NavMeshAgent>();

			if(_agent == null) return;
			_agent.speed = _motionConfig.Speed;
			_agent.acceleration = 1f / (_motionConfig.Drag > 0f ? _motionConfig.Drag : 0.001f);
		}
		
		private void Update() {
			_agent?.SetDestination(PlayerController.POSITION);
			_animation?.Trigger();
			_animation?.Update();
		}

		public void Activate(Vector3 position) {
			transform.position = position;
			gameObject.SetActive(true);
		}
		
		public void Deactivate(){
			gameObject.SetActive(false);
			OnKill?.Invoke();
			OnKill = null;
		}
	}
}