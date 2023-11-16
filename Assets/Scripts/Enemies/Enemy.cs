using System;
using DamageSystem;
using Player;
using PoolSystem;
using Units;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies {
	public class Enemy : PoolObject {
		[SerializeField] private NavMeshAgent _agent;
		[SerializeField] private Transform _bodyTransform;
		[SerializeField] private MeshFilter _modelMeshFilter;
		[SerializeField] private MeshRenderer _modelMeshRenderer;

		private UnitAnimation _animation;
		private Action _onKill;
		
		public void Init(EnemyConfig config, Action onKill) {
			_onKill = onKill;
			
			_animation = new UnitAnimation(config.AnimationConfig, _bodyTransform);
			_agent.speed = config.MotionConfig.Speed;
			_agent.acceleration = 1f / (config.MotionConfig.Drag > 0f ? config.MotionConfig.Drag : 0.001f);
			_modelMeshFilter.mesh = config.Model;
			_modelMeshRenderer.transform.localPosition = config.Offset;
		}
		
		private void Update() {
			_agent.SetDestination(PlayerController.POSITION);
			_animation.Trigger();
			_animation.Update();
		}

		public override void Deactivate(){
			_onKill?.Invoke();
			_onKill = null;
		}
	}
}