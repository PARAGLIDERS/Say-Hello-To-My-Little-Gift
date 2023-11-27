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
		
		public virtual void Enter() {
			Core.UiController.Show(UiScreenType.Loading);
            Time.timeScale = 0;
            _ = Load();
		}

        private async Task Load() {
            AsyncOperation loading = SceneManager.LoadSceneAsync(_sceneIndex);

            while (!loading.isDone) {
                await Task.Delay(1000);
            }

            await Task.Delay(1000);

            Core.StateController.SetState(_nextState);
        }

        public void Update() {

        }

        public virtual void Exit() {
			Time.timeScale = 1;
        }
    }
}