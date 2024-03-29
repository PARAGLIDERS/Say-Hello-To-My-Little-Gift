using System.Collections.Generic;
using GameStateMachine.States;
using UnityEngine;

namespace GameStateMachine {
	public class StateController {
		private readonly Dictionary<StateType, IState> _states;
		private IState _current;
		private bool _locked;

		public StateController() {
			_states = new Dictionary<StateType, IState>() {
				{ StateType.Boot,		new StateBoot()},
				{ StateType.Main,		new StateMain()},
				{ StateType.Play,		new StatePlay()},
				{ StateType.Pause,		new StatePause()},
				{ StateType.LoadLevel,	new StateLoadLevel()},
				{ StateType.QuitPlay,	new StateQuitPlay()},
				{ StateType.Fail,		new StateFail()},
				{ StateType.Restart,	new StateRestart()},
				{ StateType.Win,		new StateWin()},
				{ StateType.NextLevel,	new StateNextLevel()},
				{ StateType.Ending,		new StateEnding()},
				{ StateType.GunWheel,	new StateGunWheel()},
			};
		}

		public void SetState(StateType stateType) {
			if(_locked) return;
			if (_states.TryGetValue(stateType, out IState state)) {
				_current?.Exit();
				_current = state;
				_current?.Enter();
				//Debug.Log($"state changed to {stateType}");
				return;
			}
			
			Debug.LogError($"no state {stateType} found");
		}

		public void Update() {
			_current?.Update();
		}

		public void Lock() {
			_locked = true;
		}

		public void Unlock() {
			_locked = false;
		}
	}

	public enum StateType {
		Boot,
		Main,
		LoadLevel,
		Play,
		Pause,
		QuitPlay,
		Win,
		Fail,
		Restart,
		NextLevel,
		Ending,
		GunWheel,
	}
}