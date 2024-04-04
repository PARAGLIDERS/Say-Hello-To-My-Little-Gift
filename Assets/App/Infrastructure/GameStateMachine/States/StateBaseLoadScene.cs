using System.Threading.Tasks;
using Music;
using Pooling;
using Root;
using Ui;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameStateMachine.States {
	public abstract class StateBaseLoadScene : IState {
		protected abstract int _sceneIndex { get; }
		protected abstract StateType _nextState { get; }
		protected virtual bool _showLoadingScreen { get; } = true;
		protected virtual int _delay { get; } = 500;

        private readonly UiService _uiController;
		private readonly PoolService _poolController;
		private readonly MusicPlayer _musicController;
		private readonly StateController _stateController;



		public virtual void Enter() {
            if (_showLoadingScreen) {
                _uiController.Show(UiScreenType.Loading);
            }
			
            _poolController.DeactivateAll();
            _musicController.Stop();

			Time.timeScale = 0;
            _ = Load();
		}

        private async Task Load() {
            AsyncOperation loading = SceneManager.LoadSceneAsync(_sceneIndex);

            while (!loading.isDone) {
                await Task.Delay(1);
            }

            await Task.Delay(_delay);

            _stateController.SetState(_nextState);
        }

        public void Update() {

        }

        public virtual void Exit() {
			Time.timeScale = 1;
        }
    }
}