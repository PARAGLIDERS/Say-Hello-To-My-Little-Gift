using System.Collections.Generic;
using Misc.GameStateMachine.States;
using UnityEngine;

namespace Misc.GameStateMachine {
	public class StateController {
		private readonly Dictionary<StateType, IState> _states;
		private IState _current;

		public StateController() {
			_states = new Dictionary<StateType, IState>() {
				{ StateType.Boot , new StateBoot()},
				{ StateType.Main , new StateMain()},
				{ StateType.Play , new StatePlay()},
				{ StateType.Pause , new StatePause()},
				{ StateType.LoadLevel , new StateLoadLevel()},
				{ StateType.QuitPlay , new StateQuitPlay()},
			};
		}

		public void SetState(StateType stateType) {
			if (_states.TryGetValue(stateType, out IState state)) {
				_current?.Exit();
				_current = state;
				_current?.Enter();
				Debug.Log($"state changed to {stateType}");
				return;
			}
			
			Debug.LogError($"no state {stateType} found");
		}

		public void Update() {
			_current?.Update();
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
	}
}