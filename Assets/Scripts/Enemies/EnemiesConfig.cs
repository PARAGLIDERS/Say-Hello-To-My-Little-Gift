using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies {
	[CreateAssetMenu(menuName = "Enemies Config")]
	public class EnemiesConfig : ScriptableObject {
		[SerializeField] private List<EnemyConfig> _enemyConfigs;
		public List<EnemyConfig> EnemyConfigs => _enemyConfigs;

		public Enemy Get(EnemyType type) {
			return _enemyConfigs.Find(x => x.Type == type).Enemy;
		}
	}

	[Serializable]
	public struct EnemyConfig {
		[SerializeField] private EnemyType _type;
		[SerializeField] private Enemy _enemy;

		public EnemyType Type => _type;
		public Enemy Enemy => _enemy;
	}
}