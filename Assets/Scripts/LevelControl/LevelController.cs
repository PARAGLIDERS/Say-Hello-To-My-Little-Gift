

using EnemySpawning;
using Misc.Root;
using UnityEngine;

namespace Misc.LevelControl {
	public class LevelController {
		public EnemySpawner EnemySpawner { get; }
		
		public LevelController() {
			EnemySpawner = Object.Instantiate(Core.Resources.EnemySpawner);
			EnemySpawner.Init(OnAllEnemiesKilled, OnFinishSpawning);
		}
		
		public void StartLevel() {
			EnemySpawner.StartSpawning();
		}

		public void PlayerDied() {
			EnemySpawner.StopSpawning();
		}

		public void OnFinishSpawning() {
			
		}
		
		public void OnAllEnemiesKilled() {
			
		}
	}
}