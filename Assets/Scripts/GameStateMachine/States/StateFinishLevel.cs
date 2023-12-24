using Root;
using System.Threading.Tasks;
using UnityEngine;

namespace GameStateMachine.States {
	public abstract class StateFinishLevel : IState {
		public async void Enter() {
			Core.StateController.Lock();
			Time.timeScale = 0.3f;
			Core.LevelController.Pause();
			Core.UiController.Show(Ui.UiScreenType.PreFinish);
			HandleBeforeEnterDelay();
			await Task.Delay(2000);
			Core.StateController.Unlock();
			Time.timeScale = 0f;
			HandleAfterEnterDelay();
		}

		public void Exit() {
			Time.timeScale = 1f;
			HandleExit();
		}

		public void Update() { }

		protected abstract void HandleBeforeEnterDelay();
		protected abstract void HandleAfterEnterDelay();
		protected abstract void HandleExit();
	}
}
