using EnemySpawning;
using Misc.Root;
using TMPro;
using UnityEngine;

namespace Ui.Screens {
	public class UiScreenHud : UiScreen {
		[SerializeField] private TextMeshProUGUI _rounds;
		[SerializeField] private TextMeshProUGUI _waves;
		[SerializeField] private TextMeshProUGUI _enemies;

		private EnemySpawner _enemySpawner;
		
		public override void Init() {
			_enemySpawner = Core.LevelController.EnemySpawner;
			_enemySpawner.OnChange += UpdateVisuals;
		}

		private void UpdateVisuals() {
			_rounds.text = $"round:{_enemySpawner.CurrentRound}";
			_waves.text = $"wave: {_enemySpawner.CurrentWave}";
			_enemies.text = $"enemies: {_enemySpawner.CurrentWaveEnemyCount}";
		}
	}
}