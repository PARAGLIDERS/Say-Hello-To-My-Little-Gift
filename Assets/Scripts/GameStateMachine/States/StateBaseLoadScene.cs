using System;
using System.Threading.Tasks;
using Root;
using Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameStateMachine.States {
	public abstract class StateBaseLoadScene : IState {
		protected abstract int _sceneIndex { get; }
		protected abstract StateType _nextState { get; }
		protected virtual bool _showLoadingScreen { get; } = true;
		protected virtual int _delay { get; } = 500;

		public virtual void Enter() {
            if (_showLoadingScreen) {
                Core.UiController.Show(UiScreenType.Loading);
            }

            Time.timeScale = 0;
            _ = Load();
		}

        private async Task Load() {
            AsyncOperation loading = SceneManager.LoadSceneAsync(_sceneIndex);

            while (!loading.isDone) {
                await Task.Delay(1);
            }

            await Task.Delay(_delay);

            Core.StateController.SetState(_nextState);
        }

        public void Update() {

        }

        public virtual void Exit() {
			Time.timeScale = 1;
        }
    }
}