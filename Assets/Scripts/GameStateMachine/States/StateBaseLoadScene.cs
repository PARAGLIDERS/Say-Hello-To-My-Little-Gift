using System;
using System.Threading.Tasks;
using Misc.Root;
using Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Misc.GameStateMachine.States {
	public abstract class StateBaseLoadScene : IState {
		protected abstract int _sceneIndex { get; }
		protected abstract StateType _nextState { get; }
		
		private AsyncOperation _loadingMainScene;
		
		public virtual void Enter() {
			Core.UiController.Show(UiScreenType.Loading);
			_loadingMainScene = SceneManager.LoadSceneAsync(_sceneIndex);
		}

		public virtual void Update() {
			if(_loadingMainScene == null) return;
			if(!_loadingMainScene.isDone) return;
			
			Core.StateController.SetState(_nextState);
		}

		public virtual void Exit() {
			
		}
	}
}