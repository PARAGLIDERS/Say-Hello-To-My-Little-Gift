using DG.Tweening;
using EnemySpawning;
using Root;
using TMPro;
using UnityEngine;

namespace Ui.Components {
	public class EnemySpawnerHudPanel : MonoBehaviour {
		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private RectTransform _body;
		[SerializeField] private TextMeshProUGUI _title;
		[SerializeField] private ProgressBar _progress;

		private EnemySpawner EnemySpawner => Core.LevelController.EnemySpawner;
		private Sequence _sequence;

		public void Init() {
			_canvasGroup.alpha = 0;
			_sequence = DOTween.Sequence();

			EnemySpawner.EnemyKilled += UpdateProgress;
			EnemySpawner.RoundStarted += Show;
			EnemySpawner.RoundFinished += Hide;

			if(EnemySpawner.CurrentEnemyCount > 0) {
				_canvasGroup.alpha = 1f;
				UpdateTitle();
				InitProgress();
			}
		}

		private void OnDestroy() {
			EnemySpawner.EnemyKilled -= UpdateProgress;
			EnemySpawner.RoundStarted -= Show;
			EnemySpawner.RoundFinished -= Hide;
		}

		private void Show() {
			UpdateTitle();
			InitProgress();

			// easy code is hard, hard code is easy
			_body.localScale = Vector3.one * 10f;

			_sequence?.Kill();
			_sequence.Insert(0f, _canvasGroup.DOFade(1f, 0.3f));
			_sequence.Insert(0f, _body.DOScale(1f, 0.5f));
		}

		private void Hide() {
			_sequence?.Kill();
			_sequence.Insert(0f, _canvasGroup.DOFade(0f, 0.3f));
		}

		private void UpdateTitle() {
			if (EnemySpawner.CurrentRound < EnemySpawner.MaxRounds - 1) {
				_title.text = $"ROUND {EnemySpawner.CurrentRound + 1}";
			} else {
				_title.text = $"LAST ROUND";
			}
		}

		private void InitProgress() {
			_progress.Init(EnemySpawner.CurrentEnemyCount, EnemySpawner.MaxEnemies);
		}

		private void UpdateProgress() {
			_ = _progress.GetUpdateSequence(EnemySpawner.CurrentEnemyCount);
		}
	}
}
