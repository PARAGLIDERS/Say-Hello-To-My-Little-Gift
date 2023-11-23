using EnemySpawning;

namespace Misc.LevelControl {
	public class LevelController {
		public EnemySpawner EnemySpawner { get; }
		
		public LevelController() {
			EnemySpawner = new EnemySpawner(OnAllEnemiesKilled);
		}
		
		public void StartLevel() {
			EnemySpawner.StartSpawning();
		}

		public void PlayerDied() {
			EnemySpawner.StopSpawning();
		}
		
		public void OnAllEnemiesKilled() {
			
		}

		public void OnQuitLevel() {
			EnemySpawner?.StopSpawning();
			EnemySpawner?.Dispose();
		}

		public void Dispose() {
			OnQuitLevel();
		}
	}
}